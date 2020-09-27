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
    [Route("api/v1/category")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _categoryService.GetAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpGet("{id}/product")]
        public async Task<IActionResult> GetListProductsByCategory([FromRoute] int id)
        {
            var result = await _categoryService.GetAsync(filter: cate => cate.CategoryId == id, includeProperties: "Product");
            return Ok(result);
        }
        [HttpGet("paging")]
        public async Task<IActionResult> GetCategoriesByPageSize([FromQuery] CategoryModel categoryModel)
        {
            var result = await _categoryService.GetWithPagingAsync(pageIndex: categoryModel.PageIndex, pageSize: categoryModel.PageSize);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryModel categoryModel)
        {
            var result = await _categoryService.UpdateAsync(categoryModel);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _categoryService.DeleteAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryModel model)
        {
            var result = await _categoryService.CreateAsync(model);
            return Ok(result);
        }

    }
}
