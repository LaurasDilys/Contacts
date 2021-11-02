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

        /// <summary>
        /// Gets all user's contacts
        /// </summary>
        /// <returns>All user's contacts</returns>
        /// <param name="userKey">User key</param>
        /// <response code="200">Returns user's contacts</response>
        /// <response code="404">If user could not be found by key</response>
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

        /// <summary>
        /// Creates new contact
        /// </summary>
        /// <returns>Newly created contact</returns>
        /// <param name="userKey">User key</param>
        /// <param name="request">Create contact request</param>
        /// <response code="201">Returns the newly created contact</response>
        /// <response code="404">If user could not be found by key</response>
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

        /// <summary>
        /// Updates a contact
        /// </summary>
        /// <returns>Updated contact</returns>
        /// <param name="contactKey">Contact key</param>
        /// <param name="request">Update contact request</param>
        /// <response code="200">Returns the updated contact</response>
        /// <response code="404">If contact could not be found by key</response>
        /// <response code="403">If attempting to update user's personal contact – which can only be changed by updating the user</response>
        /// <response code="409">If keys in route and body don't match</response>
        [HttpPut("Contacts/{contactKey}", Name = nameof(UpdateContact))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ContactResponse>> UpdateContact([FromRoute] string contactKey, [FromBody] UpdateContactRequest request)
        {
            if (contactKey != request.Id)
                return StatusCode(StatusCodes.Status409Conflict,
                    "Keys in route and body don't match.");

            var contact = await _contactsService.FindByIdAsync(contactKey);

            if (contact == null) return StatusCode(StatusCodes.Status404NotFound);

            if (contact.Me) return StatusCode(StatusCodes.Status403Forbidden,
                "Personal contact information can only be changed by updating user.");

            var updatedContact = await _contactsService.UpdateAsync(request);

            return Ok(updatedContact);
        }

        /// <summary>
        /// Deleted a contact
        /// </summary>
        /// <param name="contactKey">Contact key</param>
        /// <response code="200">Returns confimation "Contact has been deleted."</response>
        /// <response code="404">If contact could not be found by key</response>
        /// <response code="403">If attempting to delete user's personal contact – which can only be hidden (not retrieved, if ShowMyContact is false) and can only be changed by updating the user</response>
        [HttpDelete("Contacts/{contactKey}", Name = nameof(DeleteContact))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteContact([FromRoute] string contactKey)
        {
            var contact = await _contactsService.FindByIdAsync(contactKey);

            if (contact == null) return StatusCode(StatusCodes.Status404NotFound);

            if (contact.Me) return StatusCode(StatusCodes.Status403Forbidden,
                "Personal contact information must be accessed through user.");

            await _contactsService.DeleteAsync(contactKey);

            return Ok("Contact has been deleted.");
        }

        /// <summary>
        /// Shares a contact
        /// </summary>
        /// <returns>Newly shared contact</returns>
        /// <param name="contactKey">Contact key</param>
        /// <param name="userKey">User key</param>
        /// <response code="200">Returns the newly shared contact</response>
        /// <response code="404">If contact or user could not be found by their keys</response>
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

        /// <summary>
        /// Accepts shared contact – removes from UnacceptedShares and adds to ContactUsers
        /// </summary>
        /// <returns>Newly accepted contact</returns>
        /// <param name="contactKey">Contact key</param>
        /// <param name="userKey">User key</param>
        /// <response code="200">Returns the newly accepted contact</response>
        /// <response code="404">If UnacceptedShare could not be found by the contact and user keys</response>
        [HttpPost("Users/{userKey}/AcceptShare/{contactKey}", Name = nameof(AcceptShare))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactResponse>> AcceptShare([FromRoute] string userKey, [FromRoute] string contactKey)
        {
            var response = await _contactsService.AcceptSharedContact(contactKey, userKey);

            if (response == null) return StatusCode(StatusCodes.Status404NotFound);

            return Ok(response);
        }

        /// <summary>
        /// Declines shared contact – removes from UnacceptedShares
        /// </summary>
        /// <param name="contactKey">Contact key</param>
        /// <param name="userKey">User key</param>
        /// <response code="200">Returns confimation "Contact has been removed."</response>
        /// <response code="404">If UnacceptedShare could not be found by the contact and user keys</response>
        [HttpDelete("Users/{userKey}/DeclineShare/{contactKey}", Name = nameof(DeclineShare))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeclineShare([FromRoute] string userKey, [FromRoute] string contactKey)
        {
            var response = await _contactsService.StopSharingContact(contactKey, userKey);

            if (response == null) return StatusCode(StatusCodes.Status404NotFound);

            return Ok("Contact has been removed.");
        }

        /// <summary>
        /// Stops sharing contact – removes from UnacceptedShares or from ContactUsers (whichever may apply)
        /// </summary>
        /// <returns>The contact, that's no longer shared with the provided user</returns>
        /// <param name="contactKey">Contact key</param>
        /// <param name="userKey">User key</param>
        /// <response code="200">Returns the contact with one less SharedWith user</response>
        /// <response code="404">If neithet UnacceptedShare, nor ContactUser could be found by the contact and user keys</response>
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
