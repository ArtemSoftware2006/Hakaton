using Amazon.Runtime.Internal.Util;
using DAL.Interfaces;
using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Impl
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly ILogger<CommentsService> _logger;
        public CommentsService(ICommentsRepository commentsRepository, ILogger<CommentsService> logger)
        {
            _logger = logger;
            _commentsRepository = commentsRepository;
        }
        public async Task<BaseResponse<bool>> Create(CommentsVm model)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(model.Text))
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Text is empty",
                        StatusCode = Domain.Enum.StatusCode.NotFound,
                        Data = false
                    };
                }

                var comment = new Comments()
                {
                    Text = model.Text,
                    TimeCreated = DateTime.UtcNow,
                    DealId = model.DealId,
                    UserId = model.UserId,
                };

                bool result = await _commentsRepository.Create(comment);

                if (result)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Коммент создан",
                        StatusCode = Domain.Enum.StatusCode.Ok,
                        Data = true
                    };
                }
                return new BaseResponse<bool>()
                {
                    Description = "Коммент не создан",
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                    Data = false
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка в Сервисе Комментарией " + typeof(CommentsService).Name);
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);

               return new BaseResponse<bool>()
               {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError,
                    Data = false
               };
            }
        }

        public Task<BaseResponse<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<Comments>> Get(int id)
        {
            try
            {
                var comment = await _commentsRepository.Get(id);
                if (comment == null)
                {
                    return new BaseResponse<Comments>()
                    {
                        Description = "Коммент с id " + id + " не найден",
                        StatusCode = Domain.Enum.StatusCode.NotFound,
                        Data = null
                    };
                }

                return new BaseResponse<Comments>()
                {
                    Description = "Ok",
                    StatusCode = Domain.Enum.StatusCode.Ok,
                    Data = comment
                };
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Ошибка в Сервисе Комментарией " + typeof(CommentsService).Name);
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);

                return new BaseResponse<Comments>()
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError
                };
            }
        }

        public async Task<BaseResponse<List<Comments>>> GetAll()
        {
            try
            {
                var comments =  await _commentsRepository.GetAll().ToListAsync();
                return new BaseResponse<List<Comments>>()
                {
                    Data = comments,
                    Description = "Ok",
                    StatusCode = Domain.Enum.StatusCode.Ok
                };
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Ошибка в Сервисе Комментарией " + typeof(CommentsService).Name);
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new BaseResponse<List<Comments>>()
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError
                };
            }
        }

        public Task<BaseResponse<Comments>> Update(CommentsVm model)
        {
            throw new NotImplementedException();
        }
    }
}