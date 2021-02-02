using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Configuration;
using IdentityServer.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //MVC required because we will be serving some pages from identityserver
            services.AddMvc(option => option.EnableEndpointRouting = false);
            // SameSite problems: https://www.thinktecture.com/en/identity/samesite/prepare-your-identityserver/
            services.ConfigureNonBreakingSameSiteCookies();
            //register the IdentityServer services in DI and in-memory store for runtime state.  (development-only)
            services.AddIdentityServer()
            //create temporary key material for signing tokens. Replace by persistent key material for production scenarios. 
                .AddDeveloperSigningCredential()
                .AddInMemoryApiScopes(ApiScopes.GetApiScopes())
                .AddInMemoryApiResources(ApiResources.GetApiResources())
                .AddInMemoryClients(Clients.GetClients())
                .AddTestUsers(Users.GetUsers())
                .AddInMemoryApiResources(ApiResources.GetApiResources()) // adding api resources
                .AddInMemoryIdentityResources(IdentityResources.GetIdentityResources()); // <-- adding identity resources
           
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCookiePolicy();
            app.UseHttpsRedirection();

            app.UseIdentityServer();

            //We will be serving views as well
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
