using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TSC.Common.Models;
using Toolbox.Portable.Services;
using SQLite;

namespace TSC.Common.Services
{
    public class Database
    {

        public static SQLiteConnection Connection
        {
            get
            {
                return connection;
            }
        }

        private static SQLiteConnection connection;
        private static string databaseFile => "weather.db3";

        public static void Initialize()
        {
            string path = Path.Combine(FileSystem.DocumentStorage.BasePath, databaseFile);
            connection = new SQLiteConnection(path);
            CreateDatabaseAndTables();
        }

        static void CreateDatabaseAndTables()
        {
            connection.CreateTable<City>();
            connection.CreateTable<CityBackground>();
            var cityTable = connection.Table<City>();

            if (!cityTable.Any())
            {
                connection.InsertAll(new List<City>{
                    new City{
                         Name = "Bellevue"
                    },
                     new City{
                         Name = "San Francisco"
                    },
                     new City{
                         Name = "London"
                    },
                });
            }
        }

    }
}