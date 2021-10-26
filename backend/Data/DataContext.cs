﻿using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ContactUser> ContactUsers { get; set; }

        public DbSet<UnacceptedShare> UnacceptedShares { get; set; }

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

            modelBuilder.Entity<ContactUser>().HasKey(cu => new { cu.ContactId, cu.UserId });

            modelBuilder.Entity<ContactUser>()
                        .HasOne<Contact>(cu => cu.Contact)
                        .WithMany(c => c.ContactUsers)
                        .HasForeignKey(cu => cu.ContactId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ContactUser>()
                        .HasOne<User>(cu => cu.User)
                        .WithMany(u => u.ContactUsers)
                        .HasForeignKey(cu => cu.UserId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                        .HasMany<UnacceptedShare>(u => u.UnacceptedShares)
                        .WithOne(us => us.User)
                        .HasForeignKey(us => us.UserId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
