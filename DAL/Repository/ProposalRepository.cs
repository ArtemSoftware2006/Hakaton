using DAL.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class ProposalRepository : IProposalRepository
    {
        public AppDbContext _dbContext { get; set; }

        public ProposalRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Proposal entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Proposal entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Proposal> Get(int id)
        {
            return await _dbContext.Proposals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Proposal> GetAll()
        {
            return _dbContext.Proposals.Include(x => x.User);
        }

        public async Task<Proposal> Update(Proposal entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Proposal> GetWithUser(int id)
        {
            return await _dbContext.Proposals
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
