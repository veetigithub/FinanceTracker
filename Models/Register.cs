using MongoDB.Bson;
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
                var objectIdProperty = typeof(T).GetProperty("_id");
                if (objectIdProperty != null)
                {
                    var objectIdValue = objectIdProperty.GetValue(record, null);
                    if (objectIdValue is ObjectId objectId)
                    {
                        Console.WriteLine("toimiiko???");
                        var filter = Builders<T>.Filter.Eq("_id", objectId);
                        var existingRecord = collection.Find(filter).FirstOrDefault();
                        if (existingRecord != null)
                        {
                            if (record is Registration userRecord) // Testausta varten jotta voi vaihtaa nimen jos löytyy sama id
                            {
                                userRecord.FirstName = "erilainennimi";
                            }
                            // Löytyy sama id joten replace
                            var replaceResult = collection.ReplaceOne(filter, record, new ReplaceOptions { IsUpsert = true });
                            if (replaceResult.IsAcknowledged && replaceResult.ModifiedCount > 0)
                            {
                                return record;
                            }
                        }
                        else
                        {
                            // Ei löydy samaa id:tä joten insert
                            collection.InsertOne(record);
                            return record;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Nyt kämähti :(  " + err.Message);
            }
            // For simplicity, let's just print the user details to the console
            //Console.WriteLine($"User registered: firstname={firstname}, lastname={lastname} username={username}, password={password}");
            return record;
        }
    }
}
