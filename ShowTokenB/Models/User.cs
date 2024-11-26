using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ShowTokenB.Models
    {
        public class User
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; } = string.Empty;


            [BsonElement("username")]
            public string Username { get; set; } = string.Empty;

            [BsonElement("password")]
            public string Password { get; set; } = string.Empty;
        }
    }
