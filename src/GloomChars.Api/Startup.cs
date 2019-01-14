using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloomChars.Api.Authentication;
using GloomChars.Authentication.Interfaces;
using GloomChars.Authentication.Repositories;
using GloomChars.Authentication.Services;
using GloomChars.GameData.Interfaces;
using GloomChars.GameData.Services;
using GloomChars.Common.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GloomChars.Characters.Interfaces;
using GloomChars.Characters.Repositories;
using GloomChars.Characters.Services;

namespace GloomChars.Api
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
            services.Configure<DatabaseConfig>(Configuration.GetSection("Database"));
            services.Configure<AuthConfig>(Configuration.GetSection("Authentication"));
            
            //Should probably put these elsewhere if this list gets too big
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IGameDataService, GameDataService>();
            services.AddTransient<ICharactersEditRepository, CharactersEditRepository>();
            services.AddTransient<ICharactersReadRepository, CharactersReadRepository>();
            services.AddTransient<ICharactersService, CharactersService>();

            services.AddAuthentication(BearerTokenDefaults.AuthenticationOptions)
                    .AddBearerToken();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
