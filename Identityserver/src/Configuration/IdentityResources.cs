using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Configuration
{
    //IdentityResource controls what goes in the id token to the client.
    public class IdentityResources
    {
            public static IEnumerable<IdentityServer4.Models.IdentityResource> GetIdentityResources()
            {
                var role = new IdentityResource(
                   name: "role",
                   displayName: "Your Organization Role",
                   claimTypes: new[] { JwtClaimTypes.Role });


                return new List<IdentityResource>
                    {
                        new IdentityServer4.Models.IdentityResources.OpenId(),
                        new IdentityServer4.Models.IdentityResources.Email(),
                        new IdentityServer4.Models.IdentityResources.Profile(),
                        new IdentityServer4.Models.IdentityResources.Phone(),
                        new IdentityServer4.Models.IdentityResources.Address(),
                        role
                    };
            }
    }
}
