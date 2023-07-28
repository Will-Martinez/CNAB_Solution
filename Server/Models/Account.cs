using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace CNABSolution.Server.Models.Account
{
    public class Account
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("account_id")]
        [BsonRequired]
        public required string account_id { get; set; }

        [BsonElement("account_owner")]
        [BsonRequired]
        public required string account_owner { get; set; }
    }
}
