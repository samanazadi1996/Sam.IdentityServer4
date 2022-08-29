using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Services.WebApi.Infrastructure.Configurations.SwaggerConfigurations
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize =
              context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
              || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

            if (hasAuthorize)
            {
                operation.Responses.Add(((int)HttpStatusCode.Unauthorized).ToString(), new OpenApiResponse { Description = HttpStatusCode.Unauthorized.ToString() });
                operation.Responses.Add(((int)HttpStatusCode.Forbidden).ToString(), new OpenApiResponse { Description = HttpStatusCode.Forbidden.ToString() });

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecurityScheme {Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"}
                            }
                        ] = new[] { Assembly.GetCallingAssembly().GetName().Name }
                    }
                };

            }
        }
    }

}
