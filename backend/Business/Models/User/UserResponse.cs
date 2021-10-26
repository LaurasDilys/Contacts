using Business.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.User
{
    public class UserResponse : ContactInformation
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool ShowMyContact { get; set; }
    }
}
