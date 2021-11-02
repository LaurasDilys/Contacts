using Application.Dto.User;
using Application.Services;
using Business.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly UsersService _usersService;

        public UsersController(JwtTokenService jwtTokenService, UsersService usersService)
        {
            _jwtTokenService = jwtTokenService;
            _usersService = usersService;
        }

        /// <summary>
        /// Returns all users, excluding the one who's key is provided in route
        /// </summary>
        /// <returns>A collection of all users – only their basic information</returns>
        /// <param name="userKey">User key</param>
        /// <response code="200">Returns all users</response>
        [HttpGet("OtherThan/{userKey}", Name = nameof(OtherThan))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<UserBasic>))]
        public async Task<ActionResult<ICollection<UserBasic>>> OtherThan(string userKey)
        {
            return Ok(await _usersService.GetOtherUsersAsync(userKey));
        }

        /// <summary>
        /// Updates user information
        /// </summary>
        /// <returns>Updated user and their personal contact – which is null, if ShowMyContact is false</returns>
        /// <param name="userKey">User key</param>
        /// <param name="request">Update user request</param>
        /// <response code="200">Returns updated user and their personal contact</response>
        /// <response code="404">If user could not be found by key</response>
        /// <response code="409">If keys in route and body don't match</response>
        [HttpPut("{userKey}", Name = nameof(UpdateUser))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUserResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UpdateUserResponse>> UpdateUser([FromRoute] string userKey, [FromBody] UpdateUserRequest request)
        {
            if (userKey != request.Id)
                return StatusCode(StatusCodes.Status409Conflict,
                    "Keys in route and body don't match.");

            if (await _usersService.FindByIdAsync(userKey) == null)
                return StatusCode(StatusCodes.Status404NotFound);

            var response = await _usersService.UpdateUser(request);

            return Ok(response);
        }

        /// <summary>
        /// Changes user's password
        /// </summary>
        /// <param name="userKey">User key</param>
        /// <param name="request">Change password request</param>
        /// <response code="200">Returns confimation "User password updated successfully."</response>
        /// <response code="403">If user is not logged in, or provided data doesn't match user information, or new password does not meet the requirements.</response>            
        [HttpPost("{userKey}/ChangePassword", Name = nameof(ChangePassword))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ChangePassword([FromRoute] string userKey, [FromBody] ChangePasswordRequest request)
        {
            var token = Request.Cookies["token"];

            if (token is null) return StatusCode(StatusCodes.Status403Forbidden,
                    "You are not logged in.");

            var userName = _jwtTokenService.UserNameFromToken(token);

            var user = await _usersService.FindByNameAsync(userName);

            if (!await _usersService.UserNameAndPasswordAreValidAsync(user, request.CurrentPassword) || userKey != user.Id)
                return StatusCode(StatusCodes.Status403Forbidden,
                    "Provided data doesn't match user information.");
            
            if (!await _usersService.ChangeUserPassword(user, request.NewPassword))
                return StatusCode(StatusCodes.Status403Forbidden,
                    "New password does not meet the requirements.");

            return Ok("User password updated successfully.");
        }
    }
}
