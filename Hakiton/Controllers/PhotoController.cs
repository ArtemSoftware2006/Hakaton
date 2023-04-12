using Domain.ViewModel.Photo;
using Microsoft.AspNetCore.Mvc;
using System.IO;

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
                        if (!Directory.Exists(Environment.CurrentDirectory + "\\Avatars\\"))
                        {
                            Directory.CreateDirectory(Environment.CurrentDirectory + "\\Avatars\\");
                        }
                        string path = "\\Avatars\\" + UserId + ".jpeg";

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
                    string path = Environment.CurrentDirectory + "\\Avatars\\" + UserId + ".jpeg";

                    if (System.IO.File.Exists(path))
                    {
                        return Json(path);
                    }
                    return Json(path);
                    // return StatusCode(400, "У вас нет фотографии");
                }
                return StatusCode(403);
            }
            return BadRequest("Модель не валидна");
        }
        [HttpGet]
        public async Task<IActionResult> LoadAllAvatar()
        {
            if (ModelState.IsValid)
            {
                var files = new List<AvatarVM>();
                DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory + "\\Avatars");

                foreach(var item in dir.GetFiles())
                {
                    files.Add(new AvatarVM { Path = item.FullName, UserId = int.Parse(Path.GetFileNameWithoutExtension(item.Name)) });
                }
                return Json(files);
            }
            return BadRequest("Модель не валидна");
        }

    }
}
