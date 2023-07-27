using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace CNABSolution.Server.Models.Account
{
    internal class Account
    {
        [BsonElement("account_id")]
        [BsonRequired]
        public required string account_id { get; set; }

        [BsonElement("account_owner")]
        [BsonRequired]
        public required string account_owner { get; set; }
    }
}
