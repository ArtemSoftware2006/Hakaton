using Domain.Entity;

namespace DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository<User> 
    {
        Task<User> GetWithCategories(int id);
    }
}
