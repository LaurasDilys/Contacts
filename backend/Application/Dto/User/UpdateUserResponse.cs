using Application.Dto.Contact;

namespace Application.Dto.User
{
    public class UpdateUserResponse
    {
        public UserResponse User { get; set; }
        public ContactResponse MyContact { get; set; }
    }
}
