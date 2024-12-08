using Api.Domain.RequestMessages;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Controller]
[Route("v1/vehicle")]
public class VehicleController : Controller
{
    private readonly IVehicleOperationsService _vehicleOperationsService;
    private readonly ILogger<VehicleController> _logger;
    
    public VehicleController(IVehicleOperationsService vehicleOperationsService, ILogger<VehicleController> logger)
    {
        _vehicleOperationsService = vehicleOperationsService;
        _logger = logger;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _vehicleOperationsService.GetVehicleDetailByIdAsync(id);

        if (result == null) return NotFound();

        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateVehicleRequestMessage requestMessage)
    {
        _logger.LogInformation($"CreateVehicleRequestMessage {System.Text.Json.JsonSerializer.Serialize(requestMessage)}");

        var error = requestMessage.Validate();

        if (error != null)
        {
            return BadRequest(error);
        }
        
        var result = await _vehicleOperationsService.AddVehicleDetailsAsync(requestMessage);

        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> ListAll([FromQuery]ListAllVehicleRequestMessage requestMessage)
    {
        _logger.LogInformation($"ListAllVehicleRequestMessage {System.Text.Json.JsonSerializer.Serialize(requestMessage)}");

        var error = requestMessage.Validate();

        if (error != null)
        {
            return BadRequest(error);
        }
        
        var result = await _vehicleOperationsService.GetVehiclesDetailsAsync(requestMessage);

        return Ok(result);
    }
}