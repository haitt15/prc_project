using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRC_Project.Data.ViewModels;
using PRC_Project_Business.Services;

namespace PRC_Project.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAll(filter: f => f.DelFlg == false).ToListAsync();
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] SearchProductModel model)
        {
            var result = await _productService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize, filter: f => f.DelFlg == false);
            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(string productId)
        {
            var result = await _productService.GetByIdAsync(productId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductModel model)
        {
            var result = await _productService.CreateAsync(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(string productId)
        {
            var result = await _productService.DeleteAsync(productId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductModel model)
        {
            var result = await _productService.UpdateAsync(model);
            return Ok(result);
        }
    
    }
}
