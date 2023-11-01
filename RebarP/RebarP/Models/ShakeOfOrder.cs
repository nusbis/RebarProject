namespace RebarP.Models;

public class ShakeOfOrder
{
    public Guid ID { get; private set; }
    public Guid IDShake { get; set; }
    public Size Size { get; set; }
    public double Price { get; set; }
}
