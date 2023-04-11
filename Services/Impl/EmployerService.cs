using DAL.Repository;
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
        public EmployerRepository _employerRepository { get; set; }

        public EmployerService(EmployerRepository employerRepository)
        {
            _employerRepository = employerRepository;
        }

        public Task<BaseResponse<bool>> Create(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<User>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IEnumerable<Employer>>> GetAll()
        {
            try
            {
                var users = _employerRepository.GetAll();
                if (users.Count() != 0)
                {
                    return new BaseResponse<IEnumerable<Employer>>()
                    {
                        Data = users,
                        StatusCode = Domain.Enum.StatusCode.Ok,
                        Description = "Ok"
                    };
                }
                else
                {
                    return new BaseResponse<IEnumerable<Employer>>()
                    {
                        Data = users,
                        StatusCode = Domain.Enum.StatusCode.NotFound,
                        Description = "Нет заказчиков."
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Employer>>()
                {
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError,
                    Description = ex.Message,
                };
            }
        }

        public Task<BaseResponse<User>> GetByLogin(string login)
        {
            throw new NotImplementedException();
        }

    }
}
