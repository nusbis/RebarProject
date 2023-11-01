﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RebarP.Models;
using RebarP.Servers;
using System.Text.RegularExpressions;

namespace RebarP.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private ShakeServe shakeServer = new ShakeServe();
    private OrderService orderServer = new OrderService();
    private CheckoutService accountService = new CheckoutService();


    [HttpPost(Name = "AddOrder")]
    public IActionResult AddOrder(OrderForClient orderForClient)
    {
        DateTime resultDateOfStartOrder;
        if (orderForClient.lstShakes == null || orderForClient.lstShakes.Count == 0)
            return BadRequest("There are no items on order");
        if (orderForClient.lstShakes.Count > 10)
            return BadRequest("An order can include a maximum of 10 shakes :)");
        if (orderForClient.nameOfCustomer == null)
            return BadRequest("Missing customer name");
        if (!DateTime.TryParse(orderForClient.dateOfStartOrder, out resultDateOfStartOrder))
            return BadRequest("Date not in correct format");


        double sumOfOrder = orderForClient.lstShakes.Sum(shake => shake.Price);

        if (orderForClient.lstShakes.Any(item => shakeServer.GetById(item.IDShake) == null))
        {
            return BadRequest("The shake does not exist in the database");
        }

        Order newOrder = new Order
        {
            ListOfShakes = orderForClient.lstShakes,
            StartOrder = resultDateOfStartOrder,
            NameOfCustomer = orderForClient.nameOfCustomer
        };
        newOrder = orderServer.Add(newOrder);
        Checkout myAcoount = accountService.GetById(orderForClient.IdAccount);
        if (myAcoount == null)
            return BadRequest("Account does not exist");

        try { accountService.AddOrderToAccount(newOrder.ID, myAcoount); }
        catch (Exception ex)
        {
            orderServer.Delete(newOrder.ID);
            return BadRequest("We cant add your order in to this account becouse: " + ex.Message);
        }
        return Ok(new { Message = "Order successfully saved", Value = sumOfOrder });
    }





    //foreach (var item in orderForClient.lstShakes)
    //{
    //    if (shakeServer.GetById(item.IDShake) == null)
    //    {
    //        return BadRequest("The shake does not exist in the database");
    //    }
    //}

    //public bool ValidateString(string input)
    //{
    //    // הביטוי הרגולרי מגדיר את התנאים: בדיוק 4 אותיות אנגליות ובדיוק 2 מספרים.
    //    string pattern = @"^[A-Za-z]{4}\d{2}$";

    //    // בודקים האם המחרוזת מתאימה לתבנית הרגולרית.
    //    return Regex.IsMatch(input, pattern);
    //}

    //public bool DoesShakeExist(Guid id)
    //{
    //    if (shakeServer.GetById(id) != null)
    //        return true;
    //    return false;
    //}
}
