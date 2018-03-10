using Microsoft.AspNetCore.Authorization;
using MoviesWebApp.Authorization.Requirements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWebApp.Authorization.Handlers
{
    public class SearchPermissionHandler : AuthorizationHandler<SearchRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   SearchRequirement requirement)
        {

            //Always succeed if the role is admin
            if (context.User.HasClaim("role", "Admin") ||
                context.User.HasClaim("role", "Reviewer"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
