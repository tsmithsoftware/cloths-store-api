using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothsStore.Api.Services;
using ClothsStore.BL.Models;
using ClothsStore.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ClothsStore.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private AuthenticationManager authManager;

        public LoginController(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            authManager = new AuthenticationManager();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();
            var user = authManager.AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = JwtManager.GenerateToken(user.Username);

                var claimsPrincipal = JwtManager.GetPrincipal(tokenString);

                response = Ok(new { token = tokenString });
            }

            return response;
        }
    }
}