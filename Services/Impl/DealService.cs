using DAL.Interfaces;
using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Deal;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impl
{
    public class DealService : IDealService
    {
        public IDealRepository _dealRepository { get; set; }
        public DealService(IDealRepository dealRepository, IUserRepository userRepository)
        {
            _dealRepository = dealRepository;
        }
        public async Task<BaseResponse<bool>> Create( DealCreateVM model)
        {
            try
            {
                var deal = new Deal()
                {
                    Title = model.Title,
                    Description = model.Description,
                    DatePublication = DateTime.UtcNow,
                    StartDate = model.StartDate,
                    StopDate = model.StopDate,
                    MaxPrice = model.MaxPrice,
                    MinPrice = model.MinPrice,
                    CategoryId = model.CategoryId,
                    Status = Domain.Enum.StatusDeal.Published,
                    location = model.location,
                    UserId = model.UserId,
                };

                await _dealRepository.Create(deal);

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

        public async Task<BaseResponse<bool>> Delete(int id)
        {
            try
            {
                var deal = await _dealRepository.Get(id);

                if (deal != null)
                {
                    _dealRepository.Delete(deal);

                    return new BaseResponse<bool>()
                    {
                        Data = true,
                        Description = "Ok",
                        StatusCode = Domain.Enum.StatusCode.Ok,
                    };
                }
                return new BaseResponse<bool>()
                {
                    Data = false,
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                    Description = $"Нет заказа с id = {id}",
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

        public async Task<BaseResponse<Deal>> Get(int id)
        {
            try
            {
                var deal = await _dealRepository.Get(id);
                if (deal != null)
                {
                    return new BaseResponse<Deal>()
                    {
                        Data = deal,
                        Description = "Ok",
                        StatusCode = Domain.Enum.StatusCode.Ok,
                    };
                }
                return new BaseResponse<Deal>()
                {
                    Data = deal,
                    Description = $"Нет заказа с id = {id}",
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Deal>()
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError,
                };
            }
        }

        public async Task<BaseResponse<List<Deal>>> GetAll()
        {
            try
            {
                var deals = _dealRepository.GetAll().Where(x => x.Status == Domain.Enum.StatusDeal.Published).ToList();
                if (deals.Count != 0)
                {
                    return new BaseResponse<List<Deal>>()
                    {
                        Data = deals,
                        Description = "Ok",
                        StatusCode = Domain.Enum.StatusCode.Ok,
                    };
                }
                return new BaseResponse<List<Deal>>()
                {
                    Data = deals,
                    Description = "Нет заказов",
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Deal>>()
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError,
                };
            }
        }

        public Task<BaseResponse<Deal>> GetByTitle(string Title)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<List<Deal>>> GetByCetegory(int id)
        {
            try
            {
                var deals = _dealRepository.GetAll().Where(x => x.CategoryId == id).ToList();
                if (deals.Count != 0)
                {
                    return new BaseResponse<List<Deal>>()
                    {
                        Data = deals,
                        Description = "Ok",
                        StatusCode = Domain.Enum.StatusCode.Ok,
                    };
                }
                return new BaseResponse<List<Deal>>()
                {
                    Data = deals,
                    Description = $"Нет заказов с категорией с id={id}",
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Deal>>()
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError,
                };
            }
        }

        public async Task<BaseResponse<Deal>> Update(DealUpdateVM model)
        {
            try
            {
                var deal = await _dealRepository.GetAll().FirstOrDefaultAsync(x=> x.Id == model.Id && x.Status == Domain.Enum.StatusDeal.Published);
                if (deal != null)
                {
                    deal.MaxPrice = model.MaxPrice ?? deal.MaxPrice;
                    deal.MinPrice = model.MinPrice ?? deal.MinPrice;
                    deal.StartDate = model.StartDate ?? deal.StartDate;
                    deal.StopDate = model.StopDate ?? deal.StopDate;
                    deal.Description = model.Description ?? deal.Description;
                    deal.Title = model.Title ?? deal.Title;
                    deal.CategoryId = model.CategoryId ?? deal.CategoryId;
                    deal.location = model.location ?? deal.location;

                    _dealRepository.Update(deal);

                    return new BaseResponse<Deal>()
                    {
                        Data = deal,
                        Description = "Ok",
                        StatusCode = Domain.Enum.StatusCode.Ok,
                    };
                }
                return new BaseResponse<Deal>()
                {
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                    Description = $"нет заказа с id = {model.Id}"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Deal>()
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError,
                };
            }
        }
    }
}
