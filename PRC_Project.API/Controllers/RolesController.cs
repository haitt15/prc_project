using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PRC_Project.Data.ViewModels;
using PRC_Project_Business.Services;

namespace PRC_Project.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleModel model)
        {
            var result = await _roleService.CreateAsync(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }
    }
}
