using Microsoft.AspNetCore.Mvc;
using RebarP.Models;
using RebarP.Servers;

namespace RebarP.Controllers;
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private AccountService accountServer = new AccountService();

    [HttpPost(Name = "AddAcoount")]
    public void AddAccount(Account account)
    {
        accountServer.Add(account);
    }

    [HttpGet("GetAccountByID/{id}")]
    public Account GetById(Guid id)
    {
        return accountServer.GetById(id);
    }
}
