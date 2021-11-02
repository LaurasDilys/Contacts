using Business.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Contact
{
    public class UpdateContactRequest : ContactInformation
    {

        [Required]
        public string Id { get; set; }
    }
}
