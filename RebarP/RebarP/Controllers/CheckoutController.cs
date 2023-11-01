using Microsoft.AspNetCore.Mvc;
using RebarP.Models;
using RebarP.Servers;

namespace RebarP.Controllers;
[ApiController]
[Route("[controller]")]
public class CheckoutController : ControllerBase
{
    private CheckoutService checkoutService = new CheckoutService();
    private OrderService orderService = new OrderService();


    //[HttpPost(Name = "AddAcoount")]
    //public void AddAccount(Checkout account)
    //{
    //    checkoutService.Add(account);
    //}

    [HttpGet("GetAccountByID/{id}")]
    public Checkout GetById(Guid id)
    {
        return checkoutService.GetById(id);
    }
    //[HttpPost(Name = "CloseAcoountForToday")]
    //public IActionResult CloseAcoountForToday(string password)
    //{
    //    int countOfOrders;
    //    double totalPriceForToday=0;
    //    List<Order> idsOfOrderOfToday;
    //    if (password == null) return BadRequest("We cant close this checkout fot today becouse the password is empty");
    //    Checkout mycheckout = checkoutService.GetByPassword(password);
    //    if (mycheckout == null) return BadRequest("there are no checkout with this password!");
    //    if (mycheckout.ListOfOrderIDs == null) countOfOrders = 0;
    //    countOfOrders = mycheckout.ListOfOrderIDs.Count();
    //    //List<Guid> orderIDs = mycheckout.ListOfOrderIDs.Select(id => Guid.Parse(id)).ToList();
    //    try
    //    {
    //        idsOfOrderOfToday = orderService.GetAllOrdersById(mycheckout.ListOfOrderIDs.Select(id => Guid.Parse(id)).ToList());
    //    }
    //    catch { return BadRequest("There is an internal error in the system, one of today's orders cannot be found"); }
    //    if (idsOfOrderOfToday == null) return BadRequest("there are no orders for todaty");//לבדקקקק
    //    foreach (var order in idsOfOrderOfToday)
    //    {
    //        totalPriceForToday += order.ListOfShakes.Sum(shake => shake.Price);
    //    }
    //    DailyReport dailyReport = new DailyReport
    //    {
    //        CountOfOrders = countOfOrders,
    //        TotalPriceForAllOrders = totalPriceForToday
    //    };
    //    return Ok(new { Message = "Close checkout  successfull Today!", Value = dailyReport });



    //}
}
