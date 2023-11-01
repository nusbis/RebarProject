using RebarP.Models;
using RebarP.Servers;

namespace RebarP.Controllers;

public static class Validation
{

    public static void ValidationOfOrderForClient(OrderForClient orderForClient)
    {
        DateTime resultDateOfStartOrder;
        if (orderForClient.lstShakes == null || orderForClient.lstShakes.Count == 0)
            throw new Exception("There are no items on order");
        if (orderForClient.lstShakes.Count > 10)
            throw new Exception("An order can include a maximum of 10 shakes :)");
        if (orderForClient.nameOfCustomer == null)
            throw new Exception("Missing customer name");
        if (!DateTime.TryParse(orderForClient.dateOfStartOrder, out resultDateOfStartOrder))
            throw new Exception("Date not in correct format");
    }
    public static void ValidationShake(Shake sh)
    {
        ShakeService shakeService = new ShakeService();
        if (shakeService.NameOfExistingShake(sh.Name))
            throw new Exception("this name of shake is exsist in our system");
        if (sh.Description == null || sh.Description == "")
            throw new Exception("No description for the shake We cannot add a shake without a description");
        if (sh.PriceS == null || sh.PriceL == null || sh.PriceM == null)
            throw new Exception("One of the prices for the shake is missing. We cannot add a shake when one of the sizes is missing a price");

    }
}
