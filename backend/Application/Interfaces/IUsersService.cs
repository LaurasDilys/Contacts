using Application.Dto.Authentication;
using Application.Dto.User;
using Business.Models;
using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsersService
    {
        Task<bool> ChangeUserPassword(User user, string newPassword);
        Task<bool> CreateAsync(RegisterRequest request);
        Task<User> FindByIdAsync(string userId);
        Task<User> FindByNameAsync(string userName);
        Task<ICollection<UserBasicInformation>> GetOtherUsersAsync(string userId);
        Task<bool> NameExistsAsync(string userName);
        Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request);
        Task<bool> UserNameAndPasswordAreValidAsync(string username, string password);
        Task<bool> UserNameAndPasswordAreValidAsync(User user, string password);
    }
}