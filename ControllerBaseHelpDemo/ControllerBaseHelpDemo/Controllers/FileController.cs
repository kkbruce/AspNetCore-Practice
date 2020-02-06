using Microsoft.AspNetCore.Mvc;

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