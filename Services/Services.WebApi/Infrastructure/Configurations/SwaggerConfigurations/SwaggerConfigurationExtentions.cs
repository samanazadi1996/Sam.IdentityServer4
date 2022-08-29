using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Services.WebApi.Infrastructure.Configurations.SwaggerConfigurations
{
    public static class SwaggerConfigurationExtentions
    {
        public static IApplicationBuilder UseSwaggerWithVersioning(this IApplicationBuilder app)
        {
            var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
            var assemblyName = Assembly.GetCallingAssembly().GetName().Name;
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
                options.OAuthClientId(assemblyName);
                options.OAuthAppName($"Swagger UI for {assemblyName}");
                options.OAuthClientSecret("secret");
                options.OAuthUsePkce();
            });

            return app;
        }

        public static IServiceCollection AddSwaggerWithVersioning(this IServiceCollection services, string identityServerUrl)
        {
            var assemblyName = Assembly.GetCallingAssembly().GetName().Name;

            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(setup =>
            {
                setup.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{identityServerUrl}/connect/authorize"),
                            TokenUrl = new Uri($"{identityServerUrl}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {assemblyName, $"{assemblyName} - full access"},
                            }
                        }
                    }
                });


                setup.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return services;
        }
    }

}
