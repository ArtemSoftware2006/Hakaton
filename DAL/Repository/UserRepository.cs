﻿using DAL.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Login == entity.Login);
            await _dbContext.AddAsync(new Employer()
            {
              UserId = user.Id
            }); 
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(User entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<User> Get(int id)
        {
            return await _dbContext.Users.Include(x => x.Profil).FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<User> GetAll()
        {
            return _dbContext.Users;
        }

        public async Task<User> Update(User entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
