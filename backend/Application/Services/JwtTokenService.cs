using Application.Models;
using Business.Interfaces.Dto;
using Business.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtTokenOptions _options;

        public JwtTokenService(IOptions<JwtTokenOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateJwtToken(ILoginRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_options.Secret);

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("username", request.Username)
            };

            if (request.Remember)
                claims.Add(new Claim("rememberMe", "true"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = request.Remember ? DateTime.Now.AddDays(_options.DaysRemembered) : DateTime.Now.AddMinutes(_options.Minutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public CookieOptions GenerateCookieOptions(ILoginRequest request)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = request.Remember ? DateTime.Now.AddDays(_options.DaysRemembered) : DateTime.Now.AddMinutes(_options.Minutes),
            };
        }
    }
}
