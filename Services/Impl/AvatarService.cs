using DAL.Interfaces;
using Domain.Entity;
using Domain.Response;
using Services.Interfaces;
using Amazon.S3;
using Amazon.S3.Model;
using Domain.ViewModel.Avatar;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

namespace Services.Impl
{
    public class AvatarService : IAvatarService
    {
        private readonly string secretKey = "YCM1cYGM00G-uoIrK8-OvI2qqsjy7Z09bbM7MZAx";
        private readonly string  accessKey = "YCAJEzoOin_R-Dc7ATcBaKPCv";
        private readonly string bucketName = "storage-artem";
        private readonly AmazonS3Config configsS3;
        private readonly ILogger<AvatarService> _logger;
        private readonly IAvatarRepository _repository;
        private readonly IMemoryCache _cache;

        public AvatarService(IAvatarRepository repository, ILogger<AvatarService> logger, IMemoryCache cache)
        {
            _repository = repository;
            _logger = logger;
            _cache = cache;

             configsS3 = new  AmazonS3Config() {
                ServiceURL="https://storage.yandexcloud.net"
            };
        }
        public async Task<BaseResponse<bool>> Create(CreateAvatarVM model)
        {
            try
            {
                var avatar = await _repository.Get(model.UserId);

                if (avatar != null)
                    await _repository.Delete(avatar);

                avatar = new Avatar() {
                    Key = Guid.NewGuid(),
                    UserId = model.UserId,
                };
                
                using (var client = new AmazonS3Client(accessKey, secretKey,configsS3))
                {

                    var request = new PutObjectRequest()
                    {
                        BucketName = bucketName,
                        Key = avatar.Key.ToString(),
                        InputStream = model.file,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    var response = await client.PutObjectAsync(request);

                    _logger.LogInformation(response.HttpStatusCode.ToString());

                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        await _repository.Create(avatar);

                        return new BaseResponse<bool>() 
                        {
                            Data = true,
                            Description= "Ok",
                            StatusCode = Domain.Enum.StatusCode.Ok,
                        };
                    }
                    
                }
                return new BaseResponse<bool>() 
                {
                    Data = false,
                    Description= "Ошибка загрузки",
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError,
                };
            }
        }

        public Task<BaseResponse<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<MemoryStream>> Get(int id)
        {
            try
            {
                _cache.TryGetValue(id, out MemoryStream? file);
                if (file != null)
                {
                    _logger.LogInformation("Сработал кэш");
                     return new BaseResponse<MemoryStream>()
                    {
                        Data = file,
                        Description = "Ok",
                        StatusCode = Domain.Enum.StatusCode.Ok,
                    };
                }
                
                var avatar = await _repository.Get(id);

                if (avatar != null)
                {
                    MemoryStream memStream = new MemoryStream();

                    using (var client = new AmazonS3Client(accessKey, secretKey,configsS3))
                    {
                        GetObjectResponse response = await client.GetObjectAsync(bucketName, avatar.Key.ToString());

                        await response.ResponseStream.CopyToAsync(memStream);

                        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                        {
                            _cache.Set(id, memStream,new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                            _logger.LogInformation("Сработала связь с облаком");

                            return new BaseResponse<MemoryStream>()
                            {
                                Data = memStream,
                                Description = "Ok",
                                StatusCode = Domain.Enum.StatusCode.Ok,
                            };
                        }

                    }
                }

                return new BaseResponse<MemoryStream>()
                {
                    Description = "Ошибка загрузки файла",
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<MemoryStream>()
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError,
                };
            }        
        }

        public Task<BaseResponse<List<Avatar>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> Update(CreateAvatarVM model)
        {
            throw new NotImplementedException();
        }
    }
}