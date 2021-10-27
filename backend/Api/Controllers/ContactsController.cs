using Application.Dto.Contact;
using Application.Services;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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

        [AllowAnonymous]
        [HttpPatch]
        public IActionResult Test()
        {
            var user = _context.Users
                .Include(u => u.Contacts).ThenInclude(c => c.ContactUsers).ThenInclude(cu => cu.User)
                .Include(u => u.ContactUsers).ThenInclude(cu => cu.Contact).ThenInclude(c => c.Creator)
                .Include(u => u.UnacceptedShares).ThenInclude(us => us.Contact).ThenInclude(c => c.Creator)
                .FirstOrDefault();

            return Ok(user.Contacts.First().ContactUsers);
        }
        //
        //
        //
        //
        //
        //

        [HttpGet("User/{userKey}/Contacts", Name = nameof(Get))]
        public async Task<ActionResult<ICollection<ContactResponse>>> Get([FromRoute] string userKey)
        {
            if (await _userManager.FindByIdAsync(userKey) == null)
                return StatusCode(StatusCodes.Status404NotFound);

            var contacts = await _contactsService.GetAsync(userKey);

            return Ok(contacts);
        }

        [HttpPost("User/{userKey}/Contacts/Create", Name = nameof(Create))]
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
            if (!await _contactsService.ExistsAsync(key))
                return StatusCode(StatusCodes.Status404NotFound);

            var updatedContact = await _contactsService.UpdateAsync(request);

            return Ok(updatedContact);
        }

        [HttpDelete("Contacts/{key}", Name = nameof(Delete))]
        public async Task<IActionResult> Delete([FromRoute] string key)
        {
            if (!await _contactsService.ExistsAsync(key))
                return StatusCode(StatusCodes.Status404NotFound);

            await _contactsService.DeleteAsync(key);

            return Ok();
        }
        // After =>
        // {
        // var unacceptedShares = _context.UnacceptedShares.Where(us => us.ContactId == key);
        // _context.UnacceptedShares.RemoveRange(unacceptedShares);
        // _context.SaveChanges();
        // }

        [HttpPost("Contacts/{contactKey}/ShareWith/{userKey}", Name = nameof(Share))]
        public async Task<IActionResult> Share([FromRoute] string contactKey, [FromRoute] string userKey)
        {
            //if (!await _contactsService.ExistsAsync(contactKey))
            //    return StatusCode(StatusCodes.Status404NotFound);

            //if (await _userManager.FindByIdAsync(userKey) == null)
            //    return StatusCode(StatusCodes.Status404NotFound);

            await _contactsService.ShareContact(contactKey, userKey);

            return Ok();
        }

        [HttpPost("Users/{userKey}/AcceptShare/{contactKey}", Name = nameof(AcceptShare))]
        public async Task<IActionResult> AcceptShare([FromRoute] string userKey, [FromRoute] string contactKey)
        {
            if (!await _contactsService.AcceptShare(contactKey, userKey))
                return StatusCode(StatusCodes.Status404NotFound);

            return Ok();
        }

        [HttpDelete("Users/{userKey}/RemoveShare/{contactKey}", Name = nameof(RemoveShare))]
        public async Task<IActionResult> RemoveShare([FromRoute] string userKey, [FromRoute] string contactKey)
        {
            if (!await _contactsService.RemoveShare(contactKey, userKey))
                return StatusCode(StatusCodes.Status404NotFound);

            return Ok();
        }
    }
}
