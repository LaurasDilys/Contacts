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
        public string GenerateJwtToken(ILoginRequest request);

        public CookieOptions GenerateCookieOptions(ILoginRequest request);
    }
}
