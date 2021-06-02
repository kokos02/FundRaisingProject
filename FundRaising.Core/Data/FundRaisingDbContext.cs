using FundRaising.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Data
{
    public class FundRaisingDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Fund> Funds { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog = FundRaising.Core; Integrated Security = true");
        }
    }
}
