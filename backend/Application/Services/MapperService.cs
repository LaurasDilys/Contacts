using Application.Dto;
using Business.Interfaces.Dto;
using Business.Interfaces.Models;
using Business.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MapperService : IMapperService
    {
        public IUserResponse ResponseFrom(IUser user)
        {
            return new UserResponse
            {
                UserName = user.UserName
            };
        }
    }
}
