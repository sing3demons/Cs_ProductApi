using Cs_ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Cs_ProductApi.Database
{
    public class DatabaseContext: DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options){}

    public  DbSet<Product> Products { get; set; }
    public  DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<Category>(c => {
                c.Property(u => u.productId).HasColumnName("product_id");
            });

            //Default value
            modelBuilder.Entity<Product>(b =>
        {
            //b.Property(p => p.Timestamp).HasDefaultValueSql("(getdate())");
            b.Property(p => p.name).HasDefaultValue("unknows");
        });
        }

        //Data Type
        //    modelBuilder.Entity<Product>(b =>
        //    {
        //        b.Property(p => p.Name).HasColumnType("varchar(200)");
        //    });
        //}
    }
}
