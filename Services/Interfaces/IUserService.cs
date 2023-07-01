using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<User>> Get(int id);
        Task<BaseResponse<User>> GetByLogin(string login);
        Task<BaseResponse<List<User>>> GetAllUsers();
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<bool>> VIP(int id);
        Task<BaseResponse<bool>> Update(UserUpdateVM model);
        Task<BaseResponse<bool>> SetCategory(UserSetCategoryVM model);
        Task<BaseResponse<User>> Registr(UserRegistrVM model);
        Task<BaseResponse<User>> Login(UserLoginVM model);
            
    }
}
