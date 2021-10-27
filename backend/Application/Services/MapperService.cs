using Application.Dto.Contact;
using Application.Dto.User;
using Business.Models;
using Business.Services;
using Data.Models;
using System;

namespace Application.Services
{
    public class MapperService
    {
        private readonly ContactInformationMapper _mapper;

        public MapperService(ContactInformationMapper mapper)
        {
            _mapper = mapper;
        }

        public UserBasic UserBasinInformationFrom(User user)
        {
            return new UserBasic
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            };
        }

        public UserResponse ResponseFrom(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                ShowMyContact = user.ShowMyContact
            };
        }

        public Contact NewContactFrom(string userId, CreateContactRequest request)
        {
            var newContact = new Contact
            {
                Id = Guid.NewGuid().ToString(),
                CreatorId = userId
            };

            _mapper.ReplaceContactInformationWith(request, newContact);

            return newContact;
        }

        public void UpdateContact(Contact contact, UpdateContactRequest request)
        {
            _mapper.ReplaceContactInformationWith(request, contact);
        }

        public ContactResponse ContactResponseFrom(Contact contact)
        {
            var response = new ContactResponse { Id = contact.Id };

            _mapper.ReplaceContactInformationWith(contact, response);

            return response;
        }

        public ContactResponse ContactResponseFrom(User user)
        {
            var response = new ContactResponse { Id = user.Id };

            _mapper.ReplaceContactInformationWith(user, response);

            return response;
        }
    }
}
