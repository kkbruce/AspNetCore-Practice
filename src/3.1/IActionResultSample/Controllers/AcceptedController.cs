using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;

namespace IActionResultSample.Controllers
{
    [Route("Accepted/[action]")]
    [ApiController]
    public class AcceptedController : ControllerBase
    {
        /// <summary>
        /// Return HTTP 202
        /// 回傳 HTTP 202
        /// </summary>
        /// <returns>AcceptedResult</returns>
        public IActionResult Demo1()
        {
            return Accepted();
        }

        /// <summary>
        /// Return HTTP 202 and custom message object.
        /// 回傳 HTTP 202 和自訂訊息物件。
        /// </summary>
        /// <returns>AcceptedResult</returns>
        public IActionResult Demo2()
        {
            var obj = new
            {
                Message = "Accepted."
            };
            return Accepted(obj);
        }

        /// <summary>
        /// Return HTTP 202 and Location header value.
        /// 回傳 HTTP 202 和 Location 標頭與值。
        /// </summary>
        /// <returns>AcceptedResult</returns>
        public IActionResult Demo3()
        {
            var uri = new Uri("https://skilltree.my");
            return Accepted(uri);
        }

        /// <summary>
        /// Return HTTP 202 and Location header value.
        /// 回傳 HTTP 202 和 Location 標頭與值。
        /// </summary>
        /// <returns>AcceptedResult</returns>
        public IActionResult Demo4()
        {
            return Accepted("https://skilltree.my");
        }

        /// <summary>
        /// Return HTTP 202 and Location header value and custom message object.
        /// 回傳 HTTP 202 和 Location 標頭與值和自訂訊息物件。
        /// </summary>
        /// <returns>AcceptedResult</returns>
        public IActionResult Demo5()
        {
            var uri = new Uri("https://skilltree.my");
            var obj = new
            {
                Message = "Accepted."
            };
            return Accepted(uri, obj);
        }

        /// <summary>
        /// Return HTTP 202 and Location header value and custom message object.
        /// 回傳 HTTP 202 和 Location 標頭與值和自訂訊息物件。
        /// </summary>
        /// <returns>AcceptedResult</returns>
        public IActionResult Demo6()
        {
            var obj = new
            {
                Message = "Accepted."
            };
            return Accepted("https://skilltree.my", obj);
        }


        /// <summary>
        /// Return HTTP 202 and Location header value, URI use ActionName to generate.
        /// 回傳 HTTP 202 和 Location 標頭與值，URI 使用 ActionName 來產生。
        /// </summary>
        /// <returns>AcceptedAtActionResult</returns>
        public IActionResult Demo7()
        {
            return AcceptedAtAction("DemoAction");
        }

        /// <summary>
        /// Return HTTP 202 and Location header value, URI use ActionName and ControllerName to generate.
        /// 回傳 HTTP 202 和 Location 標頭與值，URI 使用 ActionName 和 ControllerName 來產生。
        /// </summary>
        /// <returns>AcceptedAtActionResult</returns>
        public IActionResult Demo8()
        {
            return AcceptedAtAction("DemoUnicode", "Content");
        }

        /// <summary>
        /// Return HTTP 202 and Location header value and custom message object, URI use ActionName to generate.
        /// 回傳 HTTP 202 和 Location 標頭與值和自訂訊息物件，URI 使用 ActionName 來產生。
        /// </summary>
        /// <returns>AcceptedAtActionResult</returns>
        public IActionResult Demo9()
        {
            var obj = new
            {
                Message = "Accepted."
            };
            return AcceptedAtAction("DemoAction", obj);
        }

        /// <summary>
        /// Return HTTP 202 and Location header value, URI use ActionName and ControllerName to generate.
        /// 回傳 HTTP 202 和 Location 標頭與值，URI 使用 ActionName 和 ControllerName 來產生。
        /// </summary>
        /// <returns>AcceptedAtActionResult</returns>
        public IActionResult Demo10()
        {
            return AcceptedAtAction("DemoAction", "Accepted", new { id = 1 });
        }

