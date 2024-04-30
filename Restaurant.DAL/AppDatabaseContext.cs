using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entity;

namespace Restaurant.DAL;

public class AppDatabaseContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<PhoneCode> PhoneCodes { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<ReservationTable> ReservationTables { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=Manga;");
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CatalogId);
    }
}