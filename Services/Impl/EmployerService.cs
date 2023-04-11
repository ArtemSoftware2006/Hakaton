using Domain.Entity;
using Domain.Response;
using Domain.ViewModel;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class EmployerService : IEmployerService
    {
        public Task<BaseResponse<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<User>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<User>> GetByLogin(string login)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponse<bool>> IEmployerService.Create(int UserId)
        {
            throw new NotImplementedException();
        }
    }
}
