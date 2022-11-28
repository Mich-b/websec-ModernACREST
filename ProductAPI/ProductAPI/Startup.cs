using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Build.Security.AspNetCore.Middleware.Extensions;
using Build.Security.AspNetCore.Middleware.Request;
using Build.Security.AspNetCore.Middleware.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ProductAPI
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
            //services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddControllers();
            services.AddRouting();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://identityserver:4430";

                    // name of the API resource
                    options.Audience = "productapi";

                    // we are using https
                    options.RequireHttpsMetadata = true;
                });
            //Add OPA integration
            services.AddBuildAuthorization(options =>
            {
                options.Enable = true;
                options.BaseAddress = "http://opaserver:8181";
                options.PolicyPath = "/authz/allow";
                options.AllowOnFailure = false;
                options.Timeout = 5;
                //options.PermissionHierarchySeparator = '.';
            });
            //End add OPA integration

            //Add cors to allow call from JS client 
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("https://singlepageapp:4432")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("default");
            app.UseRouting();
            app.UseAuthentication();
            //Add OPA integration
            app.UseBuildAuthorization();
            //End add OPA integration
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
