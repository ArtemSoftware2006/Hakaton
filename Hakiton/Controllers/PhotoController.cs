using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;

namespace Hakiton.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PhotoController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> UploadAvatar(int UserId,[FromForm]IFormFile avatar)
        {
            if(ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    if (avatar != null)
                    {
                        if (!Directory.Exists(Environment.CurrentDirectory + "/Avatars/"))
                        {
                            Directory.CreateDirectory(Environment.CurrentDirectory + "/Avatars/");
                        }
                        string path = "/Avatars/" + UserId + ".jpeg";

                        using (var fileStream = new FileStream(Environment.CurrentDirectory + path, FileMode.Create))
                        {
                            await avatar.CopyToAsync(fileStream);
                        }
                        return Ok();
                    }
                    return StatusCode(400, "Вы не загрузили фото");
                }
                return StatusCode(403);
            }
            return BadRequest("Модель не валидна");
        }
        [HttpGet]
        public async Task<IActionResult> LoadAvatar(int UserId)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    try
                    {
                        string path = Environment.CurrentDirectory + "/Avatars/" + UserId + ".jpeg";
                        FileStream fileStream = new FileStream(path, FileMode.Open);
                        string contentType = "image/jpeg";
                        string downloadName = UserId + ".jpeg";
                        return File(fileStream, contentType, downloadName);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(400, "У вас нет фотографии");
                    }
                }
                return StatusCode(403);
            }
            return BadRequest("Модель не валидна");
        }


    }
}
