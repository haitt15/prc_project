using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PRC_Project.Data.ViewModels;
using PRC_Project_Business.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PRC_Project.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IConfiguration _config;

        public AuthController(IUserService userService, IRoleService roleService, IConfiguration config)
        {
            _userService = userService;
            _roleService = roleService;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _userService.CreateUser(model, model.Password);
            if (result != null)
            {
                return Created("", result);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userService.GetByUserName(model.Username);
            var result = await _userService.CheckPassWord(model.Username, model.Password);
            if (user != null && result)
            {
                var role = _roleService.GetRole(user);

                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role)
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                var apiUrl = _config.GetSection("AppSettings:Url").Value;
                var token = new JwtSecurityToken(
                    issuer: apiUrl,
                    audience: apiUrl,
                    expires: DateTime.Now.AddYears(13),
                    claims: authClaims,
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    role = role,
                    email = user.Email,
                    fullName = user.FullName,
                    username = user.Username,
                    photo = user.Photo,
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
    }
}
