using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UsersRepository
    {
        private readonly DataContext _context;

        public UsersRepository(DataContext context)
        {
            _context = context;
        }

        //public async Task<ICollection<User>> GetAllUsersAsync()
        //{
        //    return await _context.Users.ToListAsync();
        //}

        public async Task<User> GetUserWithDeepRelationsAsync(string userId)
        {
            var user = await _context.Users
                .Include(u => u.Contacts).ThenInclude(c => c.ContactUsers).ThenInclude(cu => cu.User)
                .Include(u => u.ContactUsers).ThenInclude(cu => cu.Contact).ThenInclude(c => c.Creator)
                .Include(u => u.UnacceptedShares).ThenInclude(us => us.Contact).ThenInclude(c => c.Creator)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }
    }
}
