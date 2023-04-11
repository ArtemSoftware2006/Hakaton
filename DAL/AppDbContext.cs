using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Deal> Deals { get; set; }
        //public DbSet<Employer> Employers { get; set; }
        //public DbSet<Executor> Executors { get; set; }
        //public DbSet<Profil> Profils { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Разработка"},
                new Category() { Id = 2, Name = "Дизайн" },
                new Category() { Id = 3, Name = "Базы данных" },
                new Category() { Id = 4, Name = "Репетиторство" },
                new Category() { Id = 5, Name = "Литература" },
                new Category() { Id = 6, Name = "Музыка" },
                new Category() { Id = 7, Name = "Видео мантаж" },
                new Category() { Id = 8, Name = "Фото" },
                new Category() { Id = 9, Name = "3D" },
                new Category() { Id = 10, Name = "Переводы" });
        }
    }
}
