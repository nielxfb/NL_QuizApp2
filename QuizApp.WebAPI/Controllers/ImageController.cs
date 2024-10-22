using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace QuizApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ImageController(IWebHostEnvironment env)
        {
            _env = env;
        }
        
        [HttpPost("upload-image")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!validExtensions.Contains(extension))
                return BadRequest("Invalid file type.");

            var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            var uniqueFileName = Guid.NewGuid() + extension;
            var filePath = Path.Combine(uploadsPath, uniqueFileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativePath = Path.Combine("uploads", uniqueFileName);

            return Ok(new { FilePath = relativePath });
        }

    }
}
