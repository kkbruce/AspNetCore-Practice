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
        /// Return HTTP 400 and ModelState( with MVC)
        /// </summary>
        /// <returns>BadRequestObjectResult</returns>
        public IActionResult Demo3()
        {
            var msd = new ModelStateDictionary();
            return BadRequest(msd);
        }
    }
}