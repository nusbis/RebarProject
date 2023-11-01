using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;

namespace RebarP.Models;

public class Checkout
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid ID { get;private set; }= Guid.NewGuid();
    public string Password { get; set; }
    public List<string> ListOfOrderIDs { get; private set; } = new List<string>();
}
