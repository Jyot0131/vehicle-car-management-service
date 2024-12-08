using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Controller]
[Route("commission-report/v1/salesman")]
public class CommissionReportController : Controller
{
    private readonly IVehicleOperationsService _vehicleOperationsService;
    private readonly ILogger<CommissionReportController> _logger;
    
    public CommissionReportController(IVehicleOperationsService vehicleOperationsService, ILogger<CommissionReportController> logger)
    {
        _vehicleOperationsService = vehicleOperationsService;
        _logger = logger;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation($"Commission report Get API called with Id {id}");
        var result = await _vehicleOperationsService.GetCommissionDetailsBySalesmanIdAsync(id);
        return Ok(result);
    }
}