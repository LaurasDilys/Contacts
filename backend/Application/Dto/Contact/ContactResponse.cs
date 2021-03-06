using Business.Models;
using System.Collections.Generic;

namespace Application.Dto.Contact
{
    public class ContactResponse : ContactInformation
    {
        public string Id { get; set; }
        public bool Me { get; set; }
        public string Type { get; set; }
        public UserBasicInformation ReceivedFrom { get; set; } = null; // is null, if isn't received from anyone
        public ICollection<UserBasicInformation> SharedWith { get; set; } = null; // is null, if IS received and therefore cannot be shared
    }
}
