using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TCGManager.Api.Controllers.V1;

public class HealthController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet]
    public IActionResult Get() => Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
}