using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Deal;

namespace Services.Interfaces
{
    public interface IDealService
    {
        Task<BaseResponse<Deal>> Get(int id);
        Task<BaseResponse<Deal>> GetByTitle(string Title);
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<Deal>> Update(DealUpdateViewModel model);
        Task<BaseResponse<bool>> Create(DealCreateViewModel model);
        Task<BaseResponse<List<Deal>>> GetByUserId(int id);
        Task<BaseResponse<List<Deal>>> GetAll();
        Task<BaseResponse<List<Deal>>> GetByCetegory(int id);
    }
}
