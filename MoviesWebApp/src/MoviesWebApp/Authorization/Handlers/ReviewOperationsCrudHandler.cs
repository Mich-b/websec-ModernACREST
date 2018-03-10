using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using MoviesWebApp.Authorization.Requirements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoviesWebApp.Authorization.Requirements.ReviewOperationsCrudHandler;

namespace MoviesWebApp.Authorization.Handlers
{
    public class ReviewOperationsCrudHandler:
        AuthorizationHandler<OperationAuthorizationRequirement, MoviesLibrary.MovieReview>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       OperationAuthorizationRequirement requirement,
                                                       MoviesLibrary.MovieReview review)
        {

            // Succeed if the scope array contains the required scope
            //Always succeed if the role is admin
            if (context.User.HasClaim("role", "Admin") &&
                requirement.Name == ReviewOperations.Delete.Name)
                context.Succeed(requirement);

            //Only if you wrote the review, you can edit it
            if (requirement == ReviewOperations.Update)
            {
                var sub = context.User.FindFirst("sub")?.Value;
                if (sub != null && review.UserId == sub)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
