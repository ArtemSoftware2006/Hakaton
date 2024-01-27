using DAL.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class ContractRepository : IContractRepository
    {
        private readonly AppDbContext _dbContext;

        public ContractRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Contract entity)
        {
            await _dbContext.AddAsync(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Contract entity)
        {
            _dbContext.Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Contract> Get(int id)
        {
            return await _dbContext.Contracts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Contract> GetAll()
        {
            return _dbContext.Contracts;
        }

        public async Task<Contract> Update(Contract entity)
        {
            Contract contract = _dbContext.Update(entity).Entity;

            await _dbContext.SaveChangesAsync();

            return contract;
        }
    }
}