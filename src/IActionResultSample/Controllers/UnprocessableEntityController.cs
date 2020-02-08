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
        /// </summary>
        /// <returns>UnprocessableEntityResult</returns>
        public IActionResult Demo1()
        {
            return UnprocessableEntity();
        }

        /// <summary>
        /// Return HTTP 422 and custom message object.
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
        /// Return HTTP 422 and ModelState (with MVC)
        /// </summary>
        /// <returns>UnprocessableEntityObjectResult</returns>
        public IActionResult Demo3()
        {
            var msd = new ModelStateDictionary();
            return UnprocessableEntity(msd);
        }
    }
}