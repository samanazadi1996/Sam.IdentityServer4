using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Services.WebApi.Infrastructure.Configurations.AuthenticationConfigurations
{
    public static class AuthenticationConfigurationExtentions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, string identityServerUrl)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.ApiName = Assembly.GetEntryAssembly().GetName().Name;
                    options.Authority = identityServerUrl;
                });

            return services;
        }
    }
}
