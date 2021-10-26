using System.Collections.Generic;

namespace Application.Dto.Contact
{
    public class ContactsResponse
    {
        public ICollection<ContactResponse> MyContacts { get; set; }
        public ICollection<ContactResponse> Received { get; set; }
        public ICollection<ContactResponse> Shared { get; set; }
        public ICollection<ContactResponse> Unaccepted { get; set; }
    }
}
