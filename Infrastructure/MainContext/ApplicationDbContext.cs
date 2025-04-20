using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MainContext;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    DbSet<ApplicationUser> ApplicationUsers { get; set; }

    DbSet<Car> Cars { get; set; }
    public DbSet<Race> Races { get; set; }
    DbSet<Event> Events { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<HomePageContent> HomePageContents { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<CarImage> CarImages { get; set; }
    public DbSet<RaceCar> RaceCars { get; set; }
    public DbSet<HomeContentDescription> HomeContentDescriptions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // In your DbContext's OnModelCreating
        modelBuilder.Entity<HomePageContent>().HasCheckConstraint("CK_SingleRecord", "Id = 1");
        modelBuilder.Entity<HomePageContent>().HasData(
            new HomePageContent
            {
                Id = 1,
                BannerText = "BannerText is empty",
                VideoFile = null,
                VideoPath = "VideoPath is empty"

            }
        );
        modelBuilder.Entity<RaceCar>().HasKey(r => new { r.CarId, r.RaceId });
        modelBuilder.Entity<RaceCar>()
            .HasOne(rc => rc.Race)
            .WithMany(r => r.RaceCars)
            .HasForeignKey(rc => rc.RaceId);

        modelBuilder.Entity<RaceCar>()
            .HasOne(rc => rc.Car)
            .WithMany(c => c.RaceCars)
            .HasForeignKey(rc => rc.CarId);
        
        modelBuilder.Entity<Race>()
            .HasOne(r => r.Location)
            .WithMany(l => l.Races)
            .HasForeignKey(r => r.LocationId)
            .OnDelete(DeleteBehavior.SetNull); // LocationId SetNull
        
        modelBuilder.Entity<Car>()
            .HasOne(r => r.Brand)
            .WithMany(l => l.Cars)
            .HasForeignKey(r => r.BrandId)
            .OnDelete(DeleteBehavior.SetNull); 
    }
}
