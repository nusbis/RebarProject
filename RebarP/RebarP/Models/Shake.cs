using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RebarP.Models;

public class Shake
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid ID { get;private set; } = new Guid();
    public string Name { get; set; }
    public string Description { get; set; }
    public double PriceS { get; set; }
    public double PriceM { get; set; }
    public double PriceL { get; set; }

}
