using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MdtApi.Models
{
    public class Officer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int CustomId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Rank { get; set; } = null!;
        public string PersonalNumber { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string PhotoLink { get; set; } = null!;
        public string Passcode { get; set; } = null!;
        public Citizen Owner { get; set; } = null!;
    }
}
