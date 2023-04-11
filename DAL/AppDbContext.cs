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
        public DbSet<Proposal> Proposals { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Deal>().HasData(
            //    new Deal() 
            //    { 
            //        Id = 1, 
            //        DatePublication = DateTime.UtcNow, 
            //        Description="Напишите игру Змейка на Python", 
            //        location="Москва",
            //        MinPrice = 1000,
            //        MaxPrice = 2000,
            //        Title = "Игра Змейка",
            //        StartDate = DateTime.UtcNow,
            //        StopDate = DateTime.UtcNow.AddDays(3),
            //        UserId = 1,
            //        CategoryId = 1
            //    },
            //    new Deal()
            //    {
            //        Id = 1,
            //        DatePublication = DateTime.UtcNow,
            //        Description = "Разработать дизайн сайта для продажи машин",
            //        location = "Дубна",
            //        MinPrice = 1300,
            //        MaxPrice = 2300,
            //        Title = "Дизайн сайта",
            //        StartDate = DateTime.UtcNow,
            //        StopDate = DateTime.UtcNow.AddDays(5),
            //        UserId = 1,
            //        CategoryId = 2
            //    });
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
