using DAL.Interfaces;
using Domain.Entity;
using Domain.Response;
using Services.Interfaces;

namespace Services.Impl
{
    public class CategoryService : ICategoryService
    {
        public ICategoryRepository _categoryRepository { get; set; }

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public Task<BaseResponse<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Category>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<List<Category>>> GetAll()
        {
            try
            {
                var categories = _categoryRepository.GetAll().ToList();
                if (categories.Count != 0)
                {
                    return new BaseResponse<List<Category>>()
                    {
                        Data = categories,
                        StatusCode = Domain.Enum.StatusCode.Ok,
                        Description = "Ok"
                    };
                }
                return new BaseResponse<List<Category>>()
                {
                    Data = categories,
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                    Description = "Нет категорий"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Category>>()
                {
                    StatusCode = Domain.Enum.StatusCode.InternalServiseError,
                    Description = ex.Message,
                };
            }
        }

        public Task<BaseResponse<Category>> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
