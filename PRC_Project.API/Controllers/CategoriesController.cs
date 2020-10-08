using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAll(filter: f => f.DelFlg == false).ToListAsync();
            return Ok(result);
        }

        [HttpGet("{categoryId}/products")]
        public async Task<IActionResult> GetListProductsByCategory([FromRoute] string categoryId)
        {
            var result = await _categoryService.GetAsync(pageIndex: 1, pageSize: 10, filter: cate => cate.CategoryId == categoryId, includeProperties: "Product");
            return Ok(result);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetById([FromRoute] string categoryId)
        {
            var result = await _categoryService.GetByIdAsync(categoryId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] SearchCategoryModel model)
        {
            var result = await _categoryService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize, filter: x => x.DelFlg == false);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("products")]
        public IActionResult Get()
        {
            var result = _categoryService.GetAll(filter: cate => cate.DelFlg == false, includeProperties: "Product");
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryModel categoryModel)
        {
            var result = await _categoryService.UpdateAsync(categoryModel);
            return Ok(result);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> Delete(string categoryId)
        {
            var result = await _categoryService.DeleteAsync(categoryId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryModel model)
        {
            var result = await _categoryService.CreateAsync(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

    }
}
