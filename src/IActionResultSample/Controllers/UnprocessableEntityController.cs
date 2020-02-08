using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IActionResultSample.Controllers
{
    [Route("UnprocessableEntity/[action]")]
    [ApiController]
    public class UnprocessableEntityController : ControllerBase
    {
        /// <summary>
        /// Return HTTP 422
        /// 回傳 HTTP 422
        /// </summary>
        /// <returns>UnprocessableEntityResult</returns>
        public IActionResult Demo1()
        {
            return UnprocessableEntity();
        }

        /// <summary>
        /// Return HTTP 422 and custom message object.
        /// 回傳 HTTP 422 與自訂訊息物件。
        /// </summary>
        /// <returns>UnprocessableEntityObjectResult</returns>
        public IActionResult Demo2()
        {
            var obj = new
            {
                Error = "Unprocessable."
            };

            return UnprocessableEntity(obj);
        }

        /// <summary>
        /// Return HTTP 422 and ModelState (MVC).
        /// 回傳 HTTP 422 和 ModelState 物件。
        /// </summary>
        /// <returns>UnprocessableEntityObjectResult</returns>
        public IActionResult Demo3()
        {
            var msd = new ModelStateDictionary();
            return UnprocessableEntity(msd);
        }
    }
}