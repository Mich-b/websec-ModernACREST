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

                    RedirectUris =           { "https://movieswebapp:4431/signin-oidc" },
                    PostLogoutRedirectUris = { "https://movieswebapp:4431/signout-callback-oidc" },
                    AllowedCorsOrigins =     { "https://movieswebapp:4431" },

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
                    RedirectUris =           { "https://singlepageapp:4432/callback.html" },
                    PostLogoutRedirectUris = { "https://singlepageapp:4432/index.html" },
                    AllowedCorsOrigins =     { "https://singlepageapp:4432" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "role",
                        "productapi",
                        "productapi.read"
                    }
                }
            };
        }
    }
}
