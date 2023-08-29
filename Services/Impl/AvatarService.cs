using DAL.Interfaces;
using Domain.Entity;
using Domain.Response;
using Services.Interfaces;
using Amazon.S3;
using Amazon.S3.Model;
using Domain.ViewModel.Avatar;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Formats.Tar;
using Microsoft.AspNetCore.Hosting;

namespace Services.Impl
{
    public class AvatarService : IAvatarService
    {
        private readonly AmazonS3Config configsS3;
        private readonly AvatarProvider avatarProvider;
        private readonly ILogger<AvatarService> _logger;
        private readonly IAvatarRepository _repository;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _config;

        public AvatarService(IAvatarRepository repository, ILogger<AvatarService> logger, IMemoryCache cache, IConfiguration config)
        {
            _config = config;

            _repository = repository;
            _logger = logger;
            _cache = cache;
            avatarProvider = new AvatarProvider();

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

                await avatarProvider.SaveAvatarAsync(model.file, avatar.Key.ToString());

                _logger.LogInformation("Сохранён файл " + avatar.Key.ToString());
                
                await _repository.Create(avatar);

                _cache.Remove(model.UserId);
                _cache.Set(model.UserId, model.file.ToArray(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));

                return new BaseResponse<bool>() 
                {
                    Data = true,
                    Description= "Ok",
                    StatusCode = Domain.Enum.StatusCode.Ok,
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

        public async Task<BaseResponse<byte[]>> Get(int id)
        {
            try
            {
                _cache.TryGetValue(id, out byte[]? file);
                if (file != null)
                {
                    _logger.LogInformation("Сработал кэш");
                     return new BaseResponse< byte[]>()
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

                    _cache.Set(id, memStream, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                    _logger.LogInformation("Сработала связь с облаком");

                    return new BaseResponse< byte[]>()
                    {
                        Data = await avatarProvider.LoadAvatarAsync(avatar.Key.ToString()),
                        Description = "Ok",
                        StatusCode = Domain.Enum.StatusCode.Ok,
                    };
                }

                return new BaseResponse< byte[]>()
                {
                    Description = "Ошибка загрузки файла",
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse< byte[]>()
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