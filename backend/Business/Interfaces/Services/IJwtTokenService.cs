using Business.Interfaces.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Services
{
    public interface IJwtTokenService
    {
        public string GenerateJwtToken(string userName);

        public string GenerateJwtToken(string userName, bool remember);

        public CookieOptions GenerateCookieOptions();

        public CookieOptions GenerateCookieOptions(bool remember);

        public string UserNameFromToken(string tokenString);

        public bool NewCookieIsNecessary(string tokenString);
    }
}
