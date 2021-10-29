using Application.Dto.Authentication;
using Application.Dto.User;
using Business.Models;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly UsersRepository _usersRepository;
        private readonly ContactsRepository _contactsRepository;
        private readonly MapperService _mapper;

        public UsersService(UserManager<User> userManager,
            UsersRepository usersRepository,
            ContactsRepository contactsRepository,
            MapperService mapper)
        {
            _userManager = userManager;
            _usersRepository = usersRepository;
            _contactsRepository = contactsRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<UserBasic>> GetOtherUsersAsync(string userId)
        {
            var users = await _usersRepository.GetOtherUsersAsync(userId);

            var response = new List<UserBasic>();
            foreach (var user in users)
            {
                response.Add(_mapper.UserBasinInformationFrom(user));
            }
            return response;
        }

        public async Task<bool> NameExistsAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return false;

            return true;
        }

        public async Task<bool> CreateAsync(RegisterRequest request)
        {
            var userId = Guid.NewGuid().ToString();

            var user = new User
            {
                Id = userId,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded) // Create contact using user's own contact information
            {
                var createdUser = await _userManager.FindByIdAsync(userId);
                await CreateMyContact(createdUser);
            }

            return result.Succeeded;
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> UserNameAndPasswordAreValidAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return false;

            return true;
        }

        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request)
        {
            var user = await _usersRepository.GetUserWithOwnContactsAsync(request.Id);

            _mapper.UpdateUser(user, request);

            var contact = user.Contacts.FirstOrDefault(c => c.Me);

            if (contact == null)
            {
                await CreateMyContact(user);
                contact = user.Contacts.FirstOrDefault(c => c.Me);
            }

            _mapper.UpdateContact(contact, request);

            await _usersRepository.SaveChangesAsync();

            return new UpdateUserResponse
            {
                User = _mapper.UserResponseFrom(user),
                MyContact = user.ShowMyContact ? _mapper.SharedOrOther(contact) : null
            };
        }

        private async Task CreateMyContact(User user)
        {
            var contact = _mapper.NewContactFrom(user.Id, user);

            contact.Me = true;
            await _contactsRepository.CreateAsync(contact);
        }
    }
}
