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

        public async Task<bool> Create(CommentDeals entity)
        {
            await _dbContext.AddAsync(entity);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(CommentDeals entity)
        {
            _dbContext.Remove(entity);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<CommentDeals> Get(int id)
        {
            return await _dbContext.CommentDeals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<CommentDeals> GetAll()
        {
            return _dbContext.CommentDeals;
        }

        public async Task<CommentDeals> Update(CommentDeals entity)
        {
            _dbContext.Update(entity);
            return entity;
        }
    }
}
