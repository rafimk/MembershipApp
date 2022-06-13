namespace Membership.Api.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class ApiContollerBase : ControllerBase
{
    private ISender _mediator;
    protected ISender Mediator => _mediator ?? HttpContext.RequestServices.GetService<ISender>();
}