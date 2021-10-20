using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }

        public string CreatorId { get; set; }
        public User Creator { get; set; }
    }
}
