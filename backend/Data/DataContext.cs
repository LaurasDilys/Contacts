using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Contact> Contacts { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                        .HasMany<Contact>(u => u.Contacts)
                        .WithOne(c => c.Creator)
                        .HasForeignKey(c => c.CreatorId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
