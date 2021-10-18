using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
    }

    [ApiController]
    [Route("api/")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AuthController : ControllerBase
    {
        public AuthController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [HttpPost(nameof(Test))]
        public IActionResult Test()
        {
            return Ok(DateTime.Now);
        }

        //[HttpPost]
        //[Route("login")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login([FromBody] LoginRequest model)
        //{
        //    var authClaims = new List<Claim>
        //    {
        //        new Claim("username", model.Username),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //    };

        //    if (model.Remember)
        //        authClaims.Add(new Claim("rememberMe", "true"));

        //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"]));

        //    var expirationTime = model.Remember ? DateTime.Now.AddDays(30) : DateTime.Now.AddSeconds(5);

        //    var token = new JwtSecurityToken(
        //        expires: expirationTime,
        //        claims: authClaims,
        //        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //    );

        //    var cookieOptions = new CookieOptions
        //    {
        //        SameSite = SameSiteMode.None,
        //        HttpOnly = true,
        //        Secure = true,
        //        Expires = expirationTime,
        //    };

        //    HttpContext.Response.Cookies.Append("token", new JwtSecurityTokenHandler().WriteToken(token), cookieOptions);

        //    return Ok(model.Username);
        //}

        [HttpPost(nameof(Login))]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            GenerateJwtToken(model);

            return Ok(model.Username);
        }

        private void GenerateJwtToken(LoginRequest model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //
            var key = Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"]);
            //
            List<Claim> claims = new List<Claim>
            {
                //
                new Claim("username", model.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                //
            };

            if (model.Remember)
                claims.Add(new Claim("rememberMe", "true"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                //
                Expires = model.Remember ? DateTime.Now.AddDays(1) : DateTime.Now.AddSeconds(5),
                //
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                //
                Expires = model.Remember ? DateTime.Now.AddDays(1) : DateTime.Now.AddSeconds(5)
            };

            HttpContext.Response.Cookies.Append("token", tokenHandler.WriteToken(token), cookieOptions);
        }
    }
}
