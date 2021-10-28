using Application.Dto.Contact;
using Application.Models;
using Business.Models;
using Data.Models;
using Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ContactsService
    {
        private readonly ContactsRepository _contactsRepository;
        private readonly UsersRepository _usersRepository;
        private readonly MapperService _mapper;

        public ContactsService(ContactsRepository repository,
            UsersRepository usersRepository,
            MapperService mapper)
        {
            _contactsRepository = repository;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _contactsRepository.ExistsAsync(id);
        }

        public async Task<ICollection<ContactResponse>> GetAsync(string userId)
        {
            var user = await _usersRepository.GetUserWithDeepRelationsAsync(userId);
            var response = new List<ContactResponse>();

            foreach (var contact in user.ContactUsers.Select(cu => cu.Contact))
            {
                AddReceivedTo(response, contact);
            }

            foreach (var contact in user.UnacceptedShares.Select(us => us.Contact))
            {
                AddReceivedTo(response, contact, ContactTypes.Unaccepted);
            }

            foreach (var contact in user.Contacts/*.Where(c => !c.Me)*/)
            {
                AddSharedOrOtherTo(response, contact);
            }

            //if (user.ShowMyContact)
            //    AddMeTo(response, user.Contacts.FirstOrDefault(c => c.Me));

            return response;
        }

        //private void AddMeTo(List<ContactResponse> response, Contact contact)
        //{
        //    if (contact != null) // this contact should be present,
        //                         // but still it's worth checking
        //    {
        //        var me = _mapper.ContactResponseFrom(contact);
        //        me.Type = ContactTypes.Me;
        //        response.Add(me);
        //    }
        //}

        private void AddReceivedTo(List<ContactResponse> response, Contact contact)
        {
            AddReceivedTo(response, contact, ContactTypes.Received);
        }

        private void AddReceivedTo(List<ContactResponse> response, Contact contact, string type)
        {
            var receivedContact = _mapper.ContactResponseFrom(contact);
            receivedContact.Type = type;

            receivedContact.ReceivedFrom = _mapper.UserBasinInformationFrom(contact.Creator);
            response.Add(receivedContact);
        }

        private void AddSharedOrOtherTo(List<ContactResponse> response, Contact contact)
        {
            var currentContact = _mapper.ContactResponseFrom(contact);
            if (contact.ContactUsers.Count == 0 &&
                contact.UnacceptedShares.Count == 0)
                currentContact.Type = ContactTypes.Other;
            else
            {
                currentContact.Type = ContactTypes.Shared;
                currentContact.SharedWith = new List<UserBasic>();
                foreach (var user in contact.ContactUsers.Select(cu => cu.User))
                {
                    currentContact.SharedWith.Add(_mapper.UserBasinInformationFrom(user));
                }
                foreach (var user in contact.UnacceptedShares.Select(cu => cu.User))
                {
                    currentContact.SharedWith.Add(_mapper.UserBasinInformationFrom(user));
                }
            }
            response.Add(currentContact);
        }

        //public async Task<Contact> FindByIdAsync(string contactId)
        //{
        //    return await _repository.FindByIdAsync(contactId);
        //}

        public async Task<ContactResponse> CreateAsync(string userId, CreateContactRequest request)
        {
            var contact = _mapper.NewContactFrom(userId, request);
            await _contactsRepository.CreateAsync(contact);
            return _mapper.ContactResponseFrom(contact);
        }

        public async Task<ContactResponse> UpdateAsync(UpdateContactRequest request)
        {
            var contact = await _contactsRepository.FindByIdAsync(request.Id);
            _mapper.UpdateContact(contact, request);
            await _contactsRepository.SaveChangesAsync();
            return _mapper.ContactResponseFrom(contact);
        }

        public async Task<ContactResponse> ShareContact(string contactId, string userId)
        {
            var unacceptedShare = new ContactUser
            {
                ContactId = contactId,
                UserId = userId
            };

            await _contactsRepository.AddContactUserAsync(unacceptedShare);
            await _contactsRepository.SaveChangesAsync();

            var result = _mapper.ContactResponseFrom(
                await _contactsRepository.FindByIdAsync(contactId));

            return result;
        }

        //public async Task<ContactResponse> AcceptShare(string contactId, string userId)
        //{
        //    var unacceptedShare = await _contactsRepository.GetUnacceptedShareAsync(contactId, userId);

        //    if (unacceptedShare != null)
        //    {
        //        var contactUser = new ContactUser
        //        {
        //            ContactId = contactId,
        //            UserId = userId
        //        };

        //        await _contactsRepository.AddContactUserAsync(contactUser);
        //        _contactsRepository.RemoveUnacceptedShare(unacceptedShare);
        //        await _contactsRepository.SaveChangesAsync();
        //    }

        //    // // //
        //    return _mapper.ContactResponseFrom(
        //        await _contactsRepository.FindByIdAsync(contactId));
        //}

        //public async Task<ContactResponse> DeclineShare(string contactId, string userId)
        //{
        //    var unacceptedShare = await _contactsRepository.GetUnacceptedShareAsync(contactId, userId);
        //    var contactUser = await _contactsRepository.GetContactUserAsync(contactId, userId);

        //    if (unacceptedShare != null) _contactsRepository.RemoveUnacceptedShare(unacceptedShare);
        //    if (contactUser != null) _contactsRepository.RemoveContactUser(contactUser);
        //    await _contactsRepository.SaveChangesAsync();

        //    // // //
        //    return _mapper.ContactResponseFrom(
        //        await _contactsRepository.FindByIdAsync(contactId));
        //}

        public async Task DeleteAsync(string id)
        {
            await _contactsRepository.DeleteAsync(id);
        }
    }
}
