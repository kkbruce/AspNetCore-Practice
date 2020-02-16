using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IActionResultSample.Controllers
{
    [Route("Problem/[action]")]
    [ApiController]
    public class ProblemController : ControllerBase
    {
        /// <summary>
        /// Default return HTTP 500
        /// 預設回傳 HTTP 500
        /// </summary>
        /// <returns>ObjectResult</returns>
        public IActionResult Demo1()
        {
            return Problem();
        }

        /// <summary>
        /// Return HTTP 500 and custom message information.
        /// 回傳 HTTP 500 和與自訂訊息物件。
        /// </summary>
        /// <returns>ObjectResult</returns>
        public IActionResult Demo2()
        {
            var title = "The website is down";
            var detail = "Try again in a few minutes.";

            return Problem(detail: detail, title: title);
        }

        /// <summary>
        /// Return custom HTTP 422 and custom message information.
        /// 回傳自訂狀態碼 HTTP 422 與自訂訊息物件。
        /// </summary>
        /// <returns>ObjectResult</returns>
        public IActionResult Demo3()
        {
            var title = "Unprocessable entity";
            var detail = "Try again in a few minutes.";
            return Problem(detail:detail, title: title, statusCode:422);
        }

        /// <summary>
        /// Return custom HTTP 429 and custom message information.
        /// </summary>
        /// <returns>ObjectResult</returns>
        public IActionResult Demo4()
        {
            var detail = "Try again in a few minutes.";
            var instance = HttpContext.Request.Path;
            var title = "Too Many Requests";
            var type = "https://tools.ietf.org/html/rfc7807";
            var statusCode = StatusCodes.Status429TooManyRequests;

            return Problem(detail, instance, statusCode, title, type);
        }
    }
}