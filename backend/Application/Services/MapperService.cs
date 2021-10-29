using Application.Dto.Contact;
using Application.Dto.User;
using Application.Models;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class MapperService
    {
        private readonly ContactInformationMapper _contactInformationMapper;

        public MapperService(ContactInformationMapper contactInformationMapper)
        {
            _contactInformationMapper = contactInformationMapper;
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

        public void UpdateUser(User user, UpdateUserRequest request)
        {
            user.ShowMyContact = request.ShowMyContact;

            _contactInformationMapper.
                ReplaceContactInformationWith(request, user);
        }

        public UserResponse UserResponseFrom(User user)
        {
            var response = new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                ShowMyContact = user.ShowMyContact
            };

            _contactInformationMapper.
                ReplaceContactInformationWith(user, response);

            return response;
        }

        public Contact NewContactFrom(string userId, IContactInformation request)
        {
            var newContact = new Contact
            {
                Id = Guid.NewGuid().ToString(),
                CreatorId = userId
            };

            _contactInformationMapper.
                ReplaceContactInformationWith(request, newContact);

            return newContact;
        }

        public void UpdateContact(Contact contact, IContactInformation request)
        {
            _contactInformationMapper.
                ReplaceContactInformationWith(request, contact);
        }

        public ContactResponse ContactResponseFrom(Contact contact)
        {
            var response = new ContactResponse { Id = contact.Id };

            _contactInformationMapper.
                ReplaceContactInformationWith(contact, response);

            return response;
        }

        public ContactResponse Received(Contact contact)
        {
            return Received(contact, ContactTypes.Received);
        }

        public ContactResponse Received(Contact contact, string type)
        {
            var response = ContactResponseFrom(contact);
            response.Type = type;

            response.ReceivedFrom = UserBasinInformationFrom(contact.Creator);
            return response;
        }

        public ContactResponse SharedOrOther(Contact contact)
        {
            var response = ContactResponseFrom(contact);
            response.Me = contact.Me;

            if ((contact.ContactUsers == null ||
                contact.ContactUsers.Count == 0) &&
                (contact.UnacceptedShares == null ||
                contact.UnacceptedShares.Count == 0))
                response.Type = ContactTypes.Other;
            else
            {
                response.Type = ContactTypes.Shared;
                response.SharedWith = new List<UserBasic>();
                foreach (var user in contact.ContactUsers.Select(cu => cu.User))
                {
                    response.SharedWith.Add(UserBasinInformationFrom(user));
                }
                foreach (var user in contact.UnacceptedShares.Select(cu => cu.User))
                {
                    response.SharedWith.Add(UserBasinInformationFrom(user));
                }
            }
            return response;
        }
    }
}
