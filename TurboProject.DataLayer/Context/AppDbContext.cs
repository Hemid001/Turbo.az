using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TurboProject.DataLayer.Entity;

namespace TurboProject.DataLayer.Context
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BodyType> BodyTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<EngineSize> EngineSizes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<CarsModel> Models { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Status)
                .WithMany()
                .HasForeignKey(p => p.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Car)
                .WithMany()
                .HasForeignKey(p => p.CarId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Brand>()
                .HasIndex(b => b.Name)
                .IsUnique(); 

            modelBuilder.Entity<CarsModel>()
                .HasIndex(m => m.Name)
                .IsUnique(); 

            modelBuilder.Entity<CarsModel>()
                .HasOne(m => m.Brand)
                .WithMany(b => b.Models)
                .HasForeignKey(m => m.BrandId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}


