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
        Task<BaseResponse<Category>> Get(int id);
        Task<BaseResponse<Category>> GetByName(string name);
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<List<Category>>> GetAll();
    }
}
