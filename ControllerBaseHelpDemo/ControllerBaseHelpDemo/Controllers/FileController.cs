using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ControllerBaseHelpDemo.Controllers
{
    [Route("File/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public FileController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        /// <summary>
        /// (byte[], string) return byte[] data.
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult DemoFile1()
        {
            // Need injection IWebHostEnvironment to wwwroot path.
            var wwwroot = _environment.WebRootPath;
            var sameple = System.IO.File.ReadAllBytes($"{wwwroot}/files/Sample.pdf");
            return File(sameple, "application/pdf");
        }

        /// <summary>
        /// (string, string) It will look for the "wwwroot" directory
        /// (string, string) 會到 wwwroot 目錄尋找檔案
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult DemoFile2()
        {
            return File("files\\TaiwanNo1.txt", "application/octet-stream");
        }

        /// <summary>
        /// (string, string) It will look for the "wwwroot" directory and set download name.
        /// (string, string) 會到 wwwroot 目錄尋找檔案，並設定下載檔案名稱
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult DemoFile3()
        {
            return File("files\\TaiwanNo1.txt", "application/octet-stream", "SkilltreeNo1.txt");
        }
    }
}