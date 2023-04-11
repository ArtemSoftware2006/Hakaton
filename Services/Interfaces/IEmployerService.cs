using Domain.Entity;
using Domain.Response;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEmployerService
    {
        Task<BaseResponse<User>> Get(int id);
        Task<BaseResponse<IEnumerable<Employer>>> GetAll();
        Task<BaseResponse<User>> GetByLogin(string login);
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<bool>> Create(int UserId);
    }
}
