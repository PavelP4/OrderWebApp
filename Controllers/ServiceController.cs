using Microsoft.AspNetCore.Mvc;
using OrderWebApp.Exceptions;

namespace OrderWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(ILogger<ServiceController> logger)
        { 
            _logger = logger;
        }

        [HttpGet]
        [Route("ok")]
        public IActionResult GetOk()
        {
            return Ok("OK");
        }

        [HttpPost]
        [Route("throw-unknown-error")]
        public IActionResult ThrowUnknownError()
        {
            throw new UnknownException("Some unknown exception message.");
        }

        [HttpPost]
        [Route("throw-known-error")]
        public IActionResult ThrowKnownError()
        {
            throw new AppException("Some app exception message.");
        }

        [HttpPost]
        [Route("log-message")]
        public IActionResult LogMessage()
        {
            _logger.LogInformation("Some info message.");

            return Ok("Logged");
        }
    }
}
