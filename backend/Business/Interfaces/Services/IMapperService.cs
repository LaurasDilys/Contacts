using Business.Interfaces.Dto;
using Business.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Services
{
    public interface IMapperService
    {
        public IUserResponse ResponseFrom(IUser user);
    }
}
