using System;
namespace Cs_ProductApi.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string image { get; set; }
        public int price { get; set; }
        public string category { get; set; }
    }
}
