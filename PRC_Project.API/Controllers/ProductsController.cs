using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRC_Project.Data.ViewModels;
using PRC_Project_Business.Services;

namespace PRC_Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var result = await _productService.GetAsync(filter: f => f.DelFlg == false, page: 2);
        //    return Ok(result);
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductModel model)
        {
            var result = await _productService.CreateAsync(model);
            return Ok(result);
        }
    }
}
