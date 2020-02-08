using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IActionResultSample.Controllers
{
    [Route("Conflict/[action]")]
    [ApiController]
    public class ConflictController : ControllerBase
    {
        /// <summary>
        /// Return HTTP 409
        /// 回傳 HTTP 409
        /// </summary>
        /// <returns>ConflictResult</returns>
        public IActionResult Demo1()
        {
            return Conflict();
        }

        /// <summary>
        /// Return HTTP 409 and custom message object.
        /// 回傳 HTTP 409 與自訂訊息物件。
        /// </summary>
        /// <returns>ConflictObjectResult</returns>
        public IActionResult Demo2()
        {
            var obj = new
            {
                Error = "Conflict."
            };

            return Conflict(obj);
        }

        /// <summary>
        /// Return HTTP 409 and ModelState(MVC)
        /// 回傳 HTTP 409
        /// </summary>
        /// <returns>ConflictObjectResult</returns>
        public IActionResult Demo3()
        {
           var msd = new ModelStateDictionary();
            return Conflict(msd);
        }
    }
}