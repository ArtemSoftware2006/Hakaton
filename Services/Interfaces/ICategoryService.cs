using DAL.Interfaces;
using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Deal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        Task<BaseResponse<Deal>> Get(int id);
        Task<BaseResponse<Deal>> GetByName(string name);
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<bool>> Create(string login, DealCreateVM model);
        Task<BaseResponse<List<Deal>>> GetAll();
        Task<BaseResponse<List<Deal>>> GetByCetegory(int id);
    }
}
