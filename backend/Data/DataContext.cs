using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class DataContext : IdentityDbContext<User>
    {
        // DbSets

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var user = new User
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
                UserName = "Lauras",
                FirstName = "Lauras",
                LastName = "Dilys"
            };

            modelBuilder.Entity<User>().HasData(user);
        }
    }
}
