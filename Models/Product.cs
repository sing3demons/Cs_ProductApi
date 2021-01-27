using System;
namespace Cs_ProductApi.Models
{
   
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int CategoryID { get; set; } // Relation [Key]
        public Category ProductCategory { get; set; }

      
    }
}
