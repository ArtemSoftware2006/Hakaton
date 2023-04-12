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
        Task<BaseResponse<Deal>> Update(DealUpdateVM model);
        Task<BaseResponse<bool>> Create(DealCreateVM model);
        Task<BaseResponse<List<Deal>>> GetAll();
        Task<BaseResponse<List<Deal>>> GetByCetegory(int id);
    }
}
