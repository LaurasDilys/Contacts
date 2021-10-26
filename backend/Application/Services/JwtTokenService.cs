using Application.Models;
using Business.Interfaces.Dto;
using Business.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class JwtTokenService
    {
        private readonly JwtTokenOptions _options;

        public JwtTokenService(IOptions<JwtTokenOptions> options)
        {
            _options = options.Value;
        }

        private string GenerateJwtToken(string userName) => GenerateJwtToken(userName, false);

        public string GenerateJwtToken(string userName, bool remember)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_options.Secret);

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("username", userName)
            };

            if (remember)
                claims.Add(new Claim("rememberMe", "true"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = remember ? DateTime.Now.AddDays(_options.DaysRemembered) : DateTime.Now.AddMinutes(_options.Minutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private CookieOptions GenerateCookieOptions() => GenerateCookieOptions(false);

        public CookieOptions GenerateCookieOptions(bool remember)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = remember ? DateTime.Now.AddDays(_options.DaysRemembered) : DateTime.Now.AddMinutes(_options.Minutes),
            };
        }

        public string UserNameFromToken(string tokenString)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);

            return token.Claims.First(claim => claim.Type == "username").Value;
        }

        public bool NewCookieIsNecessary(string tokenString)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);

            var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)token.Payload.Exp);

            if ((dateTimeOffset.LocalDateTime - DateTime.Now).TotalMinutes < _options.Minutes)
                return true;

            return false;
        }

        public void RefreshCookieIfNecessary(string tokenString, HttpContext httpContext)
        {
            var userName = UserNameFromToken(tokenString);

            RefreshCookieIfNecessary(tokenString, userName, httpContext);
        }

        public void RefreshCookieIfNecessary(string tokenString, string userName, HttpContext httpContext)
        {
            if (NewCookieIsNecessary(tokenString))
            {
                var jwtToken = GenerateJwtToken(userName);
                var cookieOptions = GenerateCookieOptions();

                httpContext.Response.Cookies.Append("token", jwtToken, cookieOptions);
            }
        }
    }
}
