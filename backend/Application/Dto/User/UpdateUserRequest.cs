using Business.Models;

namespace Application.Dto.User
{
    public class UpdateUserRequest : ContactInformation
    {
        public string Id { get; set; }
    }
}
