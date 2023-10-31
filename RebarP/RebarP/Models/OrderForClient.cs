namespace RebarP.Models;

public class OrderForClient
{
    public List<ShakeOfOrder> lstShakes  { get; set; }
    public string nameOfCustomer  { get; set; }
    public DateTime dateOfStartOrder { get; set; }
}
