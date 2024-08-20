using dogMatchmaking.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace dogMatchmaking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<DogModel> Dogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasMany(user => user.Dogs)
                .WithOne(dog => dog.User)
                .HasForeignKey(dog => dog.UserId)  
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
