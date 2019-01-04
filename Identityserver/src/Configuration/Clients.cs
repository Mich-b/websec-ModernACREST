using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // MoviesWebApp Client
                new Client
                {
                    ClientId = "movieswebapp",
                    ClientName = "The Movie App",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris =           { "http://movieswebapp:8081/signin-oidc" },
                    PostLogoutRedirectUris = { "http://movieswebapp:8081/signout-callback-oidc" },
                    AllowedCorsOrigins =     { "http://movieswebapp:8081" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "role"
                     }
                },
                // JavaScript Client
                new Client
                {
                    ClientId = "SPA",
                    ClientName = "JavaScript SPA Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris =           { "http://singlepageapp:8082/callback.html" },
                    PostLogoutRedirectUris = { "http://singlepageapp:8082/index.html" },
                    AllowedCorsOrigins =     { "http://singlepageapp:8082" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "role",
                        "productapi.read"
                    }
                }
            };
        }
    }
}
