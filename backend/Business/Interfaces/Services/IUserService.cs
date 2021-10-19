using Business.Interfaces.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Services
{
    public interface IUserService
    {
        public Task<bool> ExistsAsync(string userName);

        public Task<bool> CreateAsync(IRegisterRequest request);

        public Task<bool> UserNameAndPasswordAreValidAsync(ILoginRequest request);
    }
}
