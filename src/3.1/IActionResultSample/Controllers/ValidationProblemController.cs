using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IActionResultSample.Controllers
{
    [Route("ValidationProblem/[action]")]
    [ApiController]
    public class ValidationProblemController : ControllerBase
    {
        /// <summary>
        /// Return HTTP 400
        /// 回傳 HTTP 400
        /// </summary>
        /// <returns>ActionResult</returns>
        public IActionResult Demo1()
        {
            return ValidationProblem();
        }

        /// <summary>
        /// Return HTTP 400 and ValidationProblemDetails object.
        /// 回傳 HTTP 400 和 ValidationProblemDetails 物件。
        /// </summary>
        /// <returns>ActionResult</returns>
        public IActionResult Demo2()
        {
            var problemDetails = new ValidationProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc7807",
                Title = "Validation Problem.",
                Detail = "One or more validation problem.",
                Instance = HttpContext.Request.Path
            };

            return ValidationProblem(problemDetails);
        }


        /// <summary>
        /// Return HTTP 400 and ModelState(MVC).
        /// 回傳 HTTP 400 和 ModelState 物件。
        /// </summary>
        /// <returns>ActionResult</returns>
        public IActionResult Demo3()
        {
            var msd = new ModelStateDictionary();
            return ValidationProblem(msd);
        }
    }
}