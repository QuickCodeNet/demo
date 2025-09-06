using Microsoft.AspNetCore.Mvc;
using QuickCode.Demo.Common.Filters;

namespace QuickCode.Demo.Common.Controllers;

[ApiExceptionFilter]
[ApiController]
[ApiKey]
[Route("api/[controller]")]
public class QuickCodeBaseApiController : ControllerBase
{

}