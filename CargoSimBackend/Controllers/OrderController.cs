using System.Text.RegularExpressions;
using CargoSimBackend.Services.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargoSimBackend;


[ApiController]
[Route("api/v1/[controller]")]
public class OrderController:ControllerBase
{
    private readonly ISimulationService _simulationService;
    private readonly IGridService _gridService;

    public OrderController(ISimulationService simulationService,IGridService gridService){
        _simulationService = simulationService;
        _gridService = gridService;
    }

    [HttpPost("sim/start")]
    public async Task<ActionResult> StartSim(){

        try
        {
            await this._gridService.SetGrid();
            await this._simulationService.startSimulation();
            
            return Ok();
        }
        catch (Exception ex)
        {
            string message=Regex.Replace(ex.Message, @"[0-9]+","****");
            return StatusCode(500, new { message = message});
        }
    }
    [HttpPost("sim/stop")]
    public async Task<ActionResult> StopSim(){

        try
        {
           await  this._simulationService.stopSimulation();
            return Ok();
        }
        catch (Exception ex)
        {
            string message=Regex.Replace(ex.Message, @"[0-9]+","****");
            return StatusCode(500, new { message = message});
        }
    }
}
