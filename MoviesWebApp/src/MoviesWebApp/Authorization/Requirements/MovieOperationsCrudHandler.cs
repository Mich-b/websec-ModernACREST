using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWebApp.Authorization.Requirements
{
    public class MovieOperationsCrudHandler
    {
        public static class MovieOperations
        {
            public static OperationAuthorizationRequirement Create =
                new OperationAuthorizationRequirement { Name = nameof(Create) };
            public static OperationAuthorizationRequirement Read =
                new OperationAuthorizationRequirement { Name = nameof(Read) };
            public static OperationAuthorizationRequirement Update =
                new OperationAuthorizationRequirement { Name = nameof(Update) };
            public static OperationAuthorizationRequirement Delete =
                new OperationAuthorizationRequirement { Name = nameof(Delete) };
        }
    }
}
