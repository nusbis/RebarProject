namespace RebarP.Models;

public class OrderForClient
{
    public List<ShakeOfOrder> lstShakes  { get; set; }
    public string nameOfCustomer  { get; set; }
    public string dateOfStartOrder { get; set; }
    public Guid IdAccount { get; set; }
}
