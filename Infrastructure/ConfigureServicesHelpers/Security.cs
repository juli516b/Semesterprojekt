using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Semesterprojekt.Core.Repositories;
using Semesterprojekt.Infrastructure.Helpers;

namespace Semesterprojekt.Infrastructure.ConfigureServicesHelpers
{
    public class Security
    {
        public static void ConfigureAuthentication(IConfiguration _configuration, IServiceCollection services) 
        {
            var appSettingsSection = _configuration.GetSection ("AppSettings");
            services.Configure<AppSettings> (appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings> ();
            var key = Encoding.ASCII.GetBytes (appSettings.Secret);
            services.AddAuthentication (scheme => {
                    scheme.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    scheme.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer (jwt => {
                    jwt.Events = new JwtBearerEvents {
                        OnTokenValidated = context => {
                            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserRepository> ();
                            var userId = int.Parse (context.Principal.Identity.Name);
                            var user = userService.GetById (userId);
                            if (user == null) {
                                context.Fail ("Unauthorized");
                            }
                            return Task.CompletedTask;
                        }
                    };
                    jwt.RequireHttpsMetadata = false;
                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey (key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

        }
    }
}
