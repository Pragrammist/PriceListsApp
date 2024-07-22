using Microsoft.EntityFrameworkCore;
using WebApplication1.EfCore.Models;

namespace WebApplication1.EfCore;

public class PriceListDbContext(DbContextOptions<PriceListDbContext> dbContextOptions) : DbContext(dbContextOptions)
{

    public DbSet<PriceListModel> PriceLists { get; set; }
    public DbSet<PriceColumnPropTypeModel> PriceColumnPropTypes { get; set; }
    public DbSet<PriceColumnModel> PriceColumns { get; set; }
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<ProductColumnValueModel> ProductColumnValues { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PriceListModel>().HasMany(pl => pl.Columns).WithMany(c => c.PriceLists);
        modelBuilder.Entity<PriceColumnModel>().HasOne(pc => pc.PropType).WithMany().HasForeignKey(c => c.PropTypeId);
        modelBuilder.Entity<PriceColumnModel>().HasIndex(c => new { c.PropName, c.PropTypeId }).IsUnique();
        modelBuilder.Entity<PriceColumnPropTypeModel>().HasData([
            new()
            {
                Id = 1,
                Name = "Число"
            },
            new()
            {
                Id = 2,
                Name = "Строка"
            },
            new()
            {
                Id = 3,
                Name = "Текст"
            },
        ]);
        modelBuilder.Entity<ProductModel>().HasOne(p => p.PriceList).WithMany(c => c.Products).HasForeignKey(k => k.PriceListId);
        modelBuilder.Entity<ProductColumnValueModel>().HasOne<ProductModel>().WithMany(p => p.ColumnValues);
        modelBuilder.Entity<ProductColumnValueModel>().HasOne(pcv => pcv.Column).WithMany();
    }
}