using CruiseShip.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace CruiseShip.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // DbSet properties for models
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Room> Rooms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure relationships for Bill
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Voyager)
                .WithMany()
                .HasForeignKey(b => b.VoyagerId)
                .OnDelete(DeleteBehavior.Restrict);
            // Configure relationships for Booking
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Voyager)
                .WithMany()
                .HasForeignKey(b => b.VoyagerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Facility)
                .WithMany(f => f.Bookings)
                .HasForeignKey(b => b.FacilityId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
            // Configure relationships for Facility
            modelBuilder.Entity<Facility>()
               .HasOne<IdentityUser>() // Specify the related entity type
               .WithMany()
               .HasForeignKey(f => f.CreatedBy)
               .OnDelete(DeleteBehavior.Restrict);
            // Configure relationships for Room
            modelBuilder.Entity<Room>()
                .HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(r => r.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            
            // Admin User ID (Replace with actual admin ID or retrieve dynamically during runtime)
            var adminId = "d4689a91-5fb8-48f0-83f6-51d2db67012b";
            // Seed Facilities
            modelBuilder.Entity<Facility>().HasData(
                new Facility
                {
                    Id = 1,
                    Name = "Gym",
                    Description = "Fully equipped gym with modern equipment.",
                    Fee = 50.00m,
                    AvailableSlots = 20,
                    CreatedBy = adminId
                },
                new Facility
                {
                    Id = 2,
                    Name = "Swimming Pool",
                    Description = "Olympic-sized pool with lifeguards on duty.",
                    Fee = 100.00m,
                    AvailableSlots = 10,
                    CreatedBy = adminId
                },
                new Facility
                {
                    Id = 3,
                    Name = "Party Hall",
                    Description = "Spacious party hall with modern amenities.",
                    Fee = 300.00m,
                    AvailableSlots = 5,
                    CreatedBy = adminId
                });
        }
    }
}