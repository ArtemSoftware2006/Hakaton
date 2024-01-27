using DAL.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class DealRepository : IDealRepository
    {
        public AppDbContext _dbContext { get; set; }

        public DealRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Deal entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Deal entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Deal> Get(int id)
        {
            return _dbContext.Deals.FirstOrDefault(x => x.Id == id);
        }
        public async Task<Deal> GetWithCreator(int id)
        {
            return _dbContext.Deals
                .Include(x => x.CreatorUser)
                .ThenInclude(u => u.Categories)
                .FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Deal> GetAll()
        {
            return _dbContext.Deals.Include(x => x.Categories).Include(x => x.CreatorUser);
        }

        public async Task<Deal> Update(Deal entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Deal> GetWithCategory(int id)
        {
            return _dbContext.Deals.Include(x => x.Categories).FirstOrDefault(x => x.Id == id);
        }
    }
}
