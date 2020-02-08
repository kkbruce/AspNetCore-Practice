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
    }
}