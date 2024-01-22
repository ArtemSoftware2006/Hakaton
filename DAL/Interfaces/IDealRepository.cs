using Domain.Entity;

namespace DAL.Interfaces
{
    public interface IDealRepository : IBaseRepository<Deal> 
    { 
        public Task<Deal> GetWithCreator(int id);   
        public Task<Deal> GetWithCategory(int id);
    }
}
