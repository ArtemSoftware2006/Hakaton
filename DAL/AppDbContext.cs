using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<CommentDeals> CommentDeals { get; set; }
        public DbSet<CommentUsers> CommentUsers { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Avatar> Avatars { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deal>()
                .HasOne(x => x.CreatorUser)
                .WithMany(x => x.CreatedDeals)
                .HasForeignKey(x => x.CreatorUserId);
            modelBuilder.Entity<Deal>()
                .HasOne(x => x.ExecutorUser)
                .WithMany(x => x.AcceptedDeals)
                .HasForeignKey(x => x.ExecutorUserId);

            modelBuilder.Entity<CommentUsers>()
                .HasOne(x => x.CreatorUser)
                .WithMany(x => x.CommentUsers)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<CommentDeals>()
                .HasOne(x => x.CreatorUser)
                .WithMany(x => x.CommentDeals)
                .HasForeignKey(x => x.CreatorUserId);

            modelBuilder.Entity<Contract>()
                .HasOne(x => x.Executor)
                .WithMany(x => x.ContractsAsExecutor)
                .HasForeignKey(x => x.ExecutorId);
            
            modelBuilder.Entity<Contract>()
                .HasOne(x => x.Employer)
                .WithMany(x => x.ContractsAsEmployer)
                .HasForeignKey(x => x.EmployerId);
            
            modelBuilder
                .Entity<Category>()
                .HasData(
                    new Category() { Id = 1, Name = "Development" },
                    new Category() { Id = 2, Name = "Design" },
                    new Category() { Id = 3, Name = "Data bases" },
                    new Category() { Id = 4, Name = "Tutoring" },
                    new Category() { Id = 5, Name = "Literature" },
                    new Category() { Id = 6, Name = "Music" },
                    new Category() { Id = 7, Name = "Video editing" },
                    new Category() { Id = 8, Name = "Photo" },
                    new Category() { Id = 9, Name = "3D" },
                    new Category() { Id = 10, Name = "Translations" }
                );
        }
    }
}
