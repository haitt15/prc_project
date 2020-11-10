using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PRC_Project.Data.Helper;
using PRC_Project.Data.ViewModels;
using PRC_Project_Business.Services;
using PRC_Project_Business.Services.Authenticate;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PRC_Project.API.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IAuthenticateService _authService;

        private readonly IConfiguration _config;

        public AuthController(IUserService userService, IRoleService roleService, IConfiguration config, IAuthenticateService authService)
        {
            _userService = userService;
            _roleService = roleService;
            _authService = authService;
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
                var firebaseProject = _config.GetSection("AppSettings:FirebaseProject").Value;
                var token = new JwtSecurityToken(
                    issuer: "https://securetoken.google.com/" + firebaseProject,
                    audience: firebaseProject,
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

        [HttpPost("Google")]
        public async Task<IActionResult> LoginGoogle(UserModelRequestParam login)
        {

            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(login.Token);
            if (decodedToken != null)
            {
                string uid = decodedToken.Uid;
                UserModel user = await _authService.LoginGoogle(uid);


                var authClaims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, Constants.Roles.ROLE_USER)
                    };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                var firebaseProject = _config.GetSection("AppSettings:FirebaseProject").Value;
                var token = new JwtSecurityToken(
                    issuer: "https://securetoken.google.com/" + firebaseProject,
                    audience: firebaseProject,
                    expires: DateTime.Now.AddYears(13),
                    claims: authClaims,
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    role = Constants.Roles.ROLE_USER,
                    email = user.Email,
                    fullName = user.FullName,
                    username = user.Username,
                    photo = user.Photo,
                    expiration = token.ValidTo
                });
            }
            return BadRequest();
        }
    }
}
