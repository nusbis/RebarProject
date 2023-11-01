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


    [HttpPost(Name = "AddAcoount")]
    public IActionResult AddCheckout(Checkout chechout)
    {
        try { checkoutService.Add(chechout); return Ok("The checkout has been successfully added"); }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (Exception) { return BadRequest("Error connecting to the database"); }
    }

    [HttpGet("GetCheckoutByID/{id}")]
    public IActionResult GetById(Guid id)
    {
        Checkout checkout;
        try { checkout = checkoutService.GetById(id);}
        catch { return BadRequest("Error connecting to the database"); }
        return Ok(new { Message = "Get checkout succeeded", Value = checkout });
    }

    [HttpGet(Name = "CloseAcoountForToday")]
    public IActionResult CloseAcoountForToday(string password)
    {
        int countOfOrders;
        double totalPriceForToday = 0;
        List<Order> idsOfOrderOfToday;
        Checkout mycheckout;
        if (password == null) return BadRequest("We cant close this checkout fot today becouse the password is empty");
        try { mycheckout = checkoutService.GetByPassword(password); } catch { return BadRequest("Error connecting to the database"); }
        if (mycheckout == null) return BadRequest("there are no checkout with this password!");
        if (mycheckout.ListOfOrderIDs == null) countOfOrders = 0;
        countOfOrders = mycheckout.ListOfOrderIDs.Count();
        try { idsOfOrderOfToday = orderService.GetAllOrdersById(mycheckout.ListOfOrderIDs.Select(id => Guid.Parse(id)).ToList()); }
        catch { return BadRequest("There is an internal error in the system, one of today's orders cannot be found"); }
        if (idsOfOrderOfToday == null)
            totalPriceForToday = 0;
        else
            totalPriceForToday = idsOfOrderOfToday.Sum(order => order.TotalPrice);
        DailyReport dailyReport = new DailyReport
        {
            CountOfOrders = countOfOrders,
            TotalPriceForAllOrders = totalPriceForToday
        };
        return Ok(new { Message = "Close checkout  successfull Today!", Value = dailyReport });
    }
}
