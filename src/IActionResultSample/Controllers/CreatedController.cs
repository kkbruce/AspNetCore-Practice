using System;
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
    }
}