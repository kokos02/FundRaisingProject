using FundRaising.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FundRaising.Core.Data
{
    public class FundRaisingDbContext : DbContext 
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog= FundRaisingDbContext; Integrated Security = true");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<UserReward> UserRewards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<UserReward>()
                .ToTable("UserRewards")
                .HasKey(c => new { c.RewardId, c.UserId });
        }


    }
}




