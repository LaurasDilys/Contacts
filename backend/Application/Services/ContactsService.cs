using Application.Dto.Contact;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ContactsService
    {
        private readonly ContactsRepository _repository;
        private readonly MapperService _mapper;

        public ContactsService(ContactsRepository repository, MapperService mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _repository.ExistsAsync(id);
        }

        public async Task<ICollection<ContactResponse>> GetAsync(string userId)
        {
            var contacts = await _repository.GetAsync(userId);
            var response = new List<ContactResponse>();
            foreach (var contact in contacts)
            {
                response.Add(_mapper.ContactResponseFrom(contact));
            }
            return response;
        }

        //public async Task<Contact> FindByIdAsync(string contactId)
        //{
        //    return await _repository.FindByIdAsync(contactId);
        //}

        public async Task<ContactResponse> CreateAsync(string userId, CreateContactRequest request)
        {
            var contact = _mapper.NewContactFrom(userId, request);
            await _repository.CreateAsync(contact);
            return _mapper.ContactResponseFrom(contact);
        }

        public async Task<ContactResponse> UpdateAsync(UpdateContactRequest request)
        {
            var contact = await _repository.FindByIdAsync(request.Id);
            _mapper.UpdateContact(contact, request);
            await _repository.SaveChangesAsync();
            return _mapper.ContactResponseFrom(contact);
        }

        public async Task ShareContact(string contactId, string userId)
        {
            var unacceptedShare = new UnacceptedShare
            {
                ContactId = contactId,
                UserId = userId
            };

            await _repository.AddUnacceptedShareAsync(unacceptedShare);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> AcceptShare(string contactId, string userId)
        {
            var unacceptedShare = await _repository.GetUnacceptedShareAsync(contactId, userId);

            if (unacceptedShare == null) return false;

            var contactUser = new ContactUser
            {
                ContactId = contactId,
                UserId = userId
            };

            await _repository.AddContactUserAsync(contactUser);
            _repository.RemoveUnacceptedShare(unacceptedShare);
            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task RemoveShare(string contactId, string userId)
        {
            var unacceptedShare = await _repository.GetUnacceptedShareAsync(contactId, userId);
            var contactUser = await _repository.GetContactUserAsync(contactId, userId);

            if (unacceptedShare != null) _repository.RemoveUnacceptedShare(unacceptedShare);
            if (contactUser != null) _repository.RemoveContactUser(contactUser);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
