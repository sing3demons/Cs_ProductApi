using System;
using System.Linq;
using Cs_ProductApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cs_ProductApi.Database
{
    public class DatabaseInit
    {
        public static void INIT(IServiceProvider ServiceProvider)
        {
            var context = new DatabaseContext(ServiceProvider.GetRequiredService<DbContextOptions<DatabaseContext>>());

            // If database does not exist then the database and all its schema are created
            context.Database.EnsureCreated();
            //context.Database.EnsureDeleted();

            InsertData(context);
        }

        private static void InsertData(DatabaseContext context)
        {
            // If category table has data, it will return.
            if (context.Categories.Any())
            {
                return;
            }

            context.Categories.Add(new Category
            {
                Name = "IT"
               
            });
            context.SaveChanges();

           
            context.SaveChanges();

            context.Products.Add(new Product
            {
                Name = "MacBook",
                Desc = "2.0GHz Intel Core i5 Quad-Core Processor with Intel Iris Plus Graphics 512GB Storage",
                Image = "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/mbp13touch-space-select-202005?wid=904&hei=840&fmt=jpeg&qlt=80&op_usm=0.5,0.5&.v=1587460552755",
                Price = 59990,
                CategoryID = 1,
                

            });
            context.SaveChanges();
        }
    }
}
