using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Avatar;

namespace Services.Interfaces
{
    public interface IAvatarService
    {
        Task<BaseResponse<byte[]>> Get(int id);
        Task<BaseResponse<List<Avatar>>> GetAll();
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<bool>> Update(CreateAvatarViewModel model);
        Task<BaseResponse<bool>> Create(CreateAvatarViewModel model);
    }
}
