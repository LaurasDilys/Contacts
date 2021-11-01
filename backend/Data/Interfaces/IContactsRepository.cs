using Data.Models;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IContactsRepository
    {
        Task AddContactUserAsync(ContactUser contactUser);
        Task AddUnacceptedShareAsync(UnacceptedShare unacceptedShare);
        Task CreateAsync(Contact contact);
        Task DeleteAsync(string contactId);
        Task<Contact> FindByIdAsync(string contactId);
        Task<ContactUser> GetContactUserAsync(string contactId, string userId);
        Task<Contact> GetReceivedContact(string contactId);
        Task<Contact> GetSharedOrOtherContact(string contactId);
        Task<UnacceptedShare> GetUnacceptedShareAsync(string contactId, string userId);
        void RemoveContactUser(ContactUser contactUser);
        void RemoveUnacceptedShare(UnacceptedShare unacceptedShare);
        Task SaveChangesAsync();
    }
}