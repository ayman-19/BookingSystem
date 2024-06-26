﻿using BookingSystem.Domain.Model;
using BookingSystem.Presistance.UserConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Presistance.Data
{
    public class BookingDbContext : IdentityDbContext<User>
    {
        public BookingDbContext(DbContextOptions options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
        //        .AddInterceptors(new SoftDelete());
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(UserConfigurationType).Assembly);
            builder.Entity<IdentityRole>().HasData(new[]
            {
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "User".ToUpper(),
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                }
            });
        }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
