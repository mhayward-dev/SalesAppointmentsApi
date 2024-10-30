using Microsoft.AspNetCore.Mvc;

namespace SalesAppointments.Api.Controllers;

[ApiController]
[Route("healthcheck")]
public class HealthCheckController
{
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [HttpGet("full", Name = "Healthcheck")]
    public IActionResult Healthcheck()
    {
        return new OkObjectResult("Service is healthy");
    }
}
