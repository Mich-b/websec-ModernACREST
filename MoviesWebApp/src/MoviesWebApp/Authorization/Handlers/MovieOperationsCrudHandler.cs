using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoviesWebApp.Authorization.Requirements.MovieOperationsCrudHandler;

namespace MoviesWebApp.Authorization.Handlers
{
    public class MovieOperationsCrudHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, MoviesLibrary.MovieDetails>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                      OperationAuthorizationRequirement requirement,
                                                      MoviesLibrary.MovieDetails movie)

        {
            //Only reviewers can create new reviews
            if (context.User.HasClaim("role", "Reviewer") &&
                requirement.Name == MovieOperations.Create.Name)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
