using Application.Dto;
using Application.Services;
using Business.Interfaces.Models;
using Business.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IUserService userService, IJwtTokenService jwtTokenService)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost(nameof(Test))]
        public IActionResult Test()
        {
            return Ok(DateTime.Now);
        }

        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _userService.ExistsAsync(request.UserName))
                return StatusCode(StatusCodes.Status409Conflict,
                    "This user name is taken.");

            if (!await _userService.CreateAsync(request))
                return StatusCode(StatusCodes.Status403Forbidden,
                    "Your password does not meet the requirements.");

            return Ok("User created successfully.");
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!await _userService.UserNameAndPasswordAreValidAsync(request))
                return StatusCode(StatusCodes.Status403Forbidden,
                    "Check your details and try again.");

            var jwtToken = _jwtTokenService.GenerateJwtToken(request);
            var cookieOptions = _jwtTokenService.GenerateCookieOptions(request);

            HttpContext.Response.Cookies.Append("token", jwtToken, cookieOptions);

            return Ok(request.UserName);
        }
    }
}
