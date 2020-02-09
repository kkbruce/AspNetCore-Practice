using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        /// <returns></returns>
        public IActionResult Demo2()
        {
            var problemDetails = new ValidationProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://skilltree.my",
                Title = "Level up your ASP.NET Core Web API",
                Detail = "You need skilltree.my to level up your ASP.NET Core skill.",
                Instance = HttpContext.Request.Path
            };

            return ValidationProblem(problemDetails);
        }

        // Todo: for MVC
        //ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
    }
}