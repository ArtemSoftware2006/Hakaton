using DAL.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class EmployerRepository : IEmployerRepository
    {
        public EmployerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public AppDbContext _dbContext { get; set; }
        public async Task<bool> Create(Employer entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Employer entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Employer> Get(int id)
        {
            //Возможно не будет Deals
            return await _dbContext.Employers.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Employer> GetAll()
        {
            return _dbContext.Employers;
        }

        public async Task<Employer> Update(Employer entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
