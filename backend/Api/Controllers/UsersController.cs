using Application.Dto.User;
using Application.Services;
using Business.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("OtherThan/{key}", Name = nameof(OtherThan))]
        public async Task<ActionResult<ICollection<UserBasic>>> OtherThan(string key)
        {
            return Ok(await _usersService.GetOtherUsersAsync(key));
        }

        [HttpPut("{key}", Name = nameof(Update))]
        public async Task<ActionResult<UpdateUserResponse>> Update([FromRoute] string key, [FromBody] UpdateUserRequest request)
        {
            if (await _usersService.FindByIdAsync(key) == null)
                return StatusCode(StatusCodes.Status404NotFound);

            var response = _usersService.UpdateUser(request);

            return Ok(response);
        }
    }
}
