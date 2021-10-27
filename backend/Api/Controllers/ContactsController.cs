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
        private readonly DataContext _context;

        public ContactsController(ContactsService contactsService, UserManager<User> userManager, DataContext context)
        {
            _contactsService = contactsService;
            _userManager = userManager;
            _context = context;
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
        // After =>
        // {
        // var unacceptedShares = _context.UnacceptedShares.Where(us => us.ContactId == key);
        // _context.UnacceptedShares.RemoveRange(unacceptedShares);
        // _context.SaveChanges();
        // }

        private readonly string userId = "b4a5fe29-e471-4f81-bf41-65d353d824a8";
        private readonly string contactId = "545cbdde-4b76-428d-97c1-dc72a443b6bb";

        [AllowAnonymous]
        [HttpPatch(nameof(Share))]
        public IActionResult Share()
        {
            var firstUser = _context.Users.First();
            var firstContact = _context.Contacts.First();

            var firstShare = new ContactUser
            {
                ContactId = firstContact.Id,
                UserId = firstUser.Id
            };

            _context.ContactUsers.Add(firstShare);
            _context.Users.Include(u => u.UnacceptedShares)
                .FirstOrDefault(u => u.Id == firstUser.Id)
                .UnacceptedShares.Add(new UnacceptedShare
            {
                Id = Guid.NewGuid().ToString(),
                ContactId = firstContact.Id
                });

            _context.SaveChanges();

            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch(nameof(Accept))]
        public IActionResult Accept()
        {
            AcceptOrDecline();

            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var allContacts = new List<C>();

            var lauras = _context.Users
                .Include(u => u.Contacts)
                .Include(u => u.UnacceptedShares)
                .Include(u => u.ContactUsers).ThenInclude(cu => cu.Contact)
                .FirstOrDefault(u => u.Id == userId);

            var contacts = lauras.Contacts;
            foreach (var c in contacts)
            {
                allContacts.Add(new C
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName
                });
            }

            if (lauras.ShowMyContact)
            {
                allContacts.Add(new C
                {
                    FirstName = lauras.FirstName,
                    LastName = "(me)"
                });
            }

            var received = lauras.ContactUsers.Select(cu => cu.Contact);
            var unaccepted = lauras.UnacceptedShares.Select(us => us.ContactId);
            if (unaccepted.Count() > 0)
            {
                foreach (var c in received)
                {
                    if (unaccepted.Any(id => id == c.Id))
                    {
                        allContacts.Add(new C
                        {
                            FirstName = c.FirstName,
                            LastName = "(unaccepted)"
                        });
                    }
                    else
                    {
                        allContacts.Add(new C
                        {
                            FirstName = c.FirstName,
                            LastName = c.LastName
                        });
                    }
                }
            }
            else
            {
                foreach (var c in received)
                {
                    allContacts.Add(new C
                    {
                        FirstName = c.FirstName,
                        LastName = c.LastName
                    });
                }
            }

            return Ok(allContacts);
        }

        [AllowAnonymous]
        [HttpPatch(nameof(RemoveShared))]
        public IActionResult RemoveShared()
        {
            var contactUser = _context.ContactUsers
                .FirstOrDefault(cu => cu.ContactId == contactId
                                      && cu.UserId == userId);

            _context.ContactUsers.Remove(contactUser);
            AcceptOrDecline();

            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch(nameof(ToggleMyContact))]
        public IActionResult ToggleMyContact()
        {
            var lauras = _context.Users
                .Include(u => u.UnacceptedShares)
                .FirstOrDefault(u => u.Id == userId);

            var current = lauras.ShowMyContact;
            lauras.ShowMyContact = !current;
            _context.SaveChanges();

            return Ok();
        }

        private void AcceptOrDecline()
        {
            var lauras = _context.Users
                .Include(u => u.UnacceptedShares)
                .FirstOrDefault(u => u.Id == userId);

            if (lauras != null)
            {
                var toBeAccepted = lauras.UnacceptedShares.FirstOrDefault(us => us.ContactId == contactId);
                lauras.UnacceptedShares.Remove(toBeAccepted);
                _context.SaveChanges();
            }
        }
    }

    public class C
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
