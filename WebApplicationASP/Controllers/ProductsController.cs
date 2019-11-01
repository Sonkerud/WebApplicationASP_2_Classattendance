using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplicationASP.Models;
using WebApplicationASP_2.Services;

namespace WebApplicationASP_2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController(JsonFileProductService productService)
        {
            this.ProductService = productService;
        }

        public JsonFileProductService ProductService { get; }

        public IEnumerable<Student> Get()
        {
            return ProductService.GetProducts();
        }


        [Route("Rate")]
        [HttpGet]
        public ActionResult Get([FromQuery]string ProductId, 
                                [FromQuery ]int Rating)
        {
            //ProductService.AddRating(ProductId, Rating);
            return Ok();
        }
    }
}