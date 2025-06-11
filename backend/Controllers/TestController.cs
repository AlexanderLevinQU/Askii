using Microsoft.AspNetCore.Mvc;

namespace Askii.backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "CORS is working!" });
        }
    }
}
