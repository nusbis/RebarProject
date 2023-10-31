namespace RebarP.Models;

public class Shake
{
    public Guid ID { get;private set; } = new Guid();
    public string Name { get; set; }
    public string Description { get; set; }
    public double PriceS { get; }
    public double PriceM { get; }
    public double PriceL { get; }

}
