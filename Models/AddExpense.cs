using MongoDB.Driver;

namespace FinanceTracker.Models
{
    public class AddExpense
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

        public static T AddExpenses<T>(T record) where T : class
        {
            try
            {
                var collection = database.GetCollection<T>(typeof(T).Name);
                var DateProperty = typeof(T).GetProperty("Date");

                if (DateProperty != null)
                {
                    var DateValue = DateProperty.GetValue(record);

                    var filter = Builders<T>.Filter.Eq("Date", DateValue);

                    var existingRecord = collection.Find(filter).FirstOrDefault();
                    if (existingRecord != null)
                    {
                        return null;
                    }
                    else
                    {
                        collection.InsertOne(record);
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
