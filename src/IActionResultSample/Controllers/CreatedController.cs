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
        /// Return HTTP 201 and include Location header value.
        /// 回傳 HTTP 201 and Location 標頭與值。
        /// </summary>
        /// <returns>CreatedAtActionResult</returns>
        public IActionResult Demo4()
        {
            return CreatedAtAction("DemoAction", new { id = 1 }, null);
        }

        /// <summary>
        /// Return HTTP 201 and include Location header value and custom message object.
        /// 回傳 HTTP 201 and Location 標頭與值和自訂訊息物件。
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
        /// Return HTTP 201 and include Location header value and custom message object.
        /// 回傳 HTTP 201 and Location 標頭與值和自訂訊息物件。。
        /// </summary>
        /// <returns>CreatedAtActionResult</returns>
        public IActionResult Demo6()
        {
            // Create other controller/action RUI for Location header.
            return CreatedAtAction("DemoUnicode", "Content", new { id = 1 }, null);
        }

        /// <summary>
        /// Return HTTP 201 and include Location header value and custom message object.
        /// 回傳 HTTP 201 and Location 標頭與自訂訊息物件。
        /// </summary>
        /// <returns>CreatedAtActionResult</returns>
        public IActionResult Demo7()
        {
            var obj = new
            {
                Message = "Created."
            };
            // Create other controller/action RUI for Location header.
            return CreatedAtAction("DemoUnicode", "Content", new { id = 1 }, obj);
        }

        /// <summary>
        /// Return HTTP 201 and include Location header value.
        /// 回傳 HTTP 201 and Location 標頭與值。
        /// </summary>
        /// <returns>CreatedAtRouteResult</returns>
        public IActionResult Demo8()
        {
            return CreatedAtRoute("SampleRoute", null);
        }

        /// <summary>
        /// Return HTTP 201 and include Location header value.
        /// 回傳 HTTP 201 and Location 標頭與值。
        /// </summary>
        /// <returns>CreatedAtRouteResult</returns>
        public IActionResult Demo9()
        {
            var obj = new
            {
                Message = "Created."
            };
            return CreatedAtRoute("SampleRoute", obj);
        }

        /// <summary>
        /// Return HTTP 201 and include Location header value.
        /// 回傳 HTTP 201 and Location 標頭與值。
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
        /// Return HTTP 201 and include Location header value.
        /// 回傳 HTTP 201 and Location 標頭與值。
        /// </summary>
        /// <returns>CreatedAtRouteResult</returns>
        public IActionResult Demo11()
        {
            var obj = new
            {
                Message = "Created."
            };
            return CreatedAtRoute("SampleRoute", new { id = 1 }, obj);
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
        [Route("/DemoRoute", Name = "SampleRoute")]
        public IActionResult DemoRoute(int? id)
        {
            return Content($"取得Id={id}", "text/plain", Encoding.Unicode);
        }
    }
}