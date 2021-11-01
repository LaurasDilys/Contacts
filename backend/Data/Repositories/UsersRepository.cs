using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;

        public UsersRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetOtherUsersAsync(string userId)
        {
            return await _context.Users.Where(u => u.Id != userId).ToListAsync();
        }

        public async Task<User> GetUserWithOwnContactsAsync(string userId)
        {
            var user = await _context.Users
                .Include(u => u.Contacts).ThenInclude(c => c.ContactUsers).ThenInclude(cu => cu.User)
                .Include(u => u.Contacts).ThenInclude(c => c.UnacceptedShares).ThenInclude(cu => cu.User)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async Task<User> GetUserWithAllContactsAsync(string userId)
        {
            var user = await _context.Users
                .Include(u => u.Contacts).ThenInclude(c => c.ContactUsers).ThenInclude(cu => cu.User)
                .Include(u => u.Contacts).ThenInclude(c => c.UnacceptedShares).ThenInclude(cu => cu.User)
                .Include(u => u.ContactUsers).ThenInclude(cu => cu.Contact).ThenInclude(c => c.Creator)
                .Include(u => u.UnacceptedShares).ThenInclude(us => us.Contact).ThenInclude(c => c.Creator)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
