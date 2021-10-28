using Application.Dto.Authentication;
using Application.Dto.User;
using Application.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly UsersService _usersService;
        private readonly MapperService _mapper;

        public AuthController(JwtTokenService jwtTokenService,
            UsersService usersService,
            MapperService mapper)
        {
            _jwtTokenService = jwtTokenService;
            _usersService = usersService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _usersService.ExistsAsync(request.UserName))
                return StatusCode(StatusCodes.Status409Conflict,
                    "This user name is taken.");

            if (!await _usersService.CreateAsync(request))
                return StatusCode(StatusCodes.Status403Forbidden,
                    "Your password does not meet the requirements.");

            return StatusCode(StatusCodes.Status201Created,
                "User created successfully.");
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<ActionResult<UserResponse>> Login([FromBody] LoginRequest request)
        {
            if (!await _usersService.UserNameAndPasswordAreValidAsync(request))
                return StatusCode(StatusCodes.Status403Forbidden,
                    "Check your details and try again.");

            var user = await _usersService.FindByNameAsync(request.UserName);

            var jwtToken = _jwtTokenService.GenerateJwtToken(request.UserName, request.Remember);
            var cookieOptions = _jwtTokenService.GenerateCookieOptions(request.Remember);

            HttpContext.Response.Cookies.Append("token", jwtToken, cookieOptions);

            return Ok(_mapper.UserResponseFrom(user));
        }

        [AllowAnonymous]
        [HttpPost(nameof(LoginStatus))]
        public async Task<ActionResult<UserResponse>> LoginStatus()
        {
            var token = Request.Cookies["token"];

            if (token is null) return Ok("NotLoggedIn");

            var userName = _jwtTokenService.UserNameFromToken(token);

            var user = await _usersService.FindByNameAsync(userName);

            _jwtTokenService.RefreshCookieIfNecessary(token, userName, HttpContext);

            return Ok(_mapper.UserResponseFrom(user));
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
