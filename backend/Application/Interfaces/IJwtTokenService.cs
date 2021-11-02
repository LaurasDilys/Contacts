using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IJwtTokenService
    {
        CookieOptions GenerateCookieOptions(bool remember);
        string GenerateJwtToken(string userName, bool remember);
        bool NewCookieIsNecessary(string tokenString);
        void RefreshCookieIfNecessary(string tokenString, HttpContext httpContext);
        void RefreshCookieIfNecessary(string tokenString, string userName, HttpContext httpContext);
        string UserNameFromToken(string tokenString);
    }
}