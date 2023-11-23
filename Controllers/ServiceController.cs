using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        [HttpGet]
        [Route("ok")]
        public IActionResult GetOk()
        {
            return Ok("OK");
        }
    }
}
