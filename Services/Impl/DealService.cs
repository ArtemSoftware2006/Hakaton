using AutoMapper;
using DAL.Interfaces;
using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Deal;
using Domain.ViewModel.User;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Impl
{
    public class DealService : IDealService
    {
        private readonly IDealRepository _dealRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public DealService(IDealRepository dealRepository,
            IMapper mapper,
            ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _dealRepository = dealRepository;
        }

        public async Task<BaseResponse<bool>> Create(DealCreateViewModel model)
        {
            try
            {
                var deal = new Deal()
                {
                    Title = model.Title,
                    Description = model.Description,
                    DatePublication = DateTime.UtcNow,
                    ApproximateDate = model.ApproximateDate,
                    MaxPrice = model.MaxPrice,
                    MinPrice = model.MinPrice,
                    Status = Domain.Enum.StatusDeal.Published,
                    Localtion = model.Location,
                    CreatorUserId = model.UserId,
                };

                List<Category> categories = _categoryRepository
                    .GetAll()
                    .Where(x => model.CategoryIds.Contains(x.Id))
                    .ToList();

                deal.Categories = categories;

                await _dealRepository.Create(deal);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Ok",
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

        public async Task<BaseResponse<DealDetailsViewModel>> Get(int id)
        {
            try
            {
                Deal deal = await _dealRepository.GetWithCreator(id);
                if (deal != null)
                {
                    DealDetailsViewModel dealModel = _mapper.Map<DealDetailsViewModel>(deal);
                    return new BaseResponse<DealDetailsViewModel>()
                    {
                        Data = dealModel,
                        Description = "Ok",
                        StatusCode = Domain.Enum.StatusCode.Ok,
                    };
                }
                return new BaseResponse<DealDetailsViewModel>()
                {
                    Description = $"Нет заказа с id = {id}",
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<DealDetailsViewModel>()
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError,
                };
            }
        }

        public async Task<BaseResponse<List<DealCardViewModel>>> GetAll()
        {
            try
            {
                var deals = _dealRepository
                    .GetAll()
                    .Where(x => x.Status == Domain.Enum.StatusDeal.Published)
                    .Select(x => _mapper.Map<DealCardViewModel>(x))
                    .ToList();
                if (deals.Count != 0)
                {
                    return new BaseResponse<List<DealCardViewModel>>()
                    {
                        Data = deals,
                        Description = "Ok",
                        StatusCode = Domain.Enum.StatusCode.Ok,
                    };
                }
                return new BaseResponse<List<DealCardViewModel>>()
                {
                    Data = deals,
                    Description = "Нет заказов",
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<DealCardViewModel>>()
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
            throw new NotImplementedException();
            // try
            // {
            //     var deals = _dealRepository.GetAll().Where(x => x.CategoryId == id).ToList();
            //     if (deals.Count != 0)
            //     {
            //         return new BaseResponse<List<Deal>>()
            //         {
            //             Data = deals,
            //             Description = "Ok",
            //             StatusCode = Domain.Enum.StatusCode.Ok,
            //         };
            //     }
            //     return new BaseResponse<List<Deal>>()
            //     {
            //         Data = deals,
            //         Description = $"Нет заказов с категорией с id={id}",
            //         StatusCode = Domain.Enum.StatusCode.NotFound,
            //     };
            // }
            // catch (Exception ex)
            // {
            //     return new BaseResponse<List<Deal>>()
            //     {
            //         Description = ex.Message,
            //         StatusCode = Domain.Enum.StatusCode.InternalServiseError,
            //     };
            // }
        }

        public async Task<BaseResponse<List<Deal>>> GetByUserId(int id)
        {
            try
            {
                var deals = _dealRepository.GetAll().Where(x => x.CreatorUserId == id).ToList();
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
                    Description = $"Нет заказов с id пользователя = {id}",
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

        public async Task<BaseResponse<Deal>> Update(DealUpdateViewModel model)
        {
            try
            {
                var deal = await _dealRepository
                    .GetAll()
                    .FirstOrDefaultAsync(
                        x => x.Id == model.Id && x.Status == Domain.Enum.StatusDeal.Published
                    );
                if (deal != null)
                {
                    deal.MaxPrice = model.MaxPrice ?? deal.MaxPrice;
                    deal.MinPrice = model.MinPrice ?? deal.MinPrice;
                    deal.ApproximateDate = model.ApproximateDate ?? deal.ApproximateDate;
                    deal.Description = model.Description ?? deal.Description;
                    deal.Title = model.Title ?? deal.Title;
                    deal.Localtion = model.Location ?? deal.Localtion;

                    await _dealRepository.Update(deal);

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

        public async Task<BaseResponse<bool>> AddCategories(DealAddCategoriesViewModel model)
        {
            try
            {
                Deal deal = await _dealRepository.GetWithCategory(model.DealId);

                if (deal == null)
                {
                    return new BaseResponse<bool>() 
                    {
                        Description = $"Нет заказа с id = {model.DealId}",
                        StatusCode = Domain.Enum.StatusCode.NotFound
                    };
                }

                IEnumerable<Category> categories = _categoryRepository
                    .GetAll()
                    .Where(x => model.CategoryIds.Contains(x.Id));

                deal.Categories.AddRange(categories);

                deal = await _dealRepository.Update(deal);

                return new BaseResponse<bool>() 
                {
                    Data = true,
                    StatusCode = Domain.Enum.StatusCode.Ok,
                    Description = "Ok"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>() 
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError
                };
            }
        }
    }
}
