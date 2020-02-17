using Microsoft.AspNetCore.Http;

namespace FileUploadSample.Models
{
    public class UserProfile
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public IFormFile Avator { get; set; }
    }
}
