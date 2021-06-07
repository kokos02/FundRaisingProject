using FundRaising.Core.Interfaces;
using FundRaising.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Data
{
    public class FundRaisingDbContext : DbContext 
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog = FundRaisingProject; Integrated Security = true");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Reward> Rewards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<UserReward>()
                .ToTable("UserRewards")
                .HasKey(c => new { c.RewardId, c.UserId });
        }


        //public FundRaisingDbContext(DbContextOptions<FundRaisingDbContext> options)
        //  : base(options)
        //{
        //}

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //    base.OnModelCreating(builder);
        //}

        //public async Task<int> SaveChangesAsync()
        //{
        //    return await base.SaveChangesAsync();
        //}
        //public override int SaveChanges()
        //{
        //    return base.SaveChanges();
        //}

    }
}




