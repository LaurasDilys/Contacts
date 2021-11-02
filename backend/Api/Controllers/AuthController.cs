using Application.Dto.Authentication;
using Application.Dto.User;
using Application.Interfaces;
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
        private readonly IMapperService _mapper;

        public AuthController(JwtTokenService jwtTokenService,
            UsersService usersService,
            IMapperService mapper)
        {
            _jwtTokenService = jwtTokenService;
            _usersService = usersService;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="request">Register user request</param>
        /// <response code="201">Returns confirmation "User created successfully."</response>
        /// <response code="409">If user name is taken</response>
        /// <response code="403">If password does not meet the requirements</response>
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _usersService.NameExistsAsync(request.UserName))
                return StatusCode(StatusCodes.Status409Conflict,
                    "This user name is taken.");

            if (!await _usersService.CreateAsync(request))
                return StatusCode(StatusCodes.Status403Forbidden,
                    "Your password does not meet the requirements.");

            return StatusCode(StatusCodes.Status201Created,
                "User created successfully.");
        }

        /// <summary>
        /// Returns user information after login and adds a JWT token in the response cookie
        /// </summary>
        /// <returns>Information of logged in user</returns>
        /// <param name="request">Login request</param>
        /// <response code="200">Returns user, who's logged in</response>
        /// <response code="403">If username or password is incorrect</response>
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost(nameof(Login))]
        public async Task<ActionResult<UserResponse>> Login([FromBody] LoginRequest request)
        {
            if (!await _usersService.UserNameAndPasswordAreValidAsync(request.UserName, request.Password))
                return StatusCode(StatusCodes.Status403Forbidden,
                    "Check your details and try again.");

            var user = await _usersService.FindByNameAsync(request.UserName);

            var jwtToken = _jwtTokenService.GenerateJwtToken(request.UserName, request.Remember);
            var cookieOptions = _jwtTokenService.GenerateCookieOptions(request.Remember);

            HttpContext.Response.Cookies.Append("token", jwtToken, cookieOptions);

            return Ok(_mapper.UserResponseFrom(user));
        }

        /// <summary>
        /// Returns user information – if JWT token in request cookie is valid – and refreshes the JWT token, if necessary
        /// </summary>
        /// <returns>Information of logged in user or "NotLoggedIn" message</returns>
        /// <response code="200">Returns either user, who's logged in, or "NotLoggedIn" message</response>
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
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

        /// <summary>
        /// Checks, if JWT token in request cookie is valid, and refreshes the JWT token, if necessary – this Api call is made right before the JWT token is set to expire
        /// </summary>
        /// <response code="200">If JWT token in request cookie was valid</response>
        /// <response code="403">Returns "NotLoggedIn" message, if JWT token in request cookie was not valid</response>
        [HttpPost(nameof(NewCookie))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult NewCookie()
        {
            var token = Request.Cookies["token"];

            if (token is null)
                return StatusCode(StatusCodes.Status403Forbidden,
                    "NotLoggedIn");

            _jwtTokenService.RefreshCookieIfNecessary(token, HttpContext);

            return Ok();
        }

        /// <summary>
        /// Logs out
        /// </summary>
        /// <response code="200">Replaces the JWT token cookie with one that immediately expires</response>
        [HttpPost(nameof(Logout))]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
