using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ContactsRepository
    {
        private readonly DataContext _context;

        public ContactsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
            if (contact == null) return false;
            return true;
        }

        public async Task<ICollection<Contact>> GetAsync(string userId)
        {
            var user = await _context.Users
                .Include(x => x.Contacts)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user.Contacts;
        }

        public async Task<Contact> GetOneAsync(string id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
            return contact;
        }
        
        public async Task CreateAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
            _context.Contacts.Remove(contact);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
