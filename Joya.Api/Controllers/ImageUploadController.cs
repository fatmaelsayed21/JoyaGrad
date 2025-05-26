using Microsoft.AspNetCore.Mvc;

namespace Joya.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ImageUploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var savePath = Path.Combine(_env.WebRootPath, "images", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(savePath)!); 

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

           
            return Ok(new { imageUrl = fileName });
        }
    }
}
