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
        private readonly UserManager<User> _userManager;
        //
        //
        //
        private readonly DataContext _context;
        //
        //
        //

        public ContactsController(ContactsService contactsService, UserManager<User> userManager, DataContext context)
        {
            _contactsService = contactsService;
            _userManager = userManager;
            //
            //
            //
            _context = context;
            //
            //
            //
        }

        //[AllowAnonymous]
        //[HttpHead]
        //public IActionResult Test()
        //{
        //    var user = _context.Users
        //        .Include(u => u.Contacts).ThenInclude(c => c.ContactUsers).ThenInclude(cu => cu.User)
        //        .Include(u => u.ContactUsers).ThenInclude(cu => cu.Contact).ThenInclude(c => c.Creator)
        //        .Include(u => u.UnacceptedShares).ThenInclude(us => us.Contact).ThenInclude(c => c.Creator)
        //        .FirstOrDefault();

        //    return Ok(user.Contacts.First().ContactUsers);
        //}
        //
        //
        //
        //
        //
        //

        [HttpGet("Users/{userKey}/Contacts", Name = nameof(Get))]
        public async Task<ActionResult<ICollection<ContactResponse>>> Get([FromRoute] string userKey)
        {
            if (await _userManager.FindByIdAsync(userKey) == null)
                return StatusCode(StatusCodes.Status404NotFound);

            var contacts = await _contactsService.GetAllContactsAsync(userKey);

            return Ok(contacts);
        }

        [HttpPost("Users/{userKey}/Contacts", Name = nameof(Create))]
        public async Task<ActionResult<ContactResponse>> Create([FromRoute] string userKey, [FromBody] CreateContactRequest request)
        {
            if (await _userManager.FindByIdAsync(userKey) == null)
                return StatusCode(StatusCodes.Status404NotFound);

            var newContact = await _contactsService.CreateAsync(userKey, request);

            return StatusCode(StatusCodes.Status201Created, newContact);
        }

        [HttpPut("Contacts/{key}", Name = nameof(Update))]
        public async Task<ActionResult<ContactResponse>> Update([FromRoute] string key, [FromBody] UpdateContactRequest request)
        {
            var contact = await _contactsService.FindByIdAsync(key);

            if (contact == null) return StatusCode(StatusCodes.Status404NotFound);

            if (contact.Me) return StatusCode(StatusCodes.Status403Forbidden,
                "Personal contact information must be accessed through user.");

            var updatedContact = await _contactsService.UpdateAsync(request);

            return Ok(updatedContact);
        }

        [HttpDelete("Contacts/{key}", Name = nameof(Delete))]
        public async Task<IActionResult> Delete([FromRoute] string key)
        {
            var contact = await _contactsService.FindByIdAsync(key);

            if (contact == null) return StatusCode(StatusCodes.Status404NotFound);

            if (contact.Me) return StatusCode(StatusCodes.Status403Forbidden,
                "Personal contact information must be accessed through user.");

            await _contactsService.DeleteAsync(key);

            return Ok();
        }

        [HttpPost("Contacts/{contactKey}/ShareWith/{userKey}", Name = nameof(Share))]
        public async Task<ActionResult<ContactResponse>> Share([FromRoute] string contactKey, [FromRoute] string userKey)
        {
            //if (await _contactsService.FindByIdAsync(contactKey) == null)
            //    return StatusCode(StatusCodes.Status404NotFound);

            //if (await _userManager.FindByIdAsync(userKey) == null)
            //    return StatusCode(StatusCodes.Status404NotFound);

            return Ok(await _contactsService.ShareContact(contactKey, userKey));
        }

        [HttpPost("Users/{userKey}/AcceptShare/{contactKey}", Name = nameof(AcceptShare))]
        public async Task<IActionResult> AcceptShare([FromRoute] string userKey, [FromRoute] string contactKey)
        {
            var response = await _contactsService.AcceptSharedContact(contactKey, userKey);

            if (response == null) return StatusCode(StatusCodes.Status404NotFound);

            return Ok(response);
        }

        [HttpDelete("Users/{userKey}/DeclineShare/{contactKey}", Name = nameof(DeclineShare))]
        public async Task<IActionResult> DeclineShare([FromRoute] string userKey, [FromRoute] string contactKey)
        {
            var response = await _contactsService.StopSharingContact(contactKey, userKey);

            if (response == null) return StatusCode(StatusCodes.Status404NotFound);

            return Ok();
        }

        [HttpDelete("Contacts/{contactKey}/StopSharingWith/{userKey}", Name = nameof(StopSharing))]
        public async Task<ActionResult<ContactResponse>> StopSharing([FromRoute] string contactKey, [FromRoute] string userKey)
        {
            var response = await _contactsService.StopSharingContact(contactKey, userKey);

            if (response == null) return StatusCode(StatusCodes.Status404NotFound);

            return Ok(response);
        }
    }
}
