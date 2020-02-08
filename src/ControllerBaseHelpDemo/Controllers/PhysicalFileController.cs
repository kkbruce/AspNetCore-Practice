using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Net.Http.Headers;

namespace ControllerBaseHelpDemo.Controllers
{
    [Route("PhysicalFile/[action]")]
    [ApiController]
    public class PhysicalFileController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PhysicalFileController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// The path must be an absolute path.
        /// 路徑必須是絕對路徑。
        /// </summary>
        /// <returns>PhysicalFileResult</returns>
        public IActionResult DemoPhysicalFile1()
        {
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Files\\Sample.txt");
            return PhysicalFile(path, "text/plain");
        }

        /// <summary>
        /// The path must be an absolute path and enable range processing.
        /// 路徑必須是絕對路徑與啟用部份請求的處理。
        /// </summary>
        /// <returns>PhysicalFileResult</returns>
        public IActionResult DemoPhysicalFile2()
        {
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Files\\Sample.txt");
            return PhysicalFile(path, "text/plain", true);
        }

        /// <summary>
        /// The path must be an absolute path and set download name.
        /// 路徑必須是絕對路徑並設定下載檔案名稱。
        /// </summary>
        /// <returns>PhysicalFileResult</returns>
        public IActionResult DemoPhysicalFile3()
        {
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Files\\Sample.txt");
            return PhysicalFile(path, "text/plain", "SkilltreeNo1.txt");
        }

        /// <summary>
        /// The path must be an absolute path and set download name and enable range processing.
        /// 路徑必須是絕對路徑並設定下載檔案名稱與啟用部份請求的處理。
        /// </summary>
        /// <returns>PhysicalFileResult</returns>
        public IActionResult DemoPhysicalFile4()
        {
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Files\\Sample.txt");
            return PhysicalFile(path, "text/plain", "SkilltreeNo1.txt", true);
        }

        /// <summary>
        /// The path must be an absolute path and set "Last-Modified" and "ETag" header information.
        /// 路徑必須是絕對路徑並設定 "Last-Modified" 和 "ETag" 標頭資訊。
        /// </summary>
        /// <returns>PhysicalFileResult</returns>
        public IActionResult DemoPhysicalFile5()
        {
            var path = GetPathAndEtag(out var lastModified, out var entityTag);
            return PhysicalFile(path, "text/plain", lastModified, entityTag);
        }

        /// <summary>
        /// The path must be an absolute path and set "Last-Modified" and "ETag" header information,
        /// And enable range processing.
        /// 路徑必須是絕對路徑並設定 "Last-Modified" 和 "ETag" 標頭資訊與啟用部份請求的處理。
        /// </summary>
        /// <returns>PhysicalFileResult</returns>
        public IActionResult DemoPhysicalFile6()
        {
            var path = GetPathAndEtag(out var lastModified, out var entityTag);
            return PhysicalFile(path, "text/plain", lastModified, entityTag, true);
        }

        /// <summary>
        /// The path must be an absolute path and download name and set "Last-Modified" and "ETag" header information.
        /// 路徑必須是絕對路徑並設定下載檔案名稱與設定 "Last-Modified" 和 "ETag" 標頭資訊。
        /// </summary>
        /// <returns>PhysicalFileResult</returns>
        public IActionResult DemoPhysicalFile7()
        {
            var path = GetPathAndEtag(out var lastModified, out var entityTag);
            return PhysicalFile(path, "text/plain", "SkilltreeNo1.txt", lastModified, entityTag);
        }

        /// <summary>
        /// The path must be an absolute path and download name and set "Last-Modified" and "ETag" header information.
        /// 路徑必須是絕對路徑並設定下載檔案名稱與設定 "Last-Modified" 和 "ETag" 標頭資訊。
        /// </summary>
        /// <returns>PhysicalFileResult</returns>
        public IActionResult DemoPhysicalFile8()
        {
            var path = GetPathAndEtag(out var lastModified, out var entityTag);
            return PhysicalFile(path, "text/plain", "SkilltreeNo1.txt", lastModified, entityTag, true);
        }

        private string GetPathAndEtag(out DateTimeOffset lastModified, out EntityTagHeaderValue entityTag)
        {
            var path = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Files\\Sample.txt");
            lastModified = DateTimeOffset.Parse("2020/02/07 14:21:13 PM");
            entityTag = new EntityTagHeaderValue("\"Etag\"");
            return path;
        }
    }
}