using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class ContactInformation : IContactInformation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternativePhoneNumber { get; set; }
        public string Email { get; set; }
        public string AlternativeEmail { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
        public string Notes { get; set; }
    }
}
