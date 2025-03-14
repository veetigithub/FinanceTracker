﻿using MongoDB.Bson;
using MongoDB.Driver;

namespace FinanceTracker.Models
{
    public class Register
    {
        private static string? DATABASE_NAME;
        private static string? HOST;

        private static IConfiguration? config;
        private static MongoServerAddress? address;
        private static MongoClientSettings? clientSettings;
        private static MongoClient? client;
        public static IMongoDatabase? database;

        public static void Initialize(IConfiguration configuration)
        {
            config = configuration;
            var sections = config.GetSection("ConnectionStrings");
            DATABASE_NAME = sections.GetValue<string>("DatabaseName");
            HOST = sections.GetValue<string>("MongoConnection");
            address = new MongoServerAddress(HOST);
            clientSettings = new MongoClientSettings() { Server = address };
            client = new MongoClient(clientSettings);
            database = client.GetDatabase(DATABASE_NAME);
        }

        public static T RegisterUser<T>(T record) where T : class
        {
            try
            {
                var collection = database.GetCollection<T>(typeof(T).Name);
                var usernameProperty = typeof(T).GetProperty("Username");

                if (usernameProperty != null)
                {
                    var usernameValue = usernameProperty.GetValue(record);

                    var filter = Builders<T>.Filter.Eq("Username", usernameValue);

                    var existingRecord = collection.Find(filter).FirstOrDefault();
                    if (existingRecord != null)
                    {
                        // Username already exists, handle accordingly (e.g., return null or throw an exception)
                        return null;
                    }
                    else
                    {
                        collection.InsertOne(record);
                        return record;
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Nyt kämähti :(  " + err.Message);
            }
            return record;
        }
    }
}
