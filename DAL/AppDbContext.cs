using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();

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
