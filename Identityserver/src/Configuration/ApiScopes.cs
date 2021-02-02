using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Configuration
{
    public class ApiScopes
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope(name: "productapi.read", displayName: "Read access to product API"),
                new ApiScope(name: "productapi.readwrite", displayName: "Full access to product API")
            };
        }
    }
}
