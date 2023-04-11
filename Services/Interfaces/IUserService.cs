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
        Task<BaseResponse<List<User>>> GetExecutorByCategory(int id);
        Task<BaseResponse<User>> GetByLogin(string login);
        Task<BaseResponse<List<User>>> GetAllUsers();
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<bool>> SetCategory(UserSetCategoryVM model);
        Task<BaseResponse<ClaimsIdentity>> Registr(UserRegistrVM model);
        Task<BaseResponse<ClaimsIdentity>> Login(UserLoginVM model);

    }
}
