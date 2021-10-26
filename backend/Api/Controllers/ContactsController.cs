using Application.Dto.Contact;
using Application.Services;
using Data.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsService _contactsService;
        private readonly UserManager<User> _userManager;

        public ContactsController(ContactsService contactsService, UserManager<User> userManager)
        {
            _contactsService = contactsService;
            _userManager = userManager;
        }

        [HttpGet("User/{userKey}/Contacts", Name = nameof(Get))]
        public async Task<ActionResult<ICollection<ContactResponse>>> Get([FromRoute] string userKey)
        {
            if (await _userManager.FindByIdAsync(userKey) == null)
                return StatusCode(StatusCodes.Status404NotFound);

            var contacts = await _contactsService.Get(userKey);

            return Ok(contacts);
        }

        [HttpPost("User/{userKey}/Contacts/Create", Name = nameof(Create))]
        public async Task<ActionResult<ContactResponse>> Create([FromRoute] string userKey, [FromBody] CreateContactRequest request)
        {
            if (await _userManager.FindByIdAsync(userKey) == null)
                return StatusCode(StatusCodes.Status404NotFound);

            var newContact = await _contactsService.Create(userKey, request);

            return StatusCode(StatusCodes.Status201Created, newContact);
        }

        [HttpPut("Contacts/{key}", Name = nameof(Update))]
        public async Task<ActionResult<ContactResponse>> Update([FromRoute] string key, [FromBody] UpdateContactRequest request)
        {
            if (!await _contactsService.Exists(key))
                return StatusCode(StatusCodes.Status404NotFound);

            var updatedContact = await _contactsService.Update(request);

            return Ok(updatedContact);
        }

        [HttpDelete("Contacts/{key}", Name = nameof(Delete))]
        public async Task<IActionResult> Delete([FromRoute] string key)
        {
            if (!await _contactsService.Exists(key))
                return StatusCode(StatusCodes.Status404NotFound);

            await _contactsService.Delete(key);

            return Ok();
        }
    }
}
