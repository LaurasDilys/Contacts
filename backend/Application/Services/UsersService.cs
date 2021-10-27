using Application.Dto.Authentication;
using Business.Models;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly UsersRepository _repository;
        private readonly MapperService _mapper;

        public UsersService(UserManager<User> userManager,
            UsersRepository repository,
            MapperService mapper)
        {
            _userManager = userManager;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<UserBasic>> GetOtherUsersAsync(string id)
        {
            var users = await _repository.GetOtherUsersAsync(id);

            var response = new List<UserBasic>();
            foreach (var user in users)
            {
                response.Add(_mapper.UserBasinInformationFrom(user));
            }
            return response;
        }

        public async Task<bool> ExistsAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return false;

            return true;
        }

        public async Task<bool> CreateAsync(RegisterRequest request)
        {
            var user = new User
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            return result.Succeeded;
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<bool> UserNameAndPasswordAreValidAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return false;

            return true;
        }
    }
}
