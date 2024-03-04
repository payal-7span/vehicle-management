using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VM.API.Common.Base
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}
