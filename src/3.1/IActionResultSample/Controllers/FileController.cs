using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace IActionResultSample.Controllers
{
    [Route("File/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        /// <summary>
        /// Provide IWebHostEnvironment Instance.
        /// </summary>
        /// <param name="hostingEnvironment">IWebHostEnvironment instance</param>
        public FileController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Return byte[] data.
        /// 回傳 byte[] 資料。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult Demo1()
        {
            var fileContents = GetFileByte();
            return File(fileContents, "application/pdf");
        }

        /// <summary>
        /// Return byte[] data and enable range processing.
        /// 回傳 byte[] 資料與啟用部份請求的處理(partial requests)。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult Demo2()
        {
            var fileContents = GetFileByte();
            return File(fileContents, "application/pdf", true);
        }

        /// <summary>
        /// Return byte[] data and set download name.
        /// 回傳 byte[] 資料並設定下載檔案名稱。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult Demo3()
        {
            var fileContents = GetFileByte();
            return File(fileContents, "application/pdf", "ASPNetCoreNo1.pdf");
        }

        /// <summary>
        /// Return byte[] data and set download name and enable range processing.
        /// 回傳 byte[] 資料並設定下載檔案名稱與啟用部份請求的處理(partial requests)。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult Demo4()
        {
            var fileContents = GetFileByte();
            return File(fileContents, "application/pdf", "ASPNetCoreNo1.pdf", true);
        }

        /// <summary>
        /// Return byte[] data and set "Last-Modified" and "ETag" header information.
        /// 回傳 byte[] 資料並設定 "Last-Modified" 和 "ETag" 標頭資訊。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult Demo5()
        {
            var fileContents = GetFileByteWithEtag(out var wwwroot, out var lastModified, out var entityTag);
            return File(fileContents, "application/pdf", lastModified, entityTag);
        }

        /// <summary>
        /// Return byte[] data and set "Last-Modified" and "ETag" header information and enable range processing.
        /// 回傳 byte[] 資料並設定 "Last-Modified" 和 "ETag" 標頭資訊與啟用部份請求的處理(partial requests)。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult Demo6()
        {
            var fileContents = GetFileByteWithEtag(out var wwwroot, out var lastModified, out var entityTag);
            return File(fileContents, "application/pdf", lastModified, entityTag, true);
        }

        /// <summary>
        /// Return byte[] data and set "Last-Modified" and "ETag" header information and set download name.
        /// 回傳 byte[] 資料並設定 "Last-Modified" 和 "ETag" 標頭資訊，並設定下載檔案名稱。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult Demo7()
        {
            var fileContents = GetFileByteWithEtag(out var wwwroot, out var lastModified, out var entityTag);
            return File(fileContents, "application/pdf", "ASPNetCoreNo1.pdf", lastModified, entityTag);
        }

        /// <summary>
        /// Return byte[] data and set "Last-Modified" and "ETag" header information and set download name and enable range processing.
        /// 回傳 byte[] 資料並設定 "Last-Modified" 和 "ETag" 標頭資訊，並設定下載檔案名稱與啟用部份請求的處理(partial requests)。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult Demo8()
        {
            var fileContents = GetFileByteWithEtag(out var wwwroot, out var lastModified, out var entityTag);
            return File(fileContents, "application/pdf", "ASPNetCoreNo1.pdf", lastModified, entityTag, true);
        }

        private byte[] GetFileByte()
        {
            var wwwroot = _hostingEnvironment.WebRootPath;
            var fileContents = System.IO.File.ReadAllBytes($"{wwwroot}/Files/Sample.pdf");
            return fileContents;
        }

        private byte[] GetFileByteWithEtag(out string wwwroot, out DateTimeOffset lastModified, out EntityTagHeaderValue entityTag)
        {
            wwwroot = _hostingEnvironment.WebRootPath;
            var fileContents = System.IO.File.ReadAllBytes($"{wwwroot}/Files/Sample.pdf");
            lastModified = DateTimeOffset.Parse("2020/02/07 14:21:13 PM");
            entityTag = new EntityTagHeaderValue("\"Etag\"");
            return fileContents;
        }

        /// <summary>
        /// Return Stream data.
        /// 回傳 Stream 型別資料。
        /// </summary>
        /// <returns>FileStreamResult</returns>
        public IActionResult Demo9()
        {
            var fileStream = GetFileStream();

            return File(fileStream, "application/pdf");
        }

        /// <summary>
        /// Return Stream data and enable range processing.
        /// 回傳 Stream 型別資料與啟用部份請求的處理(partial requests)。
        /// </summary>
        /// <returns>FileStreamResult</returns>
        public IActionResult Demo10()
        {
            var fileStream = GetFileStream();

            return File(fileStream, "application/pdf", true);
        }

        /// <summary>
        /// Return Stream data and set download file.
        /// 回傳 Stream 型別資料並設定下載檔案名稱。
        /// </summary>
        /// <returns>FileStreamResult</returns>
        public IActionResult Demo11()
        {
            var fileStream = GetFileStream();
            return File(fileStream, "application/pdf", "ASPNetCoreNo1.pdf");
        }

        /// <summary>
        /// Return Stream data and set download file and enable range processing.
        /// 回傳 Stream 型別資料並設定下載檔案名稱與啟用部份請求的處理(partial requests)。
        /// </summary>
        /// <returns>FileStreamResult</returns>
        public IActionResult Demo12()
        {
            var fileStream = GetFileStream();
            return File(fileStream, "application/pdf", "ASPNetCoreNo1.pdf", true);
        }

        /// <summary>
        /// Return Stream data and set "Last-Modified" and "ETag" header information.
        /// 回傳 Stream 型別資料並設定 "Last-Modified" 和 "ETag" 標頭資訊。
        /// </summary>
        /// <returns>FileStreamResult</returns>
        public IActionResult Demo13()
        {
            var fileStream = GetFileStreamWithEtag(out var lastModified, out var entityTag);
            return File(fileStream, "application/pdf", lastModified, entityTag);
        }

        /// <summary>
        /// Return Stream data and set "Last-Modified" and "ETag" header information and enable range processing.
        /// 回傳 Stream 型別資料並設定 "Last-Modified" 和 "ETag" 標頭資訊與啟用部份請求的處理(partial requests)。
        /// </summary>
        /// <returns>FileStreamResult</returns>
        public IActionResult Demo14()
        {
            var fileStream = GetFileStreamWithEtag(out var lastModified, out var entityTag);
            return File(fileStream, "application/pdf", lastModified, entityTag, true);
        }

        /// <summary>
        /// Return Stream data and set "Last-Modified" and "ETag" header information and set download name.
        /// 回傳 Stream 型別資料並設定 "Last-Modified" 和 "ETag" 標頭資訊，設定下載檔案名稱。
        /// </summary>
        /// <returns>FileStreamResult</returns>
        public IActionResult Demo15()
        {
            var fileStream = GetFileStreamWithEtag(out var lastModified, out var entityTag);
            return File(fileStream, "application/pdf", "ASPNetCoreNo1.pdf", lastModified, entityTag);
        }

        /// <summary>
        /// Return Stream data and set "Last-Modified" and "ETag" header information and set download name and enable range processing.
        /// 回傳 Stream 型別資料並設定 "Last-Modified" 和 "ETag" 標頭資訊，設定下載檔案名稱與啟用部份請求的處理(partial requests)。
        /// </summary>
        /// <returns>FileStreamResult</returns>
        public IActionResult Demo16()
        {
            var fileStream = GetFileStreamWithEtag(out var lastModified, out var entityTag);
            return File(fileStream, "application/pdf", "ASPNetCoreNo1.pdf", lastModified, entityTag, true);
        }

        private FileStream GetFileStream()
        {
            var wwwroot = _hostingEnvironment.WebRootPath;
            var fileStream = new FileStream($"{wwwroot}/Files/Sample.pdf", FileMode.Open);
            return fileStream;
        }

        private FileStream GetFileStreamWithEtag(out DateTimeOffset lastModified, out EntityTagHeaderValue entityTag)
        {
            var wwwroot = _hostingEnvironment.WebRootPath;
            var fileStream = new FileStream($"{wwwroot}/Files/Sample.pdf", FileMode.Open);
            lastModified = DateTimeOffset.Parse("2020/02/07 14:21:13 PM");
            entityTag = new EntityTagHeaderValue("\"Etag\"");
            return fileStream;
        }

        /// <summary>
        /// VirtualFileResult it will look for the "wwwroot" directory.
        /// VirtualFileResult 會到 wwwroot 目錄尋找檔案。
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult Demo17()
        {
            // Don't need use IWebHostEnvironment Instance.
            return File("Files\\Sample.txt", "application/octet-stream");
        }

        /// <summary>
        /// VirtualFileResult will look for the "wwwroot" directory and enable range processing.
        /// VirtualFileResult 會到 wwwroot 目錄尋找檔案與啟用部份請求的處理(partial requests)。
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult Demo18()
        {
            // Don't need use IWebHostEnvironment Instance.
            return File("Files\\Sample.txt", "application/octet-stream", true);
        }

        /// <summary>
        /// VirtualFileResult will look for the "wwwroot" directory and set download name.
        /// VirtualFileResult 會到 wwwroot 目錄尋找檔案並設定下載檔案名稱。
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult Demo19()
        {
            return File("Files\\Sample.txt", "application/octet-stream", "SkilltreeNo1.txt");
        }

        /// <summary>
        /// VirtualFileResult will look for the "wwwroot" directory and set download name and enable range processing.
        /// VirtualFileResult 會到 wwwroot 目錄尋找檔案並設定下載檔案名稱與啟用部份請求的處理(partial requests)。
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult Demo20()
        {
            return File("Files\\Sample.txt", "application/octet-stream", "SkilltreeNo1.txt", true);
        }

        /// <summary>
        /// VirtualFileResult will look for the "wwwroot" directory and set "Last-Modified" and "ETag" header information.
        /// VirtualFileResult 會到 wwwroot 目錄尋找檔案並設定 "Last-Modified" 和 "ETag" 標頭資訊。
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult Demo21()
        {
            GetEtagData(out var lastModified, out var entityTag);
            return File("Files\\Sample.txt", "application/octet-stream", lastModified, entityTag);
        }

        /// <summary>
        /// VirtualFileResult will look for the "wwwroot" directory and set "Last-Modified" and "ETag" header information,
        /// and enable range processing.
        /// VirtualFileResult 會到 wwwroot 目錄尋找檔案並設定 "Last-Modified" 和 "ETag" 標頭資訊與啟用部份請求的處理(partial requests)。
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult Demo22()
        {
            GetEtagData(out var lastModified, out var entityTag);
            return File("Files\\Sample.txt", "application/octet-stream", lastModified, entityTag, true);
        }

        /// <summary>
        /// VirtualFileResult will look for the "wwwroot" directory and set download name.
        /// VirtualFileResult 會到 wwwroot 目錄尋找檔案並設定下載檔案名稱。
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult Demo23()
        {
            return File("Files\\Sample.txt", "application/octet-stream", "SkilltreeNo1.txt");
        }

        /// <summary>
        /// VirtualFileResult will look for the "wwwroot" directory and set download name and enable range processing.
        /// VirtualFileResult 會到 wwwroot 目錄尋找檔案並設定下載檔案名稱與啟用部份請求的處理(partial requests)。
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult Demo24()
        {
            return File("Files\\Sample.txt", "application/octet-stream", "SkilltreeNo1.txt", true);
        }

        private void GetEtagData(out DateTimeOffset lastModified, out EntityTagHeaderValue entityTag)
        {
            lastModified = DateTimeOffset.Parse("2020/02/07 14:21:13 PM");
            entityTag = new EntityTagHeaderValue("\"Etag\"");
        }
    }
}