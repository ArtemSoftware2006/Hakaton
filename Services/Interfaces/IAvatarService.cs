using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Avatar;
using Domain.ViewModel.Deal;
using Domain.ViewModel.User;

namespace Services.Interfaces
{
    public interface IAvatarService
    {
       Task<BaseResponse<MemoryStream>> Get(int id);
        Task<BaseResponse<List<Avatar>>> GetAll();
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<bool>> Update(CreateAvatarVM model);
        Task<BaseResponse<bool>> Create(CreateAvatarVM model );

    }
}