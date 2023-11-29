using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MdtApi.Models
{
    public class Citizen
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int CustomId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string RegNumber { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public string Sex { get; set; } = null!;
        public string Race { get; set; } = null!;
        public bool IsAlive { get; set; }
        public string BirthDate { get; set; } = null!;
        public string Streetname { get; set; } = null!;
        public string AddInfo { get; set; } = null!;
        public string PhotoLink { get; set; } = null!;
    }
}
