using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Configuration
{
    public class ApiResources
    {
        //ApiResource controls what goes into the access token for the API.
        public static IEnumerable<IdentityServer4.Models.ApiResource> GetApiResources()
        {
            return new[]
            {           
                // extended version if more control is needed
                new IdentityServer4.Models.ApiResource
                {
                    //name will be used for audience
                    Name = "productapi",
                    // include the following user claims in access token (in addition to subject id)
                    //UserClaims = { JwtClaimTypes.PreferredUserName },

                    // this API defines two scopes
                    Scopes =
                    {
                        new Scope()
                        {
                            Name = "productapi.read",
                            DisplayName = "Read access to product API",
                        },
                        new Scope
                        {
                            Name = "productapi.readwrite",
                            DisplayName = "Full access to product API"
                        }
                    }
                }
            };
        }

    }
}
