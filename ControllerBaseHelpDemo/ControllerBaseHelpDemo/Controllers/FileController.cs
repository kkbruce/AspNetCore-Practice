using Microsoft.AspNetCore.Mvc;

namespace ControllerBaseHelpDemo.Controllers
{
    [Route("File/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// (string, string) It will look for the "wwwroot" directory
        /// (string, string) 會到 wwwroot 目錄尋找檔案
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult DemoFile1()
        {
            return File("files\\TaiwanNo1.txt", "application/octet-stream");
        }

        /// <summary>
        /// (string, string) It will look for the "wwwroot" directory and set download name.
        /// (string, string) 會到 wwwroot 目錄尋找檔案，並設定下載檔案名稱
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult DemoFile2()
        {
            return File("files\\TaiwanNo1.txt", "application/octet-stream", "SkilltreeNo1.txt");
        }
    }
}