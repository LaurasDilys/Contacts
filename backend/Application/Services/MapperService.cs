using Application.Dto;
using Business.Interfaces.Dto;
using Business.Interfaces.Models;
using Business.Interfaces.Services;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MapperService
    {
        public IUserResponse ResponseFrom(IUser user)
        {
            return new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public Contact ContactFrom(string userId, ICreateContactRequest request)
        {
            return new Contact
            {
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

        public void UpdateContact(IContact contact, IUpdateContactRequest request)
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
    }
}
