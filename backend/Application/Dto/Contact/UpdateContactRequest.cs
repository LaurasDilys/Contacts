using Business.Models;

namespace Application.Dto.Contact
{
    public class UpdateContactRequest : ContactInformation
    {
        public string Id { get; set; }
    }
}
