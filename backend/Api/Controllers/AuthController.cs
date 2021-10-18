using Application.Dto;
using Application.Services;
using Business.Interfaces.Services;
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
    [ApiController]
    [Route("api/")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost(nameof(Test))]
        public IActionResult Test()
        {
            return Ok(DateTime.Now);
        }
        
        [HttpPost(nameof(Login))]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var jwtToken = _jwtTokenService.GenerateJwtToken(request);
            var cookieOptions = _jwtTokenService.GenerateCookieOptions(request);

            HttpContext.Response.Cookies.Append("token", jwtToken, cookieOptions);

            return Ok(request.Username);
        }
    }
}
