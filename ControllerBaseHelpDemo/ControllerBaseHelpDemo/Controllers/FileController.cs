using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ControllerBaseHelpDemo.Controllers
{
    [Route("File/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// It will look for the "wwwroot" directory
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult DemoFile1()
        {
            return File("files\\TaiwanNo1.txt", "application/octet-stream");
        }

        public IActionResult DemoFile2()
        {
            return File("files\\TaiwanNo1.txt", "application/octet-stream", "SkilltreeNo1.txt");
        }
    }
}