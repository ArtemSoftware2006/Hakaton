using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Deal;
using Domain.ViewModel.User;

namespace Services.Interfaces
{
    public interface IDealService
    {
        Task<BaseResponse<DealDetailsViewModel>> Get(int id, int userId);
        Task<BaseResponse<List<Deal>>> GetByTitle(string Title);
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<Deal>> Update(DealUpdateViewModel model);
        Task<BaseResponse<bool>> Create(DealCreateViewModel model);
        Task<BaseResponse<List<Deal>>> GetByUserId(int id);
        Task<BaseResponse<List<DealCardViewModel>>> GetAll();
        Task<BaseResponse<DealCardResponse>> GetAll(int page, int limit, int userId);        
        Task<BaseResponse<List<Deal>>> GetByCetegory(int id);
        Task<BaseResponse<bool>> AddCategories(DealAddCategoriesViewModel model);
    }
}
