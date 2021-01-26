using System;
using Microsoft.AspNetCore.Mvc;

namespace Cs_ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public string getProducts()
        {
            return "Hello";
        }
    }
}
