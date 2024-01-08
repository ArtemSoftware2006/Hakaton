using DAL.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly AppDbContext _dbContext;

        public CommentsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Comments entity)
        {
            await _dbContext.AddAsync(entity);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Comments entity)
        {
            _dbContext.Remove(entity);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Comments> Get(int id)
        {
            return await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Comments> GetAll()
        {
            return _dbContext.Comments;
        }

        public async Task<Comments> Update(Comments entity)
        {
            _dbContext.Update(entity);
            return entity;
        }
    }
}
