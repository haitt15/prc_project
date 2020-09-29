using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRC_Project.Data.Models;
using PRC_Project.Data.ViewModels;
using PRC_Project_Business.Services;

namespace PRC_Project.API.Controllers
{
    [Route("api/v1/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("{username}")]
        public async Task<IActionResult> OrderProducts(string username, [FromBody] IEnumerable<ProductModel> listProductModel)
        {
            var result = await _orderService.OrderProducts(listProductModel, username);
            if(result != null)
            {
                return Created("", result);
            }

            return BadRequest();
        }
    }
}
