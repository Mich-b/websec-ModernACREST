using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using MoviesWebApp.Authorization.Requirements;
using System.Security.Cryptography.X509Certificates;
using Kentor.AuthServices;
using Kentor.AuthServices.Metadata;
using Kentor.AuthServices.WebSso;
using Microsoft.AspNetCore.Authentication.Cookies;

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

            services.AddAuthentication(options =>
            {
                //Default schemes
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                 .AddCookie("Cookies", options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                })

                .AddSaml2("Saml2", options =>
                {
                    options.SPOptions.ModulePath = "Saml2";
                    options.SPOptions.EntityId = new Microsoft.IdentityModel.Tokens.Saml2.Saml2NameIdentifier("http://movieswebapp:8081");
                    var idp = new IdentityProvider(
                        new EntityId("http://shibbolethidp:8090/idp/shibboleth"), options.SPOptions)
                    {
                        //can be removed when metadata load works again                        
                        SingleSignOnServiceUrl = new System.Uri("http://shibbolethidp:8090/idp/profile/SAML2/POST/SSO"),
                        SingleLogoutServiceUrl = new System.Uri("http://shibbolethidp:8090/idp/profile/SAML2/Redirect/SLO"),
                        Binding = Saml2BindingType.HttpPost,
                    };
                    //can be removed when metadata load works again
                    idp.SigningKeys.AddConfiguredKey(new X509Certificate2("idp-signing.crt"));
                    options.IdentityProviders.Add(idp);
                    options.SPOptions.ServiceCertificates.Add(
                        new Kentor.AuthServices.ServiceCertificate()
                        {
                            Certificate = new X509Certificate2("Sustainsys.Saml2.Tests.pfx"),
                            Use = Kentor.AuthServices.CertificateUse.Both,
                            //the following can be used to set the KeyDescriptor use value. If not set, the key is unspecified (meaning it can be used both for signing as well as encryption)
                            //MetadataPublishOverride = MetadataPublishOverrideType.PublishEncryption,
                        });
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
