using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //MVC required because we will be serving some pages from identityserver
            services.AddMvc();
            //register the IdentityServer services in DI and in-memory store for runtime state.  (development-only)
            services.AddIdentityServer()
            //create temporary key material for signing tokens. Replace by persistent key material for production scenarios. 
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(ApiResources.GetApiResources())
                .AddInMemoryClients(Clients.GetClients())
                .AddTestUsers(Users.GetUsers())
                .AddInMemoryApiResources(ApiResources.GetApiResources()) // adding api resources
                .AddInMemoryIdentityResources(IdentityResources.GetIdentityResources()); // <-- adding identity resources
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentityServer();

            //We will be serving views as well
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
