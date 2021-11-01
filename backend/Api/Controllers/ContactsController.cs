using Application.Dto.Contact;
using Application.Services;
using Data;
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
        private readonly UsersService _usersService;

        public ContactsController(ContactsService contactsService, UsersService usersService)
        {
            _contactsService = contactsService;
            _usersService = usersService;
        }

        [HttpGet("Users/{userKey}/Contacts", Name = nameof(GetContacts))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<ContactResponse>>> GetContacts([FromRoute] string userKey)
        {
            if (await _usersService.FindByIdAsync(userKey) == null)
                return StatusCode(StatusCodes.Status404NotFound);

            var contacts = await _contactsService.GetAllContactsAsync(userKey);

            return Ok(contacts);
        }

        [HttpPost("Users/{userKey}/Contacts", Name = nameof(CreateContact))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ContactResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactResponse>> CreateContact([FromRoute] string userKey, [FromBody] CreateContactRequest request)
        {
            if (await _usersService.FindByIdAsync(userKey) == null)
                return StatusCode(StatusCodes.Status404NotFound);

            var newContact = await _contactsService.CreateAsync(userKey, request);

            return StatusCode(StatusCodes.Status201Created, newContact);
        }

        [HttpPut("Contacts/{key}", Name = nameof(UpdateContact))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ContactResponse>> UpdateContact([FromRoute] string key, [FromBody] UpdateContactRequest request)
        {
            var contact = await _contactsService.FindByIdAsync(key);

            if (contact == null) return StatusCode(StatusCodes.Status404NotFound);

            if (contact.Me) return StatusCode(StatusCodes.Status403Forbidden,
                "Personal contact information can only be changed by updating user.");

            var updatedContact = await _contactsService.UpdateAsync(request);

            return Ok(updatedContact);
        }

        [HttpDelete("Contacts/{key}", Name = nameof(DeleteContact))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteContact([FromRoute] string key)
        {
            var contact = await _contactsService.FindByIdAsync(key);

            if (contact == null) return StatusCode(StatusCodes.Status404NotFound);

            if (contact.Me) return StatusCode(StatusCodes.Status403Forbidden,
                "Personal contact information must be accessed through user.");

            await _contactsService.DeleteAsync(key);

            return Ok();
        }

        [HttpPost("Contacts/{contactKey}/ShareWith/{userKey}", Name = nameof(Share))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactResponse>> Share([FromRoute] string contactKey, [FromRoute] string userKey)
        {
            if (await _contactsService.FindByIdAsync(contactKey) == null)
                return StatusCode(StatusCodes.Status404NotFound);

            if (await _usersService.FindByIdAsync(userKey) == null)
                return StatusCode(StatusCodes.Status404NotFound);

            return Ok(await _contactsService.ShareContact(contactKey, userKey));
        }

        [HttpPost("Users/{userKey}/AcceptShare/{contactKey}", Name = nameof(AcceptShare))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactResponse>> AcceptShare([FromRoute] string userKey, [FromRoute] string contactKey)
        {
            var response = await _contactsService.AcceptSharedContact(contactKey, userKey);

            if (response == null) return StatusCode(StatusCodes.Status404NotFound);

            return Ok(response);
        }

        [HttpDelete("Users/{userKey}/DeclineShare/{contactKey}", Name = nameof(DeclineShare))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeclineShare([FromRoute] string userKey, [FromRoute] string contactKey)
        {
            var response = await _contactsService.StopSharingContact(contactKey, userKey);

            if (response == null) return StatusCode(StatusCodes.Status404NotFound);

            return Ok();
        }

        [HttpDelete("Contacts/{contactKey}/StopSharingWith/{userKey}", Name = nameof(StopSharing))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactResponse>> StopSharing([FromRoute] string contactKey, [FromRoute] string userKey)
        {
            var response = await _contactsService.StopSharingContact(contactKey, userKey);

            if (response == null) return StatusCode(StatusCodes.Status404NotFound);

            return Ok(response);
        }
    }
}
