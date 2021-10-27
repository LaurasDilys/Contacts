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

        public async Task<Contact> FindByIdAsync(string id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
        }
        
        public async Task CreateAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await SaveChangesAsync();
        }

        public async Task<UnacceptedShare> GetUnacceptedShareAsync(string contactId, string userId)
        {
            return await _context.UnacceptedShares
                .FirstOrDefaultAsync(us => us.ContactId == contactId && us.UserId == userId);
        }

        public async Task AddUnacceptedShareAsync(UnacceptedShare unacceptedShare)
        {
            await _context.UnacceptedShares.AddAsync(unacceptedShare);
        }

        public void RemoveUnacceptedShare(UnacceptedShare unacceptedShare)
        {
            _context.UnacceptedShares.Remove(unacceptedShare);
        }

        public async Task<ContactUser> GetContactUserAsync(string contactId, string userId)
        {
            return await _context.ContactUsers
                .FirstOrDefaultAsync(cu => cu.ContactId == contactId && cu.UserId == userId);
        }

        public async Task AddContactUserAsync(ContactUser contactUser)
        {
            await _context.ContactUsers.AddAsync(contactUser);
        }

        public void RemoveContactUser(ContactUser contactUser)
        {
            _context.ContactUsers.Remove(contactUser);
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
