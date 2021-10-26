using Business.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Contact
{
    public class ContactResponse : ContactInformation
    {
        public string Id { get; set; }
        public UserBasic ReceivedFrom { get; set; } = null; // is null, if isn't received from anyone
        public ICollection<UserBasic> SharedWith { get; set; } = null; // is null, if IS received and therefore cannot be shared
    }
}
