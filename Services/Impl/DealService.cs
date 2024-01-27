using AutoMapper;
using DAL.Interfaces;
using Domain.Entity;
using Domain.Enum;
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
        private readonly IProposalRepository _proposalRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public DealService(IDealRepository dealRepository,
            IMapper mapper,
            ICategoryRepository categoryRepository,
            IProposalRepository proposalRepository)
        {
            _proposalRepository = proposalRepository;
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
                    Views = 0,
                    Description = model.Description,
                    DatePublication = DateTime.UtcNow,
                    ApproximateDate = model.ApproximateDate,
                    MaxPrice = model.MaxPrice,
                    MinPrice = model.MinPrice,
                    Status =  StatusDeal.Published,
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
                    StatusCode =  StatusCode.Ok,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = ex.Message,
                    StatusCode =  StatusCode.InternalServiseError,
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
                        StatusCode =  StatusCode.Ok,
                    };
                }
                return new BaseResponse<bool>()
                {
                    Data = false,
                    StatusCode =  StatusCode.NotFound,
                    Description = $"Нет заказа с id = {id}",
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = ex.Message,
                    StatusCode =  StatusCode.InternalServiseError,
                };
            }
        }

        public async Task<BaseResponse<DealDetailsViewModel>> Get(int id, int userId)
        {
            try
            {
                Deal deal = await _dealRepository.GetWithCreator(id);
                if (deal != null)
                {
                    DealDetailsViewModel dealModel = _mapper.Map<DealDetailsViewModel>(deal);

                    List<Proposal> proposals = _proposalRepository
                        .GetAll()
                        .Where(x => x.UserId == userId)
                        .ToList();

                    bool alreadyResponded = proposals.Any(y => y.DealId == deal.Id);
                    dealModel.AlreadyResponded = alreadyResponded;

                    deal.Views++;
                    await _dealRepository.Update(deal);

                    return new BaseResponse<DealDetailsViewModel>()
                    {
                        Data = dealModel,
                        Description = "Ok",
                        StatusCode =  StatusCode.Ok,
                    };
                }
                return new BaseResponse<DealDetailsViewModel>()
                {
                    Description = $"Нет заказа с id = {id}",
                    StatusCode =  StatusCode.NotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<DealDetailsViewModel>()
                {
                    Description = ex.Message,
                    StatusCode =  StatusCode.InternalServiseError,
                };
            }
        }

        public async Task<BaseResponse<List<DealCardViewModel>>> GetAll()
        {
            try
            {
                var deals = _dealRepository
                    .GetAll()
                    .Where(x => x.Status == StatusDeal.Published)
                    .Select(x => _mapper.Map<DealCardViewModel>(x))
                    .ToList();
                
                if (deals.Count != 0)
                {
                    return new BaseResponse<List<DealCardViewModel>>()
                    {
                        Data = deals,
                        Description = "Ok",
                        StatusCode =  StatusCode.Ok,
                    };
                }
                return new BaseResponse<List<DealCardViewModel>>()
                {
                    Data = deals,
                    Description = "Нет заказов",
                    StatusCode =  StatusCode.NotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<DealCardViewModel>>()
                {
                    Description = ex.Message,
                    StatusCode =  StatusCode.InternalServiseError,
                };
            }
        }

        public async Task<BaseResponse<List<Deal>>> GetByTitle(string Title)
        {
            try
            {
                List<Deal> deals = await _dealRepository
                    .GetAll()
                    .Where(x => x.Title.Contains(Title))
                    .ToListAsync();

                return new BaseResponse<List<Deal>>() {
                    Data = deals,
                    StatusCode =  StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Deal>>() {
                    StatusCode =  StatusCode.InternalServiseError,
                    Description = ex.Message
                };
            }
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
            //             StatusCode =  StatusCode.Ok,
            //         };
            //     }
            //     return new BaseResponse<List<Deal>>()
            //     {
            //         Data = deals,
            //         Description = $"Нет заказов с категорией с id={id}",
            //         StatusCode =  StatusCode.NotFound,
            //     };
            // }
            // catch (Exception ex)
            // {
            //     return new BaseResponse<List<Deal>>()
            //     {
            //         Description = ex.Message,
            //         StatusCode =  StatusCode.InternalServiseError,
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
                        StatusCode =  StatusCode.Ok,
                    };
                }
                return new BaseResponse<List<Deal>>()
                {
                    Data = deals,
                    Description = $"Нет заказов с id пользователя = {id}",
                    StatusCode =  StatusCode.NotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Deal>>()
                {
                    Description = ex.Message,
                    StatusCode =  StatusCode.InternalServiseError,
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
                        x => x.Id == model.Id && x.Status ==  StatusDeal.Published
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
                        StatusCode =  StatusCode.Ok,
                    };
                }
                return new BaseResponse<Deal>()
                {
                    StatusCode =  StatusCode.NotFound,
                    Description = $"нет заказа с id = {model.Id}"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Deal>()
                {
                    Description = ex.Message,
                    StatusCode =  StatusCode.InternalServiseError,
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
                        StatusCode =  StatusCode.NotFound
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
                    StatusCode =  StatusCode.Ok,
                    Description = "Ok"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>() 
                {
                    Description = ex.Message,
                    StatusCode =  StatusCode.InternalServiseError
                };
            }
        }

        public async Task<BaseResponse<DealCardResponse>> GetAll(int page, int limit, int userId)
        {
            try
            {
                List<Proposal> proposals = _proposalRepository
                    .GetAll()
                    .Where(x => x.UserId == userId)
                    .ToList();
                List<DealCardViewModel> deals = _dealRepository
                    .GetAll()
                    .Where(x => x.Status == StatusDeal.Published)
                    .OrderByDescending(x => x.DatePublication)
                    .Skip((page - 1) * limit)
                    .Take(limit)
                    .Select(x => _mapper.Map<DealCardViewModel>(x))
                    .ToList();

                deals.ForEach(deal =>
                {
                    bool alreadyResponded = proposals.Any(y => y.DealId == deal.Id);
                    deal.AlreadyResponded = alreadyResponded;
                });

                DealCardResponse response = new DealCardResponse() 
                {
                    Deals = deals,
                    Total = _dealRepository.GetAll().Count() 
                };

                return new BaseResponse<DealCardResponse>()
                {
                    Data = response,
                    StatusCode = StatusCode.Ok,
                    Description = "Ok"
                };
            }
            catch (Exception ex)
            {

                return new BaseResponse<DealCardResponse>() 
                {
                    StatusCode =  StatusCode.InternalServiseError,
                    Description = ex.Message
                };
            }
        }
    }
}
