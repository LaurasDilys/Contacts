using Application.Dto.Contact;
using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IContactsService
    {
        Task<ContactResponse> AcceptSharedContact(string contactId, string userId);
        Task<ContactResponse> CreateAsync(string userId, CreateContactRequest request);
        Task DeleteAsync(string contactId);
        Task<Contact> FindByIdAsync(string contactId);
        Task<ICollection<ContactResponse>> GetAllContactsAsync(string userId);
        Task<ContactResponse> ShareContact(string contactId, string userId);
        Task<ContactResponse> StopSharingContact(string contactId, string userId);
        Task<ContactResponse> UpdateAsync(UpdateContactRequest request);
    }
}