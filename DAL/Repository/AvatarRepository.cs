using DAL.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class AvatarRepository : IAvatarRepository
    {
        private readonly AppDbContext _dbContext;
        public AvatarRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Create(Avatar entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Avatar entity)
        {
             _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Avatar> Get(int UserId)
        {
            return await _dbContext.Avatars.FirstOrDefaultAsync(x=>x.UserId == UserId);
        }

        public IQueryable<Avatar> GetAll()
        {
            return _dbContext.Avatars;
        }

        public async Task<Avatar> Update(Avatar entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;        
        }
    }
}