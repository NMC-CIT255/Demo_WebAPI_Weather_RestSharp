using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Demo_FileIO_NTier.DataAccessLayer;
using Demo_FileIO_NTier.Models;
using Demo_FileIO_NTier;

namespace Demo_FileIO_NTier.DataAccessLayer
{
    public class CsvDataService : IDataService
    {
        private string _dataFilePath;

        public CsvDataService()
        {
            _dataFilePath = DataSettings.dataFilePath;
        }

        public IEnumerable<Character> ReadAll()
        {
            List<string> charactersStrings = new List<string>();
            List<Character> characters = new List<Character>();

            try
            {
                StreamReader sr = new StreamReader(_dataFilePath);
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        charactersStrings.Add(sr.ReadLine());
                    }
                }
                foreach (string characterString in charactersStrings)
                {
                    characters.Add(CharacterObjectBuilder(characterString));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return characters;
        }

        public void WriteAll(IEnumerable<Character> characters)
        {
            try
            {
                StreamWriter sw = new StreamWriter(_dataFilePath);
                using (sw)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Clear();
                    foreach (Character character in characters)
                    {
                        sb.AppendLine(CharacterStringBuilder(character));
                    }
                    sw.Write(sb.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// extract the properties from the character string and 
        /// add them to a Character object
        /// </summary>
        /// <param name="characterString">character string</param>
        /// <returns>Character object</returns>
        private Character CharacterObjectBuilder(string characterString)
        {
            const char DELINEATOR = ',';
            string[] properties = characterString.Split(DELINEATOR);

            Character character = new Character()
            {
                Id = Convert.ToInt32(properties[0]),
                LastName = properties[1],
                FirstName = properties[2],
                Address = properties[3],
                City = properties[4],
                State = properties[5],
                Zip = properties[6],
                Age = Convert.ToInt32(properties[7]),
                Gender = (Character.GenderType)Enum.Parse(typeof(Character.GenderType), properties[8])
            };

            return character;
        }

        /// <summary>
        /// convert the properties from the Character object  
        /// into a string
        /// </summary>
        /// <param name="characterObject">Character object</param>
        /// <returns>Character string</returns>
        private string CharacterStringBuilder(Character characterObject)
        {
            const string DELINEATOR = ",";
            string characterString;

            characterString =
                characterObject.Id + DELINEATOR +
                characterObject.LastName + DELINEATOR +
                characterObject.FirstName + DELINEATOR +
                characterObject.Address + DELINEATOR +
                characterObject.City + DELINEATOR +
                characterObject.State + DELINEATOR +
                characterObject.Zip + DELINEATOR +
                characterObject.Age + DELINEATOR +
                characterObject.Gender;

            return characterString;
        }
    }
}
