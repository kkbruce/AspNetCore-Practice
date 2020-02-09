using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IActionResultSample.Controllers
{
    [Route("BadRequest/[action]")]
    [ApiController]
    public class BadRequestController : ControllerBase
    {
        /// <summary>
        /// Return HTTP 400
        /// 回傳 HTTP 400
        /// </summary>
        /// <returns>BadRequestResult</returns>
        public IActionResult Demo1()
        {
            return BadRequest();
        }

        /// <summary>
        /// Return HTTP 400 and custom message object.
        /// 回傳 HTTP 400 與自訂訊息物件。
        /// </summary>
        /// <returns>BadRequestObjectResult</returns>
        public IActionResult Demo2()
        {
            var obj = new
            {
                Error = "Bad Request."
            };
            return BadRequest(obj);
        }

        /// <summary>
        /// Return HTTP 400 and ModelState(MVC)
        /// 回傳 HTTP 400 和 ModelState 物件。
        /// </summary>
        /// <returns>BadRequestObjectResult</returns>
        public IActionResult Demo3()
        {
            var msd = new ModelStateDictionary();
            return BadRequest(msd);
        }

        /// <summary>
        /// Return HTTP 400 and ProblemDetails obj.
        /// 回傳 HTTP 400 和 ProblemDetails 物件。
        /// </summary>
        /// <returns>BadRequestObjectResult</returns>
        public IActionResult Demo4()
        {
            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc7807",
                Title = "BadRequest",
                Detail = "One or more client problem.",
                Instance = HttpContext.Request.Path,
                Extensions =
                {
                    ["tracking-id"] = "9527"
                }
            };
            return BadRequest(problemDetails);
        }

        /// <summary>
        /// Return HTTP 400 and ValidationProblemDetails obj.
        /// 回傳 HTTP 400 和 ValidationProblemDetails 物件。
        /// </summary>
        /// <returns>BadRequestObjectResult</returns>
        public IActionResult Demo5()
        {
            var problemDetails = new ValidationProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc7807",
                Title = "BadRequest",
                Detail = "One or more client problem.",
                Instance = HttpContext.Request.Path,
                Extensions =
                {
                    ["tracking-id"] = "9527"
                },
                Errors =
                {
                    { "Error", new[] { "Error Message" } },
                }
            };
            return BadRequest(problemDetails);
        }
    }
}