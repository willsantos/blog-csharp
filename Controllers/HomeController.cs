using Blog.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Route("v1/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [ApiKey]
        public IActionResult Get()
        {
            return Ok("Sua Api Key está funcionando");
        }
    }
}
