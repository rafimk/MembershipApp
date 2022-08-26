using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Membership.Api.Controllers.Commons;


[ApiController]
[Authorize]
[Route("[controller]")]
public class ApiController : ControllerBase
{
}
