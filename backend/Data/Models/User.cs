using Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class User : IdentityUser, IContactInformation
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string AlternativePhoneNumber { get; set; }

        [MaxLength(50)]
        public string AlternativeEmail { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string DateOfBirth { get; set; }

        [MaxLength(50)]
        public string Notes { get; set; }

        public bool ShowMyContact { get; set; } = true; // When a new user is created,
        // by default their contact will be included in the "All Contacts" section

        public ICollection<Contact> Contacts { get; set; }

        public ICollection<ContactUser> ContactUsers { get; set; }

        public ICollection<UnacceptedShare> UnacceptedShares { get; set; }
    }
}
