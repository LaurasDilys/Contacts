using Business.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Contact : IContactInformation
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [MaxLength(50)]
        public string AlternativePhoneNumber { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string AlternativeEmail { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string DateOfBirth { get; set; }

        [MaxLength(50)]
        public string Notes { get; set; }

        public bool Me { get; set; }

        public string CreatorId { get; set; }
        public User Creator { get; set; }

        public ICollection<ContactUser> ContactUsers { get; set; }

        public ICollection<UnacceptedShare> UnacceptedShares { get; set; }
    }
}
