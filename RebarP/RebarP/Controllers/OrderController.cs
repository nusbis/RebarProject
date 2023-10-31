using Microsoft.AspNetCore.Mvc;
using RebarP.Models;
using RebarP.Servers;

namespace RebarP.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private ShakeServe shakeServer = new ShakeServe();
    private OrderServer orderServer = new OrderServer();



    [HttpPost(Name = "AddOrder")]
    public void AddOrder(OrderForClient orderForClient)
    {
        if (orderForClient.lstShakes == null || orderForClient.lstShakes.Count == 0)
            throw new ArgumentNullException("There are no items on order");
        if (orderForClient.lstShakes.Count > 10)
            throw new ArgumentException("An order can include a maximum of 10 shakes :)");
        if (orderForClient.nameOfCustomer == null)
            throw new ArgumentNullException("Missing customer name");

        double sumOfOrder = orderForClient.lstShakes.Sum(shake => shake.Price);
        orderForClient.lstShakes.Select(item => shakeServer.GetById(item.ID));
        Order newOrder = new Order
        {
            ListOfShakes = orderForClient.lstShakes,
            StartOrder = orderForClient.dateOfStartOrder,
            NameOfCustomer=orderForClient.nameOfCustomer
        };

            orderServer.Add(newOrder);


    }



    //public bool DoesShakeExist(Guid id)
    //{
    //    if (shakeServer.GetById(id) != null)
    //        return true;
    //    return false;
    //}
}
