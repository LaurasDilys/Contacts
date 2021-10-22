﻿using Business.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Contact : IContact
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternativePhoneNumber { get; set; }
        public string Email { get; set; }
        public string AlternativeEmail { get; set; }
        public string DateOfBirth { get; set; }
        public string Notes { get; set; }

        public string CreatorId { get; set; }
        public User Creator { get; set; }
    }
}
