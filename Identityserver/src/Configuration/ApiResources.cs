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
            // extended version if more control is needed
            return new List<ApiResource>
            {           
                new IdentityServer4.Models.ApiResource("productapi", "Product API")
                {
                    // this API defines two scopes
                    Scopes = { "productapi.readwrite", "productapi.read" }
                }
            };
        }

    }
}
