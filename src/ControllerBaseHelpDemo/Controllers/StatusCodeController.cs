using Microsoft.AspNetCore.Mvc;

namespace ControllerBaseHelpDemo.Controllers
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
        public IActionResult Demo200()
        {
            return StatusCode(200);
        }

        /// <summary>
        /// Return HTTP 404
        /// 回傳 HTTP 404
        /// </summary>
        /// <returns>StatusCodeResult</returns>
        public IActionResult Demo404()
        {
            return StatusCode(404);
        }

        /// <summary>
        /// Return HTTP 204
        /// 回傳 HTTP 204
        /// </summary>
        /// <returns>ObjectResult</returns>
        public IActionResult Demo204()
        {
            return StatusCode(204, "No Content");
        }
    }
}