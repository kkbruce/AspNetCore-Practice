using Microsoft.AspNetCore.Mvc;

namespace IActionResultSample.Controllers
{
    [Route("Unauthorized/[action]")]
    [ApiController]
    public class UnauthorizedController : ControllerBase
    {
        /// <summary>
        /// Return HTTP 401
        /// 回傳 HTTP 401
        /// </summary>
        /// <returns>UnauthorizedResult</returns>
        public IActionResult Demo1()
        {
            return Unauthorized();
        }

        /// <summary>
        /// Return HTTP 401 and custom message object.
        /// 回傳 HTTP 401 與自訂訊息物件。
        /// </summary>
        /// <returns>UnauthorizedObjectResult</returns>
        public IActionResult Demo2()
        {
            var obj = new
            {
                Error = "You are unauthorized."
            };
            return Unauthorized(obj);
        }
    }
}