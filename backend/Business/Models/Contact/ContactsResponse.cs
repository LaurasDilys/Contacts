using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Contact
{
    public class ContactsResponse
    {
        public ICollection<ContactResponse> MyContacts { get; set; }
        public ICollection<ContactResponse> Received { get; set; }
        public ICollection<ContactResponse> Shared { get; set; }
        public ICollection<ContactResponse> Unaccepted { get; set; }
    }
}
