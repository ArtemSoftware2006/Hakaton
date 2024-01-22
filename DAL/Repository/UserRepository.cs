using DAL.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AppDbContext _dbContext { get; set; }

        public async Task<bool> Create(User entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(User entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<User> Get(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<User> GetAll()
        {
            return _dbContext.Users;
        }

        public Task<User> GetWithCategories(int id)
        {
            return _dbContext.Users.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> Update(User entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
