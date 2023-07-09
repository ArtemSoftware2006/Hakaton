using DAL.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public AppDbContext _dbContext { get; set; }

        public CategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Create(Category entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Category entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Category> Get(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public IQueryable<Category> GetAll()
        {
            return _dbContext.Categories;
        }

        public async Task<Category> Update(Category entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
