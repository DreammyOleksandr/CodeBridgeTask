using Microsoft.AspNetCore.Mvc;

namespace CodeBridgeTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkingController : ControllerBase
    {
        [HttpGet]
        [Route("ping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> Get()
        {
            try
            {
                return Ok("Dogshouseservice.Version1.0.1");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
