using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using MoviesLibrary;
using System.Linq;
using System.Threading.Tasks;
using MoviesWebApp.Authorization.Requirements;

namespace MoviesWebApp
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
            services.AddMoviesLibrary();

            services.AddAuthentication("Cookies")
                .AddCookie("Cookies", options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                })

                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "http://identityserver:8080/";
                    options.RequireHttpsMetadata = false;

                    options.ClientId = "movieswebapp";
                    options.ResponseType = "id_token";
                    options.SaveTokens = true;

                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.Scope.Add("role");

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };

                    //this can be used to add claims on the fly
                    //options.Events = new OpenIdConnectEvents
                    //{
                    //    OnTicketReceived = n =>
                    //    {
                    //        var idSvc = n.HttpContext.RequestServices.GetRequiredService<MovieIdentityService>();
                    //        var appClaims = idSvc.GetClaimsForUser(n.Principal.FindFirst("sub")?.Value);

                    //        n.Principal.Identities.First().AddClaims(appClaims);

                    //        return Task.CompletedTask;
                    //    }
                    //};
                });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //Authorization policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SearchPolicy", policy =>
                    policy.Requirements.Add(new SearchRequirement()));
            });
            services.AddTransient<IAuthorizationHandler, Authorization.Handlers.SearchPermissionHandler>();
            services.AddTransient<IAuthorizationHandler, Authorization.Handlers.MovieOperationsCrudHandler>();
            services.AddTransient<IAuthorizationHandler, Authorization.Handlers.ReviewOperationsCrudHandler>();

            // Add framework services. Only allow authenticated users access
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
