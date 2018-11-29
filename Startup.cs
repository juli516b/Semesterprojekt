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
using Swashbuckle.AspNetCore.Swagger;

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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            Security.ConfigureAuthentication(_configuration, services);

            services.AddScoped<IUserRepository, UserRepository> ();

        }
        public void Configure (IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {

            app.UseStaticFiles();

            loggerFactory.AddConsole (_configuration.GetSection ("Logging"));
            loggerFactory.AddDebug ();
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseSwagger();

            app.UseCors (x => x
                .AllowAnyOrigin ()
                .AllowAnyMethod ()
                .AllowAnyHeader ()
                .AllowCredentials ());

            app.UseAuthentication ();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc ();
        }
    }
}