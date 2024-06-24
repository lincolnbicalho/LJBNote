using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Notes.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly IConfiguration _config;

    public UploadController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("multiple/{Id}")]
    public IActionResult Multiple(IFormFile[] files, int Id)
    {
        try
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), _config.GetValue<string>("ImageUpload:Path"));
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var uniqueFileName = $"Note{Id}_{Guid.NewGuid()}_{file.FileName}";
                    var filePath = Path.Combine(uploadPath, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }

            return StatusCode(200);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
