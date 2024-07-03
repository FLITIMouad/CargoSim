using Microsoft.AspNetCore.Mvc;

namespace CargoSimBackend;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderController:ControllerBase
{
    public OrderController(){
    }

    [HttpGet("GetAllAvailable")]
    public async Task<ActionResult> GetAllAvailable()
    {
        return Ok();
    }
}
