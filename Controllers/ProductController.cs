using System;
using System.Collections.Generic;
using System.Linq;
using Cs_ProductApi.Database;
using Cs_ProductApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs_ProductApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

    private readonly DatabaseContext DatabaseContext;
    public ProductController(DatabaseContext db)
    {
        DatabaseContext = db;
    }


    //api/product
    //api/product/?name=name
    [HttpGet]
    public IEnumerable<Product> GetProducts([FromQuery] string name)
    {
        var data = DatabaseContext.Products.ToList();
            return data;
    }

        //api/product/:value
        [HttpGet("{name}/{age}")]
        public string GetByValue(string name, int age)
        {
            return $"Hello {name} Age: {age}";
        }

        //queryString
        [HttpGet("query")]
        public string GetQuery([FromQuery] string name)
        {
            return $"Hello {name} ";
        }
    }
}
