using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace QuickCode.Demo.OnlineShopModule.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ErrorController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(IWebHostEnvironment env, ILogger<ErrorController> logger)
    {
        _env = env;
        _logger = logger;
    }

    [Route("/error")]
    [HttpGet]  
    public IActionResult HandleError()
    {
        var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionFeature?.Error;
        
        _logger.LogError(exception, "Unhandled exception occurred");

        var problemDetails = new ProblemDetails
        {
            Status = 500,
            Title = "An unexpected error occurred."
        };

        problemDetails.Detail = _env.IsDevelopment() ? exception?.ToString() : "Please contact support.";

        return StatusCode(500, problemDetails);
    }
}