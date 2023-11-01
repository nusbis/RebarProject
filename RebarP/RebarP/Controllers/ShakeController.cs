using RebarP.Models;
using RebarP.Servers;
using Microsoft.AspNetCore.Mvc;

namespace RebarP.Controllers;

[ApiController]
[Route("[controller]")]
public class ShakeController : ControllerBase
{
    private ShakeService shakeServer=new ShakeService();

    [HttpGet(Name = "GetAllShakes")]
    public IActionResult GetAllShakes()
    {
        List<Shake> lstShakes;
        try { lstShakes = shakeServer.GetAll(); }
        catch { return BadRequest("Error connecting to the database"); }
        return Ok(new { Message = "Get all shakes successfull", Value = lstShakes });
    }

    [HttpPost(Name = "AddShake")]
    public IActionResult AddShake(Shake shake)
    {
        try { Validation.ValidationShake(shake); }
        catch(Exception ex) { return BadRequest(ex.Message); }
        try { shakeServer.Add(shake); }
        catch { return BadRequest("Error connecting to the database"); }
        return Ok("The shake has been successfully added");
    }

    [HttpGet("GetShakeByID/{id}")]
    public IActionResult GetById(Guid id)
    {
        Shake myShake;
        if (id == Guid.Empty)
            return BadRequest("the is of shake is empty");
        try { myShake = shakeServer.GetById(id); }
        catch { return BadRequest("Error connecting to the database"); }
        return Ok(new { Message = "Get shake by id successfull", Value = myShake });
    }
}