        /// <summary>
        /// Return HTTP 202 and Location header value and custom message object, URI use ActionName to generate.
        /// 回傳 HTTP 202 和 Location 標頭與值和自訂訊息物件，URI 使用 ActionName 來產生。
        /// </summary>
        /// <returns>AcceptedAtActionResult</returns>
        public IActionResult Demo11()
        {
            var obj = new
            {
                Message = "Accepted."
            };
            return AcceptedAtAction("DemoAction", new { id = 1 }, obj);
        }

        /// <summary>
        /// Return HTTP 202 and Location header value, URI use ActionName and ControllerName to generate.
        /// 回傳 HTTP 202 和 Location 標頭與值，URI 使用 ActionName 和 ControllerName 來產生。
        /// </summary>
        /// <returns>AcceptedAtActionResult</returns>
        public IActionResult Demo12()
        {
            var obj = new
            {
                Message = "Accepted."
            };
            return AcceptedAtAction("DemoAction", "Accepted", new { id = 1 }, obj);
        }

        /// <summary>
        /// Return HTTP 202 and Location header value, URI use default Route and RouteValue to generate.
        /// 回傳 HTTP 202 和 Location 標頭與值，URI 使用預設 Route 和 RouteValue 產生。
        /// </summary>
        /// <returns>AcceptedAtRouteResult</returns>
        public IActionResult Demo13()
        {
            return AcceptedAtRoute(new { id = 1 });
        }


        /// <summary>
        /// Return HTTP 202 and Location header value, URI use RouteName to generate.
        /// 回傳 HTTP 202 和 Location 標頭與值，URI 使用 RouteName 產生。
        /// </summary>
        /// <returns>AcceptedAtRouteResult</returns>
        public IActionResult Demo14()
        {
            return AcceptedAtRoute("AcceptedRoute");
        }

        /// <summary>
        /// Return HTTP 202 and Location header value, URI use RouteName and RouteValue to generate.
        /// 回傳 HTTP 202 和 Location 標頭與值，URI 使用 RouteName 和 RouteValue 產生。
        /// </summary>
        /// <returns>AcceptedAtRouteResult</returns>
        public IActionResult Demo15()
        {
            return AcceptedAtRoute("AcceptedRoute", new { id = 1 });
        }

        /// <summary>
        /// Return HTTP 202 and Location header value and custom message object, URI use RouteValue to generate.
        /// 回傳 HTTP 202 和 Location 標頭與值和自訂訊息物件，URI 使用 RouteValue 產生。
        /// </summary>
        /// <returns>AcceptedAtRouteResult</returns>
        public IActionResult Demo16()
        {
            var obj = new
            {
                Message = "Accepted."
            };
            return AcceptedAtRoute(new { id = 1 }, obj);
        }

        /// <summary>
        /// Return HTTP 202 and Location header value and custom message object, URI use RouteName and RouteValue to generate.
        /// 回傳 HTTP 202 和 Location 標頭與值和自訂訊息物件，URI 使用 RouteName 和 RouteValue 產生。
        /// </summary>
        /// <returns>AcceptedAtRouteResult</returns>
        public IActionResult Demo17()
        {
            var obj = new
            {
                Message = "Accepted."
            };
            return AcceptedAtRoute("AcceptedRoute", new { id = 1 }, obj);
        }

        /// <summary>
        /// For test generate new URI.
        /// 可測試新產生的 URI。
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>ContentResult</returns>
        public IActionResult DemoAction(int? id)
        {
            return Content($"取得Id={id}", "text/plain", Encoding.Unicode);
        }

        /// <summary>
        /// For test generate new URI.
        /// 可測試新產生的 URI。
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>ContentResult</returns>
        [Route("/Accepted/DemoRoute", Name = "AcceptedRoute")]
        public IActionResult DemoRoute(int? id)
        {
            return Content($"取得Id={id}", "text/plain", Encoding.Unicode);
        }
    }
}