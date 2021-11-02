using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Authentication
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool Remember { get; set; }
    }
}
