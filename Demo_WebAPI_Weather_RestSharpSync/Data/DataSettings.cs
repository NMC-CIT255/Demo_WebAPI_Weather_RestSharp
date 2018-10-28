using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_FileIO_NTier
{
    public static class DataSettings
    {
        //public const string dataFilePath = @"Data\Data.csv";
        //public const string dataFilePath = @"Data\Data.xml";
        public const string dataFilePath = @"Data\Data.json";
        public const string connectionString = "mongodb://johnvelis:password01@cluster0-shard-00-00-hasci.mongodb.net:27017,cluster0-shard-00-01-hasci.mongodb.net:27017,cluster0-shard-00-02-hasci.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true";
    }
}
