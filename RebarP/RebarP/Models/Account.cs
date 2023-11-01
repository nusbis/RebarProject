using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;

namespace RebarP.Models;

public class Account
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid ID { get;private set; }= Guid.NewGuid();
    public string Password { get; set; }
    public List<Guid> ListOfOrderIDs { get; private set; } = new List<Guid>();
}
