using Business.Models;

namespace Application.Dto.User
{
    public class UserResponse : ContactInformation
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool ShowMyContact { get; set; }
    }
}
