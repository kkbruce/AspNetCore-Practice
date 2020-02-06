using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Text;

namespace ControllerBaseHelpDemo.Controllers
{
    [Route("Content/[action]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        /// <summary>
        /// Return HTTP 200 with Content
        /// 回傳 HTTP 200 與內容
        /// </summary>
        /// <returns>ContentResult</returns>
        public IActionResult DemoText()
        {
            return Content("This is Test.");
        }

        /// <summary>
        /// Return HTTP 200 with JSON Content
        /// 回傳 HTTP 200 與 JSON 內容
        /// </summary>
        /// <returns>ContentResult</returns>
        public IActionResult DemoJson()
        {
            var obj = new
            {
                Name = "Skilltree",
                Event = "ASP.NET Core Web API",
                Time = new DateTime(2020, 5, 23),
                URL = new Uri("https://skilltree.my")
            };
            string output = JsonConvert.SerializeObject(obj);
            var header = new MediaTypeHeaderValue("application/json");
            return Content(output, header);
        }

        /// <summary>
        /// Use Browser try it.
        /// 使用瀏灠器測試。
        /// </summary>
        /// <returns>ContentResult</returns>
        public IActionResult DemoPlain()
        {
            return Content("こんにちは", "text/plain");
        }

        /// <summary>
        /// Use Browser try it.
        /// 使用瀏灠器測試。
        /// </summary>
        /// <returns>ContentResult</returns>
        public IActionResult DemoUnicode()
        {
            return Content("こんにちは", "text/plain", Encoding.Unicode);
        }

        /// <summary>
        /// Return HTTP 204
        /// 回傳 HTTP 204
        /// </summary>
        /// <returns>NoContentResult</returns>
        public IActionResult DemoNoContent()
        {
            return NoContent();
        }
    }
}