using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace CNABSolution.Server.Models.Transaction;

public class Transaction
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("type")]
    [BsonRequired]
    public required string type { get; set; }

    [BsonElement("transaction_date")]
    [BsonRequired]
    public required string transaction_date { get; set; }

    [BsonElement("amount")]
    [BsonRequired]
    public required string amount { get; set; }

    [BsonElement("cpf")]
    [BsonRequired]
    public required string cpf { get; set; }

    [BsonElement("card_number")]
    [BsonRequired]
    public required string card_number { get; set; }

    [BsonElement("store_owner")]
    [BsonRequired]

    public required string store_owner { get; set; }

    [BsonElement("store_name")]
    [BsonRequired]
    public required string store_name { get; set; }
}
