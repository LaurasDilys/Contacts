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

        [HttpGet("OtherThan/{key}", Name = nameof(OtherThan))]
        public async Task<ActionResult<ICollection<UserBasic>>> OtherThan(string key)
        {
            return Ok(await _usersService.GetOtherUsersAsync(key));
        }

        [HttpPut("{key}", Name = nameof(UpdateUser))]
        public async Task<ActionResult<UpdateUserResponse>> UpdateUser([FromRoute] string key, [FromBody] UpdateUserRequest request)
        {
            if (await _usersService.FindByIdAsync(key) == null)
                return StatusCode(StatusCodes.Status404NotFound);

            var response = await _usersService.UpdateUser(request);

            return Ok(response);
        }

        [HttpPost("{key}/ChangePassword", Name = nameof(ChangePassword))]
        public async Task<IActionResult> ChangePassword([FromRoute] string key, [FromBody] ChangePasswordRequest request)
        {
            var token = Request.Cookies["token"];

            if (token is null) return StatusCode(StatusCodes.Status403Forbidden,
                    "You are not logged in.");

            var userName = _jwtTokenService.UserNameFromToken(token);

            var user = await _usersService.FindByNameAsync(userName);

            if (!await _usersService.UserNameAndPasswordAreValidAsync(user, request.CurrentPassword) || key != user.Id)
                return StatusCode(StatusCodes.Status403Forbidden,
                    "Provided data doesn't match user information.");
            
            if (!await _usersService.ChangeUserPassword(user, request.NewPassword))
                return StatusCode(StatusCodes.Status403Forbidden,
                    "New password does not meet the requirements.");

            return Ok("User password updated successfully.");
        }
    }
}
