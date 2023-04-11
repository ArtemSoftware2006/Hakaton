using DAL.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class ProposalRepository : IProposalRepository
    {
        public ProposalRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public AppDbContext _dbContext { get; set; }
        public async Task<bool> Create(Proposal entity)
        {
            await _dbContext.Proposals.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Proposal entity)
        {
            _dbContext.Proposals.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Proposal> Get(int id)
        {
            return await _dbContext.Proposals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Proposal> GetAll()
        {
            return _dbContext.Proposals;
        }

        public async Task<Proposal> Update(Proposal entity)
        {
            _dbContext.Proposals.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
