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

        //
        // MUST first RemoveShared() foreach SharedWith User
        //
        [HttpDelete("Contacts/{key}", Name = nameof(Delete))]
        public async Task<IActionResult> Delete([FromRoute] string key)
        {
            if (!await _contactsService.Exists(key))
                return StatusCode(StatusCodes.Status404NotFound);

            await _contactsService.Delete(key);

            return Ok();
        }

        //
        //
        //

        [AllowAnonymous]
        [HttpPatch(nameof(Share))]
        public IActionResult Share()
        {
            //var laurasShared = new ContactUser
            //{
            //    ContactId = "94702cc7-3c19-49d1-b396-60a17f04d9ab",
            //    UserId = "6b675e09-54a7-4c4a-9296-c1ff08f99c91"
            //};

            var sharedShared = new ContactUser
            {
                ContactId = "aaa8d67e-f562-41d8-8d4a-e6367a542dc1",
                UserId = "020619ed-d5b0-4465-9aa3-b7b6651cfa69"
            };

            //_context.ContactUsers.Add(laurasShared);
            //_context.Users.Include(u => u.UnacceptedShares)
            //    .FirstOrDefault(u => u.Id == "6b675e09-54a7-4c4a-9296-c1ff08f99c91")
            //    .UnacceptedShares.Add(new UnacceptedShare
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    ContactId = "94702cc7-3c19-49d1-b396-60a17f04d9ab"
            //});

            _context.ContactUsers.Add(sharedShared);
            _context.Users.Include(u => u.UnacceptedShares)
                .FirstOrDefault(u => u.Id == "020619ed-d5b0-4465-9aa3-b7b6651cfa69")
                .UnacceptedShares.Add(new UnacceptedShare
            {
                Id = Guid.NewGuid().ToString(),
                ContactId = "aaa8d67e-f562-41d8-8d4a-e6367a542dc1"
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
                .FirstOrDefault(u => u.Id == "020619ed-d5b0-4465-9aa3-b7b6651cfa69");

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
                .FirstOrDefault(cu => cu.ContactId == "aaa8d67e-f562-41d8-8d4a-e6367a542dc1"
                                      && cu.UserId == "020619ed-d5b0-4465-9aa3-b7b6651cfa69");

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
                .FirstOrDefault(u => u.Id == "020619ed-d5b0-4465-9aa3-b7b6651cfa69");

            var current = lauras.ShowMyContact;
            lauras.ShowMyContact = !current;
            _context.SaveChanges();

            return Ok();
        }

        private void AcceptOrDecline()
        {
            var lauras = _context.Users
                .Include(u => u.UnacceptedShares)
                .FirstOrDefault(u => u.Id == "020619ed-d5b0-4465-9aa3-b7b6651cfa69");

            if (lauras != null)
            {
                var toBeAccepted = lauras.UnacceptedShares.FirstOrDefault(us => us.ContactId == "aaa8d67e-f562-41d8-8d4a-e6367a542dc1");
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
