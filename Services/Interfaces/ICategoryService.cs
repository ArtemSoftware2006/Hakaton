using Domain.Entity;
using Domain.Response;

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
