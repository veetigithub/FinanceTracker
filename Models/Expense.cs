using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models
{
    public class Expense
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Description { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Category { get; set; }
    }
}
