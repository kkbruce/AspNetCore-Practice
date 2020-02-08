using System;
using Microsoft.AspNetCore.Mvc;

namespace IActionResultSample.Controllers
{
    [Route("Ok/[action]")]
    [ApiController]
    public class OkController : ControllerBase
    {
        /// <summary>
        /// Return HTTP 200
        /// 回傳 HTTP 200
        /// </summary>
        /// <returns>OkResult</returns>
        public IActionResult Demo1()
        {
            return Ok();
        }

        /// <summary>
        /// Reuth HTTP 200 with Object
        /// </summary>
        /// <returns>OkObjectResult</returns>
        public IActionResult Demo2()
        {
            return Ok(new
            {
                Name = "Skilltree",
                Event = "ASP.NET Core Web API",
                URL = new Uri("https://skilltree.my")
            });
        }
    }
}