using Application.Dto.Contact;
using Application.Dto.User;
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

        public UserResponse ResponseFrom(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
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
    }
}
