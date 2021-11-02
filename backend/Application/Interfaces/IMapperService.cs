using Application.Dto.Contact;
using Application.Dto.User;
using Business.Interfaces;
using Business.Models;
using Data.Models;

namespace Application.Interfaces
{
    public interface IMapperService
    {
        ContactResponse ContactResponseFrom(Contact contact);
        Contact NewContactFrom(string userId, IContactInformation request);
        ContactResponse Received(Contact contact);
        ContactResponse Received(Contact contact, string type);
        ContactResponse SharedOrOther(Contact contact);
        void UpdateContact(Contact contact, IContactInformation request);
        void UpdateUser(User user, UpdateUserRequest request);
        UserBasicInformation UserBasinInformationFrom(User user);
        UserResponse UserResponseFrom(User user);
    }
}