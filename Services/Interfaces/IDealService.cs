using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Deal;
using Domain.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDealService
    {
        Task<BaseResponse<Deal>> Get(int id);
        Task<BaseResponse<Deal>> GetByTitle(string Title);
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<bool>> Create(string login, DealCreateVM model);
        Task<BaseResponse<List<Deal>>> GetAll();
    }
}
