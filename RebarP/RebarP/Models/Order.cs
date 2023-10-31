namespace RebarP.Models;

public class Order
{
    public Guid ID { get; private set; } = new Guid();
    public List<ShakeOfOrder> ListOfShakes { get; private set; }
    public DateTime DateOfOrder { get; set; }
    public List<Discount> ListOfDiscount { get; private set; } = null;

}
