namespace ShoesShopProject.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Good> Goods { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<GoodPhoto> GoodPhotos { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Size> Sizes { get; set; } = null!;
    public DbSet<GoodOrder> GoodOrder { get; set; } = null!;

    /*    public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }*/
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUser>()
        .HasMany(e => e.WishList)
        .WithMany();

        /*        modelBuilder.Entity<GoodOrder>()
               .HasKey(go => new { go.GoodId, go.OrderId });*/

        modelBuilder.Entity<GoodOrder>()
            .HasOne(go => go.Good)
            .WithMany(g => g.GoodOrders)
            .HasForeignKey(go => go.GoodId);

        modelBuilder.Entity<GoodOrder>()
            .HasOne(go => go.Order)
            .WithMany(o => o.GoodOrders)
            .HasForeignKey(go => go.OrderId);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Data.sqlite;");
    }

}
