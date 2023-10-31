using RebarP.Models;
using RebarP.Servers;
using Microsoft.AspNetCore.Mvc;


namespace RebarP.Controllers;
[ApiController]
[Route("[controller]")]
public class ShakeController : ControllerBase
{
    private ShakeServe shakeServer=new ShakeServe();

    [HttpGet(Name = "GetAllShakes")]
    public List<Shake> GetAllShakes()
    {
        return shakeServer.GetAll();
    }

    [HttpPost(Name = "AddShake")]
    public void AddShake(Shake shake)
    {
       shakeServer.Add(shake);
    }
}
