using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IUsersRepository
    {
        Task<ICollection<User>> GetOtherUsersAsync(string userId);
        Task<User> GetUserWithAllContactsAsync(string userId);
        Task<User> GetUserWithOwnContactsAsync(string userId);
        Task SaveChangesAsync();
    }
}