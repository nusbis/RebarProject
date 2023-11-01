namespace RebarP.Models;

public class DailyReport
{
    public Guid Id { get; private set; } = new Guid();
    public int CountOfOrders { get; set; }
    public double TotalPriceForAllOrders { get; set; }
}
