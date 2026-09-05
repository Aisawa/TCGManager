using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TCGManager.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class ApiControllerBase(IMediator mediator) : ControllerBase
{
    protected readonly IMediator Mediator = mediator;
}