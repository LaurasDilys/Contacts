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

        //public async Task<bool> ExistsAsync(string id)
        //{
        //    var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
        //    if (contact == null) return false;
        //    return true;
        //}

        public async Task<Contact> FindByIdAsync(string contactId)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);
        }

        public async Task<Contact> GetReceivedContact(string contactId)
        {
            return await _context.Contacts
                .Include(c => c.Creator)
                .FirstOrDefaultAsync(c => c.Id == contactId);
        }

        public async Task<Contact> GetSharedOrOtherContact(string contactId)
        {
            return await _context.Contacts
                .Include(c => c.ContactUsers).ThenInclude(cu => cu.User)
                .Include(c => c.UnacceptedShares).ThenInclude(cu => cu.User)
                .FirstOrDefaultAsync(c => c.Id == contactId);
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

        public async Task DeleteAsync(string contactId)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);
            _context.Contacts.Remove(contact);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
