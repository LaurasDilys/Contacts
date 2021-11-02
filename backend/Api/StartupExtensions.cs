using Application.Interfaces;
using Application.Models;
using Application.Services;
using Business.Services;
using Data;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    public static class StartupExtensions
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IContactsService, ContactsService>();
            services.AddScoped<IUsersService, UsersService>();

            services.AddScoped<IContactsRepository, ContactsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();

            services.AddTransient<IMapperService, MapperService>();
            services.AddTransient<ContactInformationMapper>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services, string securityKey)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
            };

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "token";
                    options.TicketDataFormat = new JwtDataFormat(SecurityAlgorithms.HmacSha256,
                        tokenValidationParameters);
                    options.Events.OnRedirectToLogin = (context) =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                });
        }

        public static void AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(builder => builder.UseSqlServer(connectionString));
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<DataContext>()
                    .AddDefaultTokenProviders();
        }

        public static void ConfigurePasswordRequirements(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
            });
        }

        public static void AddDatabaseMigrations(this IApplicationBuilder app)
        {
            // Database migrations added by default
            var context = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<DataContext>();
            context.Database.Migrate();
        }
    }
}
