namespace Membership.Api.Controllers;

public class TestController : ApiContollerBase
{
    [HttpGet()]
    public async Task<ActionResult> Get()
    {
        return await Mediator.Send(GetAreas query);
    }
}