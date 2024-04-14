using MongoDB.Driver;

namespace FinanceTracker.Models
{
    public class GetExpense
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
        public List<Expense> GetExpensesOfUser(string userId)
        {
            var collection = database.GetCollection<Expense>("Expense");
            // Query expenses for the specified user ID
            var filter = Builders<Expense>.Filter.Eq("UserId", userId);
            return collection.Find(filter).ToList();
        }
    }
}
