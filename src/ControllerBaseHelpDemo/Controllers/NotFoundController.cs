using Microsoft.AspNetCore.Mvc;

namespace IActionResultSample.Controllers
{
    [Route("NotFound/[action]")]
    [ApiController]
    public class NotFoundController : ControllerBase
    {
        /// <summary>
        /// Return HTTP 404
        /// 回傳 HTTP 404
        /// </summary>
        /// <returns>NotFoundResult</returns>
        public IActionResult DemoNotFound1()
        {
            return NotFound();
        }

        /// <summary>
        /// Return HTTP 404 and custom message object.
        /// 回傳 HTTP 404 與自訂訊息物件。
        /// </summary>
        /// <returns>NotFoundObjectResult</returns>
        public IActionResult DemoNotFound2()
        {
            var obj = new
            {
                Error = "You are unauthorized."
            };
            return NotFound(obj);
        }
    }
}