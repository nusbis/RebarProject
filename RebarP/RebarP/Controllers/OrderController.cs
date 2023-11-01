using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RebarP.Models;
using RebarP.Servers;
using System.Text.RegularExpressions;

namespace RebarP.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private ShakeService shakeServer = new ShakeService();
    private OrderService orderServer = new OrderService();
    private CheckoutService accountService = new CheckoutService();


    [HttpPost(Name = "AddOrder")]
    public IActionResult AddOrder(OrderForClient orderForClient)
    {
        Checkout myAcoount;

        try { Validation.ValidationOfOrderForClient(orderForClient); }
        catch (Exception ex){ return BadRequest(ex.Message); }

        double sumOfOrder = orderForClient.lstShakes.Sum(shake => shake.Price);
        sumOfOrder= orderForClient.ListOfDiscount.Sum(dis=>dis.DiscountPercentages*0.1*sumOfOrder);

        try
        {
            if (orderForClient.lstShakes.Any(item => shakeServer.GetById(item.IDShake) == null))
                return BadRequest("The shake does not exist in the database");
        }
        catch { return BadRequest("Error connecting to the database"); }

        Order newOrder = new Order
        {
            ListOfShakes = orderForClient.lstShakes,
            StartOrder =DateTime.Parse(orderForClient.dateOfStartOrder),
            NameOfCustomer = orderForClient.nameOfCustomer,
            TotalPrice=sumOfOrder
        };

        try
        {
            newOrder = orderServer.Add(newOrder);
             myAcoount = accountService.GetById(orderForClient.IdAccount);
            if (myAcoount == null) return BadRequest("The checkout does not exist in the database");
        }
        catch { return BadRequest("Error connecting to the database"); }

        try { accountService.AddOrderToAccount(newOrder.ID, myAcoount); }
        catch (Exception ex)
        {
            orderServer.Delete(newOrder.ID);
            return BadRequest("We cant add your order in to this account becouse: " + ex.Message);
        }
        return Ok(new { Message = "Order successfully saved", Value = sumOfOrder });
    }





    

    //public bool ValidateString(string input)
    //{
    //    // הביטוי הרגולרי מגדיר את התנאים: בדיוק 4 אותיות אנגליות ובדיוק 2 מספרים.
    //    string pattern = @"^[A-Za-z]{4}\d{2}$";

    //    // בודקים האם המחרוזת מתאימה לתבנית הרגולרית.
    //    return Regex.IsMatch(input, pattern);
    //}
}
