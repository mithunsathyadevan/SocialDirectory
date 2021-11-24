using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SocailDirectoryServices.Contact;
using SocailDirectoryServices.Interest;
using SocailDirectoryServices.UserManagement;
using SocialDirectoryAPI.Contract;
using SocialDirectoryAPI.Extentions;
using SocialDirectoryAPI.Services;
using SocialDirectoryContracts.Contact;
using SocialDirectoryContracts.Interest;
using SocialDirectoryContracts.UserManagement;
using SocialDirectoryDataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialDirectoryAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("AllOrigins",
                    builder =>
                    {
                        builder.AllowAnyHeader()
                                       .AllowAnyOrigin()
                                      .AllowAnyMethod();
                    });
            });
            services.AddDbContext<SocialDirectoryContext>(options => options.UseSqlServer(Configuration["Connnectionstrings:MyConnection"]));
            services.AddTokenAuthentication(Configuration);
            services.AddTransient< IAuthenticateContract ,AuthtenticationService >();
            services.AddTransient<IJwtContract, JWTService>();
            services.AddTransient<IContacts, ContactService>();
            services.AddTransient<IUserDetailsContract, UserDetailsService>();
            services.AddTransient<IMatchAlgo, MatchAlgoService>();
            services.AddTransient<InterestContract, InterestService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllOrigins");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }
    }
}
