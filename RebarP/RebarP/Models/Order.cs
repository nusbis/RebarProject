using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RebarP.Models;

public class Order
{
   // private List<ShakeOfOrder> _listOfShakes;
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid ID { get; private set; } = new Guid();
    public List<ShakeOfOrder> ListOfShakes { get; set; }
    public DateTime StartOrder { get; set; }
    public string NameOfCustomer { get; set; }
    public DateTime EndOrder { get; }=DateTime.Now;
    public List<Discount> ListOfDiscount { get; private set; } = new List<Discount>();
    public double TotalPrice { get; set; }

}
