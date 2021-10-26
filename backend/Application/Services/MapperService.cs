using Application.Dto.Contact;
using Application.Dto.User;
using Data.Models;
using System;

namespace Application.Services
{
    public class MapperService
    {
        public UserResponse ResponseFrom(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public Contact NewContactFrom(string userId, CreateContactRequest request)
        {
            return new Contact
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                AlternativePhoneNumber = request.AlternativePhoneNumber,
                Email = request.Email,
                AlternativeEmail = request.AlternativeEmail,
                DateOfBirth = request.DateOfBirth,
                Notes = request.Notes,
                CreatorId = userId
            };
        }

        public void UpdateContact(Contact contact, UpdateContactRequest request)
        {
            contact.FirstName = request.FirstName;
            contact.LastName = request.LastName;
            contact.PhoneNumber = request.PhoneNumber;
            contact.AlternativePhoneNumber = request.AlternativePhoneNumber;
            contact.Email = request.Email;
            contact.AlternativeEmail = request.AlternativeEmail;
            contact.DateOfBirth = request.DateOfBirth;
            contact.Notes = request.Notes;
        }

        public ContactResponse ContactResponseFrom(Contact contact)
        {
            return new ContactResponse
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNumber = contact.PhoneNumber,
                AlternativePhoneNumber = contact.AlternativePhoneNumber,
                Email = contact.Email,
                AlternativeEmail = contact.AlternativeEmail,
                DateOfBirth = contact.DateOfBirth,
                Notes = contact.Notes
            };
        }
    }
}
