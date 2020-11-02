using IActionResultSample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IActionResultSample.Controllers
{
    [Route("CustomResult/[action]")]
    [ApiController]
    public class CustomResultController : ControllerBase
    {
        /// <summary>
        /// Return ResponseResult, Results include any object data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ResponseBase> GetResult200()
        {
            var result = new ResponseResult
            {
                StatusCodes = StatusCodes.Status200OK.ToString(),
                Results = new object[]
                {
                    new {name = "Sherry", age = 18},
                    new {name = "Bruce", age = 20}
                }
            };

            return result;
        }

        /// <summary>
        /// Return ResponseErrorResult, Results include ProblemDetails object.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ResponseBase> GetResult500()
        {
            var result = new ResponseErrorResult
            {
                StatusCodes = StatusCodes.Status500InternalServerError.ToString(),
                Results = new object[]
                {
                    new ProblemDetails()
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
                    }
                }
            };

            return result;
        }
    }
}
