using System;
using Microsoft.AspNetCore.Mvc;

namespace Cs_ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //api/product
        //api/product/?name=name
        [HttpGet]
        public string GetProducts([FromQuery] string name)
        {
            return $"{name} :sing3demons";
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
