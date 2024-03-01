namespace ShoesShopProject.Models;

using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<Good> Goods { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<GoodPhoto> GoodPhotos { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Size> Sizes { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    /*    public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }*/
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
        .HasMany(e => e.WishList)
        .WithMany();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Data.sqlite;");
    }

}
