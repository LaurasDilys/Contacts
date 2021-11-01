using Application.Dto.Contact;
using Application.Interfaces;
using Application.Models;
using Business.Models;
using Data.Interfaces;
using Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ContactsService
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapperService _mapper;

        public ContactsService(IContactsRepository repository,
            IUsersRepository usersRepository,
            IMapperService mapper)
        {
            _contactsRepository = repository;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        //public async Task<bool> ExistsAsync(string id)
        //{
        //    return await _contactsRepository.ExistsAsync(id);
        //}

        public async Task<Contact> FindByIdAsync(string contactId)
        {
            return await _contactsRepository.FindByIdAsync(contactId);
        }

        public async Task<ICollection<ContactResponse>> GetAllContactsAsync(string userId)
        {
            var user = await _usersRepository.GetUserWithAllContactsAsync(userId);
            var response = new List<ContactResponse>();

            foreach (var contact in user.ContactUsers.Select(cu => cu.Contact))
            {
                response.Add(_mapper.Received(contact));
            }

            foreach (var contact in user.UnacceptedShares.Select(us => us.Contact))
            {
                response.Add(_mapper.Received(contact, ContactTypes.Unaccepted));
            }

            foreach (var contact in user.Contacts)
            {
                response.Add(_mapper.SharedOrOther(contact));
            }

            if (!user.ShowMyContact) response = response.Where(cu => !cu.Me).ToList();

            return response;
        }

        public async Task<ContactResponse> CreateAsync(string userId, CreateContactRequest request)
        {
            var contact = _mapper.NewContactFrom(userId, request);
            await _contactsRepository.CreateAsync(contact);
            return _mapper.ContactResponseFrom(contact);
        }

        public async Task<ContactResponse> UpdateAsync(UpdateContactRequest request)
        {
            var contact = await _contactsRepository.GetSharedOrOtherContact(request.Id);
            _mapper.UpdateContact(contact, request);
            await _contactsRepository.SaveChangesAsync();
            return _mapper.SharedOrOther(contact);
        }

        public async Task<ContactResponse> ShareContact(string contactId, string userId)
        {
            var unacceptedShare = new UnacceptedShare
            {
                ContactId = contactId,
                UserId = userId
            };

            await _contactsRepository.AddUnacceptedShareAsync(unacceptedShare);
            await _contactsRepository.SaveChangesAsync();

            var response = _mapper.SharedOrOther(
                await _contactsRepository.GetSharedOrOtherContact(contactId));

            return response;
        }

        public async Task<ContactResponse> AcceptSharedContact(string contactId, string userId)
        {
            var unacceptedShare = await _contactsRepository.GetUnacceptedShareAsync(contactId, userId);

            if (unacceptedShare == null) return null;
            else
            {
                var contactUser = new ContactUser
                {
                    ContactId = contactId,
                    UserId = userId
                };

                await _contactsRepository.AddContactUserAsync(contactUser);
                _contactsRepository.RemoveUnacceptedShare(unacceptedShare);
                await _contactsRepository.SaveChangesAsync();
            }

            return _mapper.Received(
                await _contactsRepository.GetReceivedContact(contactId));
        }

        public async Task<ContactResponse> StopSharingContact(string contactId, string userId)
        {
            var unacceptedShare = await _contactsRepository.GetUnacceptedShareAsync(contactId, userId);
            var contactUser = await _contactsRepository.GetContactUserAsync(contactId, userId);

            if (unacceptedShare == null && contactUser == null) return null;

            if (unacceptedShare != null) _contactsRepository.RemoveUnacceptedShare(unacceptedShare);
            if (contactUser != null) _contactsRepository.RemoveContactUser(contactUser);
            await _contactsRepository.SaveChangesAsync();

            var response = _mapper.SharedOrOther(
                await _contactsRepository.GetSharedOrOtherContact(contactId));

            return response;
        }

        public async Task DeleteAsync(string contactId)
        {
            await _contactsRepository.DeleteAsync(contactId);
        }
    }
}
