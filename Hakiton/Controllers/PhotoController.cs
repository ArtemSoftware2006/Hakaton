using Domain.ViewModel.Photo;
using Microsoft.AspNetCore.Mvc;
using Domain.ViewModel.Avatar;
using Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Hakiton.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PhotoController : Controller
    {
        private readonly ILogger<PhotoController> _logger;

        private readonly IAvatarService _service;

        public PhotoController(ILogger<PhotoController> logger, IAvatarService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadAvatar(int UserId, [FromForm] IFormFile avatar)
        {
            if (ModelState.IsValid)
            {
                if (avatar == null)
                {
                    return StatusCode(400, "Вы не загрузили фото");
                }

                var model = new CreateAvatarViewModel() { UserId = UserId };

                await avatar.CopyToAsync(model.file);

                var response = await _service.Create(model);

                _logger.LogInformation(response.Description);

                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                {
                    return Ok();
                }

                return StatusCode(400, response.Data);
            }
            return BadRequest("Модель не валидна");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LoadAvatar(int UserId)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.Get(UserId);

                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                {
                    _logger.LogInformation($"Response : {response.Data.Length}");

                    return File(response.Data.ToArray(), "image/jpeg");
                }

                return StatusCode(400, "У вас нет фотографии");
            }
            return BadRequest("Модель не валидна");
        }

        [HttpGet]
        public async Task<IActionResult> LoadAllAvatar()
        {
            if (ModelState.IsValid)
            {
                var files = new List<AvatarViewModel>();
                DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory + "/Avatars");

                foreach (var item in dir.GetFiles())
                {
                    files.Add(
                        new AvatarViewModel
                        {
                            Path = item.FullName,
                            UserId = int.Parse(Path.GetFileNameWithoutExtension(item.Name))
                        }
                    );
                }
                return Json(files);
            }
            return BadRequest("Модель не валидна");
        }
    }
}
