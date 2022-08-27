using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using MoviesWebApp.Authorization.Requirements;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.Cookies;
using Sustainsys.Saml2;
using Sustainsys.Saml2.Metadata;
using Sustainsys.Saml2.WebSso;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

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
                    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
                })

                .AddSaml2("Saml2", options =>
                {
                    options.SPOptions.ModulePath = "Saml2";
                    options.SPOptions.EntityId = new EntityId("https://movieswebapp:4431");
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
                        new ServiceCertificate()
                        {
                            Certificate = new X509Certificate2("Sustainsys.Saml2.Tests.pfx"),
                            Use = CertificateUse.Both,
                            //the following can be used to set the KeyDescriptor use value. If not set, the key is unspecified (meaning it can be used both for signing as well as encryption)
                            //MetadataPublishOverride = MetadataPublishOverrideType.PublishEncryption,
                        });
                })

                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "https://identityserver:4430/";
                    options.RequireHttpsMetadata = true;
                    options.ClientId = "movieswebapp";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";
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

                    //do not use the following in production - it disables certificate validation to avoid having to update this lab. 
                    HttpClientHandler handler = new HttpClientHandler();
                    handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                    options.BackchannelHttpHandler = handler;

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
                options.EnableEndpointRouting = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
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
