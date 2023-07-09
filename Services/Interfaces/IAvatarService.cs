using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Deal;
using Domain.ViewModel.User;

namespace Services.Interfaces
{
    public interface IAvatarService
    {
       Task<BaseResponse<Avatar>> Get(int id);
        Task<BaseResponse<List<Avatar>>> GetAll();
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<bool>> Update(Avatar model);
        Task<BaseResponse<bool>> Create(Avatar model);

    }
}