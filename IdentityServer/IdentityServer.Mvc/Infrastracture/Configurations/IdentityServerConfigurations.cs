using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer.Mvc.Infrastracture.Configurations
{
    public class IdentityServerConfigurations
    {

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            var claims = new List<string>(){
                    ClaimTypes.Email,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.PhoneNumber,
                    JwtClaimTypes.PhoneNumberVerified,
                    JwtClaimTypes.IdentityProvider,
                    JwtClaimTypes.AuthenticationMethod,
                    JwtClaimTypes.AuthenticationTime,
            };

            return new List<ApiScope>(){
                new ApiScope("Services.WebApi", "Services.WebApi", claims),
                new ApiScope("Services.WebMvc", "Services.WebMvc", claims)
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>(){
                new Client
                {
                    ClientId = "Services.WebApi",
                    ClientName = "Services.WebApi",
                    ClientSecrets= {new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris ={ "https://localhost:6001/swagger/oauth2-redirect.html"},
                    AllowedCorsOrigins = { "https://localhost:6001"},
                    AllowedScopes = { "Services.WebApi" }
                },
                new Client
                {
                    ClientId = "Services.WebMvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:7001/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:7001/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }

            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>(){
                new ApiResource("Services.WebApi", "Services.WebApi"){ Scopes = {"Services.WebApi" } },
                new ApiResource("Services.WebMvc", "Services.WebMvc"){ Scopes = { "Services.WebMvc" } },

            };
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }
}
