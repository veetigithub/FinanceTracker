using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models
{
    public class Registration
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
