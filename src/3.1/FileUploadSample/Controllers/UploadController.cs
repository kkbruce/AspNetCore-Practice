using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadSample.Controllers
{
    [Route("Upload/[action]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UploadController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
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

        /// <summary>
        /// Upload a file.
        /// 上傳一個檔案。
        /// </summary>
        /// <param name="file">upload key name.</param>
        /// <returns>HTTP 200</returns>
        [HttpPost]
        public IActionResult SingleFileForm([FromForm]IFormFile file)
        {
            if (file.Length > 0)
            {
                return Ok($"File Length: {file.Length}");
            }

            return BadRequest("File zero length?");
        }

        /// <summary>
        /// 上傳一個檔案並儲存至Upload資料夾下。
        /// </summary>
        /// <param name="file">upload key name.</param>
        /// <returns>HTTP 200</returns>
        [HttpPost]
        public async Task<IActionResult> SingleFileSaveDisk([FromForm]IFormFile file)
        {
            if (file.Length > 0)
            {
                var filePath = Path.Combine(_hostingEnvironment.ContentRootPath,
                                                    "Upload",
                                                    Path.GetRandomFileName());
                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
                return Ok($"Uploaded: {filePath}");
            }

            return BadRequest("File zero length?");
        }
    }
}