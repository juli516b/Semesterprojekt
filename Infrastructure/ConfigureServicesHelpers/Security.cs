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
            services.AddAuthentication(scheme =>
           {
               scheme.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               scheme.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

           }).AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(_configuration.GetSection("AppSettings:Secret").Value)),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });
        }
    }
}
