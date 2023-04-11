using DAL.Interfaces;
using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Deal;
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
        public IUserRepository _userRepository { get; set; }
        public DealService(IDealRepository dealRepository, IUserRepository userRepository)
        {
            _dealRepository = dealRepository;
            _userRepository = userRepository;
        }
        public async Task<BaseResponse<bool>> Create(string login, DealCreateVM model)
        {
            try
            {
                var deal = new Deal()
                {
                    Title = model.Title,
                    Description = model.Description,
                    DatePublication = DateTime.UtcNow,
                    MaxPrice = model.MaxPrice,
                    MinPrice = model.MinPrice,
                    CategoryId = model.CategoryId,
                    EmployerId = _userRepository.GetAll().FirstOrDefault(x => x.Login == login).Id,
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

        public Task<BaseResponse<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Deal>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<List<Deal>>> GetAll()
        {
            try
            {
                var deals = _dealRepository.GetAll().ToList();
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
    }
}
