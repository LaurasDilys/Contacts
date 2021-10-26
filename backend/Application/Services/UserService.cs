using Business.Interfaces.Dto;
using Business.Interfaces.Models;
using Business.Interfaces.Services;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ExistsAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return false;

            return true;
        }

        public async Task<bool> CreateAsync(IRegisterRequest request)
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

        public async Task<IUser> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<bool> UserNameAndPasswordAreValidAsync(ILoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return false;

            return true;
        }
    }
}
