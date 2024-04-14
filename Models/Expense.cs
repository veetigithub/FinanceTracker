using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinanceTracker.Models
{
    public class Expense
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
    }
}
