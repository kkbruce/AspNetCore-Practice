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
        /// Return HTTP 201 and Location header value.
        /// 回傳 HTTP 201 and Location 標頭與值。
        /// </summary>
        /// <returns>CreatedResult</returns>
        public IActionResult Demo1()
        {
            var uri = "http://skilltree.my/";
            return Created(uri, null);
        }

        /// <summary>
        /// Return HTTP 201 and Location header value.
        /// 回傳 HTTP 201 and Location 標頭與值。
        /// </summary>
        /// <returns>CreatedResult</returns>
        public IActionResult Demo2()
        {
            var uri = new Uri("http://skilltree.my/");
            return Created(uri, null);
        }

        /// <summary>
        /// Return HTTP 201 and Location header value and custom message object.
        /// 回傳 HTTP 201 and Location 標頭與值和自訂訊息物件。
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

        /// <summary>
        /// Return HTTP 201 and Location header value, URI use ActionName to generate.
        /// 回傳 HTTP 201 and Location 標頭與值，URI 使用 ActionName 來產生。
        /// </summary>
        /// <returns>CreatedAtActionResult</returns>
        public IActionResult Demo4()
        {
            return CreatedAtAction("DemoAction", new { id = 1 }, null);
        }

        /// <summary>
        /// Return HTTP 201 and Location header value, URI use ActionName to generate.
        /// 回傳 HTTP 201 and Location 標頭與值和自訂訊息物件，URI 使用 ActionName 來產生。
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
            return CreatedAtAction("DemoAction", new { id = 1 }, obj);
        }

        /// <summary>
        /// Return HTTP 201 and Location header value, URI use ActionName and ControllerName to generate.
        /// 回傳 HTTP 201 and Location 標頭與值，URI 使用 ActionName 和 ControllerName 來產生。
        /// </summary>
        /// <returns>CreatedAtActionResult</returns>
        public IActionResult Demo6()
        {
            return CreatedAtAction("DemoUnicode", "Content", new { id = 1 }, null);
        }

        /// <summary>
        /// Return HTTP 201 and Location header value and custom message object, URI use ActionName and ControllerName to generate.
        /// 回傳 HTTP 201 and Location 標頭與自訂訊息物件，URI 使用 ActionName 和 ControllerName 來產生。
        /// </summary>
        /// <returns>CreatedAtActionResult</returns>
        public IActionResult Demo7()
        {
            var obj = new
            {
                Message = "Created."
            };
            return CreatedAtAction("DemoUnicode", "Content", new { id = 1 }, obj);
        }

        /// <summary>
        /// Return HTTP 201 and Location header value, URI use RouteName to generate.
        /// 回傳 HTTP 201 and Location 標頭與值，URI 使用 RouteName 來產生。
        /// </summary>
        /// <returns>CreatedAtRouteResult</returns>
        public IActionResult Demo8()
        {
            return CreatedAtRoute("CreatedRoute", null);
        }

        /// <summary>
        /// Return HTTP 201 and Location header value and custom message object, URI use RouteName to generate.
        /// 回傳 HTTP 201 and Location 標頭與值與自訂訊息物件，URI 使用 RouteName 來產生。
        /// </summary>
        /// <returns>CreatedAtRouteResult</returns>
        public IActionResult Demo9()
        {
            var obj = new
            {
                Message = "Created."
            };
            return CreatedAtRoute("CreatedRoute", obj);
        }

        /// <summary>
        /// Return HTTP 201 and Location header value and custom message object, URI use default Route to generate.
        /// 回傳 HTTP 201 and Location 標頭與值與自訂訊息物件，URI 使用預設 Route 來產生。
        /// </summary>
        /// <returns>CreatedAtRouteResult</returns>
        public IActionResult Demo10()
        {
            var obj = new
            {
                Message = "Created."
            };
            return CreatedAtRoute(new { id = 1 }, obj);
        }

        /// <summary>
        /// Return HTTP 201 and Location header value and custom message object, URI use RouteName to generate.
        /// 回傳 HTTP 201 and Location 標頭與值與自訂訊息物件，URI 使用 RouteName 來產生。
        /// </summary>
        /// <returns>CreatedAtRouteResult</returns>
        public IActionResult Demo11()
        {
            var obj = new
            {
                Message = "Created."
            };
            return CreatedAtRoute("CreatedRoute", new { id = 1 }, obj);
        }

        /// <summary>
        /// For test generate new URI.
        /// 可測試新產生的 URI。
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>ContentResult</returns>
        public IActionResult DemoAction(int id)
        {
            return Content($"取得Id={id}", "text/plain", Encoding.Unicode);
        }

        /// <summary>
        /// For test generate new URI.
        /// 可測試新產生的 URI。
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>ContentResult</returns>
        [Route("/Created/DemoRoute", Name = "CreatedRoute")]
        public IActionResult DemoRoute(int? id)
        {
            return Content($"取得Id={id}", "text/plain", Encoding.Unicode);
        }
    }
}