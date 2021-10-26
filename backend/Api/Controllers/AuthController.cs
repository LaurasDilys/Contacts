using Application.Dto;
using Application.Services;
using Business.Interfaces.Dto;
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
        private readonly JwtTokenService _jwtTokenService;
        private readonly UserService _userService;
        private readonly MapperService _mapper;

        public AuthController(JwtTokenService jwtTokenService,
            UserService userService,
            MapperService mapper)
        {
            _jwtTokenService = jwtTokenService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost(nameof(Test))]
        public IActionResult Test()
        {
            return Ok(_jwtTokenService.NewCookieIsNecessary(Request.Cookies["token"]));
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

            return StatusCode(StatusCodes.Status201Created,
                "User created successfully.");
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<ActionResult<IUserResponse>> Login([FromBody] LoginRequest request)
        {
            if (!await _userService.UserNameAndPasswordAreValidAsync(request))
                return StatusCode(StatusCodes.Status403Forbidden,
                    "Check your details and try again.");

            var user = await _userService.FindByNameAsync(request.UserName);

            var jwtToken = _jwtTokenService.GenerateJwtToken(request.UserName, request.Remember);
            var cookieOptions = _jwtTokenService.GenerateCookieOptions(request.Remember);

            HttpContext.Response.Cookies.Append("token", jwtToken, cookieOptions);

            return Ok(_mapper.ResponseFrom(user));
        }

        [AllowAnonymous]
        [HttpPost(nameof(LoginStatus))]
        public async Task<ActionResult<IUserResponse>> LoginStatus()
        {
            var token = Request.Cookies["token"];

            if (token is null) return Ok("NotLoggedIn");

            var userName = _jwtTokenService.UserNameFromToken(token);

            var user = await _userService.FindByNameAsync(userName);

            _jwtTokenService.RefreshCookieIfNecessary(token, userName, HttpContext);

            return Ok(_mapper.ResponseFrom(user));
        }

        [HttpPost(nameof(NewCookie))]
        public IActionResult NewCookie()
        {
            var token = Request.Cookies["token"];

            if (token is null)
                return StatusCode(StatusCodes.Status403Forbidden,
                    "NotLoggedIn");

            _jwtTokenService.RefreshCookieIfNecessary(token, HttpContext);

            return Ok();
        }

        [HttpPost(nameof(Logout))]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Append("token", "",
                new CookieOptions
                {
                    Expires = DateTimeOffset.MinValue,
                    SameSite = SameSiteMode.None,
                    HttpOnly = true,
                    Secure = true,
                });

            return Ok();
        }
    }
}
