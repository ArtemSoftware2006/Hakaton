using Domain.ViewModel.Photo;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Amazon.S3;
using Amazon.S3.Model;

namespace Hakiton.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PhotoController : Controller
    {
        private readonly ILogger<PhotoController> _logger;
        private readonly string secretKey = "YCM1cYGM00G-uoIrK8-OvI2qqsjy7Z09bbM7MZAx";
        private readonly string  accessKey = "YCAJEzoOin_R-Dc7ATcBaKPCv";
        private readonly string bucketName = "storage-artem";
        private readonly AmazonS3Config configsS3;
        
        public PhotoController(ILogger<PhotoController> logger)
        {
            _logger = logger;
             configsS3 = new  AmazonS3Config() {
                ServiceURL="https://storage.yandexcloud.net"
            };
        }
        [HttpPost]
        public async Task<IActionResult> UploadAvatar(int UserId,[FromForm]IFormFile avatar)
        {
            if(ModelState.IsValid)
            {

                if (HttpContext.User.Identity.IsAuthenticated)
                {

                    if (avatar != null)
                    {
                        using (var client = new AmazonS3Client(accessKey, secretKey,configsS3))
                        {
                            using (var memStream = new MemoryStream())
                            {
                                await avatar.CopyToAsync(memStream);
                                var request = new PutObjectRequest()
                                {
                                    BucketName = bucketName,
                                    Key = Guid.NewGuid().ToString(),
                                    InputStream = memStream,
                                    CannedACL = S3CannedACL.PublicRead
                                };
                                var response = await client.PutObjectAsync(request);
                                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    _logger.LogInformation($"Файл успешно отправлен {avatar.FileName}, {request.Key})");
                                }
                                else
                                {
                                    _logger.LogError($"Ошибка загрузки файла ({avatar.FileName})");
                                }
                            }
                            
                        }


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
                using (var client = new AmazonS3Client(accessKey, secretKey,configsS3))
                {
                    var list = await client.ListBucketsAsync();

                    foreach (var item in list.Buckets)
                    {
                        _logger.LogInformation( "Busket : " + item.BucketName);                        
                    }
                }

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    string path = Environment.CurrentDirectory + "/Avatars/" + UserId + ".jpeg";

                    if (System.IO.File.Exists(path))
                    {
                        byte [] image =  System.IO.File.ReadAllBytes(path); 
                        return File(image,"image/jpeg");
                    }
                    return StatusCode(400, "У вас нет фотографии");
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
                DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory + "/Avatars");

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
