using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MdtApi.Models
{
    public class Impound
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int CustomId { get; set; } 
        public string Parking { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string RegNumber { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Requirements { get; set; } = null!;
        public string PhotoLink { get; set; } = null!;
        public string AddInfo { get; set; } = null!;
        public string UpdateTime { get; set; } = null!;
        public string CreateTime { get; set; } = null!;
        public Citizen? Owner { get; set; } = null!;
        
    }

}

