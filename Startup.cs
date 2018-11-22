using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Semesterprojekt.Core.Repositories;
using Semesterprojekt.Infrastructure.ConfigureServicesHelpers;
using Semesterprojekt.Persistence;
using Semesterprojekt.Persistence.Repositories;

namespace Semesterprojekt {
    public class Startup {
        private IConfiguration _configuration;

        public Startup (IConfiguration configuration) {
            _configuration = configuration;
        }
        public void ConfigureServices (IServiceCollection services) {
            services.AddCors ();
            services.AddDbContext<GoTrainDbContext> (options => options.UseSqlServer (_configuration.GetConnectionString ("Default")));
            services.AddMvc ();
            services.AddAutoMapper();

            Security.ConfigureAuthentication(_configuration, services);

            services.AddScoped<IUserRepository, UserRepository> ();

        }
        public void Configure (IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole (_configuration.GetSection ("Logging"));
            loggerFactory.AddDebug ();
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseCors (x => x
                .AllowAnyOrigin ()
                .AllowAnyMethod ()
                .AllowAnyHeader ()
                .AllowCredentials ());

            app.UseAuthentication ();

            app.UseMvc ();
        }
    }
}