using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadSample.Controllers
{
    [Route("Upload/[action]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        /// <summary>
        /// Upload a file.
        /// 上傳一個檔案。
        /// </summary>
        /// <param name="file">upload key name.</param>
        /// <returns>HTTP 200</returns>
        [HttpPost]
        public IActionResult SingleFile(IFormFile file)
        {
            if (file.Length > 0)
            {
                return Ok($"File Length: {file.Length}");
            }

            return BadRequest("File zero length?");
        }

    }
}