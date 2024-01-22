using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.User;

namespace Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<UserProfileViewModel>> Get(int id);
        Task<BaseResponse<User>> GetByLogin(string login);
        Task<BaseResponse<List<User>>> GetAllUsers();
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<bool>> VIP(int id);
        Task<BaseResponse<bool>> Update(UserUpdateViewModel model);
        Task<BaseResponse<bool>> SetCategory(UserSetCategoryViewModel model);
        //Task<BaseResponse<bool>> SetCategories(int userId, List<int> model);
        Task<BaseResponse<User>> Register(UserRegistrViewModel model);
        Task<BaseResponse<User>> Login(UserLoginViewModel model);
    }
}
