using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace ControllerBaseHelpDemo.Controllers
{
    [Route("File/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// Provide IWebHostEnvironment Instance.
        /// </summary>
        /// <param name="environment"></param>
        public FileController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        /// <summary>
        /// Return byte[] data.
        /// 回傳 byte[] 資料。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult DemoFile1()
        {
            var wwwroot = _environment.WebRootPath;
            var fileContents = System.IO.File.ReadAllBytes($"{wwwroot}/files/Sample.pdf");
            return File(fileContents, "application/pdf");
        }

        /// <summary>
        /// Return byte[] data and set download name.
        /// 回傳 byte[] 資料，並設定下載檔案名稱。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult DemoFile2()
        {
            var wwwroot = _environment.WebRootPath;
            var fileContents = System.IO.File.ReadAllBytes($"{wwwroot}/files/Sample.pdf");
            return File(fileContents, "application/pdf", "ASPNetCoreNo1.pdf");
        }

        /// <summary>
        /// Return byte[] data and set "Last-Modified" and "ETag" header information.
        /// 回傳 byte[] 資料，並設定 "Last-Modified" 和 "ETag" 標頭資訊。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult DemoFile3()
        {
            var wwwroot = _environment.WebRootPath;
            var fileContents = System.IO.File.ReadAllBytes($"{wwwroot}/files/Sample.pdf");
            var lastModified = DateTimeOffset.Parse("2020/02/07 14:21:13 PM");
            var entityTag = new EntityTagHeaderValue("\"Etag\"");
            return File(fileContents, "application/pdf", lastModified, entityTag);
        }

        /// <summary>
        /// VirtualFileResult it will look for the "wwwroot" directory.
        /// VirtualFileResult 會到 wwwroot 目錄尋找檔案。
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult DemoFile98()
        {
            // Don't need use IWebHostEnvironment Instance.
            return File("files\\TaiwanNo1.txt", "application/octet-stream");
        }

        /// <summary>
        /// VirtualFileResult it will look for the "wwwroot" directory and set download name.
        /// VirtualFileResult 會到 wwwroot 目錄尋找檔案，並設定下載檔案名稱。
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult DemoFile99()
        {
            return File("files\\TaiwanNo1.txt", "application/octet-stream", "SkilltreeNo1.txt");
        }
    }
}