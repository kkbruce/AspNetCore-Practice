using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace IActionResultSample.Controllers
{
    [Route("Created/[action]")]
    [ApiController]
    public class CreatedController : ControllerBase
    {
        /// <summary>
        /// Return HTTP 201 and Location header.
        /// 回傳 HTTP 201 and Location 標頭。
        /// </summary>
        /// <returns>CreatedResult</returns>
        public IActionResult Demo1()
        {
            var uri = "http://skilltree.my/";
            return Created(uri, null);
        }

        /// <summary>
        /// Return HTTP 201 and Location header.
        /// 回傳 HTTP 201 and Location 標頭。
        /// </summary>
        /// <returns>CreatedResult</returns>
        public IActionResult Demo2()
        {
            var uri = new Uri("http://skilltree.my/");
            return Created(uri, null);
        }

        /// <summary>
        /// Return HTTP 201 and Location header and custom message object.
        /// 回傳 HTTP 201 and Location 標頭自訂訊息物件。
        /// </summary>
        /// <returns>CreatedResult</returns>
        public IActionResult Demo3()
        {
            var uri = "http://skilltree.my/";
            var obj = new
            {
                Message = "Created."
            };
            return Created(uri, obj);
        }

        /// Return HTTP 201 and include Location header value.
        /// 回傳 HTTP 201 and Location 標頭的 URI 值。
        /// </summary>
        /// <returns>CreatedAtActionResult</returns>
        public IActionResult Demo4()
        {
            return CreatedAtAction("DemoAction", new { Id = 1 }, null);
        }

        /// <summary>
        /// Return HTTP 201 and include Location header value and object value to format in the entity body
        /// 回傳 HTTP 201 and Location 標頭自訂物件。
        /// </summary>
        /// <returns>CreatedAtActionResult</returns>
        public IActionResult Demo5()
        {
            var obj = new
            {
                Name = "Skilltree",
                Event = "ASP.NET Core Web API",
                URL = new Uri("https://skilltree.my")
            };
            return CreatedAtAction("DemoAction", new { Id = 1 }, obj);
        }

        /// <summary>
        /// Get Location header RUI data.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>ContentResult</returns>
        public IActionResult DemoAction(int Id)
        {
            return Content($"取得Id={Id}", "text/plain", Encoding.Unicode);
        }
}
}