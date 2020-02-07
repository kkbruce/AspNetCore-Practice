using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

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
    }
}