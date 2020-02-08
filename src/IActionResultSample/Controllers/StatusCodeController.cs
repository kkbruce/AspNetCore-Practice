using Microsoft.AspNetCore.Mvc;

namespace IActionResultSample.Controllers
{
    [Route("StatusCode/[action]")]
    [ApiController]
    public class StatusCodeController : ControllerBase
    {
        /// <summary>
        /// Return HTTP 200
        /// 回傳 HTTP 200
        /// </summary>
        /// <returns>StatusCodeResult</returns>
        public IActionResult Demo1()
        {
            return StatusCode(200);
        }

        /// <summary>
        /// Return HTTP 404
        /// 回傳 HTTP 404
        /// </summary>
        /// <returns>StatusCodeResult</returns>
        public IActionResult Demo2()
        {
            return StatusCode(404);
        }

        /// <summary>
        /// Return HTTP 204
        /// 回傳 HTTP 204
        /// </summary>
        /// <returns>ObjectResult</returns>
        public IActionResult Demo3()
        {
            return StatusCode(204, "No Content");
        }
    }
}