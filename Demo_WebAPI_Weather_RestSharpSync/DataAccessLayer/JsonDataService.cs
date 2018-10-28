using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Demo_FileIO_NTier.Models;
using Newtonsoft;
using Demo_FileIO_NTier.DataAccessLayer;
using Newtonsoft.Json;

namespace Demo_FileIO_NTier.DataAccessLayer

{
    public class JsonDataService : IDataService
    {
        private string _dataFilePath;

        /// <summary>
        /// read the json file and load a list of character objects
        /// </summary>
        /// <returns>list of characters</returns>
        public IEnumerable<Character> ReadAll()
        {
            List<Character> characters = new List<Character>();

            try
            {
                using (StreamReader sr = new StreamReader(_dataFilePath))
                {
                    string jsonString = sr.ReadToEnd();

                    Characters characterList = JsonConvert.DeserializeObject<RootObject>(jsonString).Characters;

                    characters = characterList.Character;
               }

            }
            catch (Exception )
            {
                throw; 
            }

            return characters;
        }

        /// <summary>
        /// write the current list of characters to the json data file
        /// </summary>
        /// <param name="characters">list of characters</param>
        public void WriteAll(IEnumerable<Character> characters)
        {
            RootObject rootObject = new RootObject();
            rootObject.Characters = new Characters();
            rootObject.Characters.Character = characters as List<Character>;

            string jsonString = JsonConvert.SerializeObject(rootObject);

            try
            {
                StreamWriter writer = new StreamWriter(_dataFilePath);
                using (writer)
                {
                    writer.WriteLine(jsonString);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonDataService()
        {
            _dataFilePath = DataSettings.dataFilePath;
        }
    }
}
