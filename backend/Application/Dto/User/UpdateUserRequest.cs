using Business.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.User
{
    public class UpdateUserRequest : ContactInformation
    {

        [Required]
        public string Id { get; set; }

        [Required]
        public bool ShowMyContact { get; set; }
    }
}
