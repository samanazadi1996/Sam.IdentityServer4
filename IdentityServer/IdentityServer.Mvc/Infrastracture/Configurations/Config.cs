using IdentityServer4.Models;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer.Mvc.Infrastracture.Configurations
{
    public class IdentityServerConfigurations
    {
        public List<IdentityServerApiScope> ApiScopes { get; set; }
        public List<IdentityServerClient> Clients { get; set; }
        public List<IdentityServerApiResource> ApiResources { get; set; }

        public IEnumerable<ApiScope> GetApiScopes()
            => ApiScopes.Select(p => new ApiScope(p.Name, p.DisplayName, p.UserClaims));
        public IEnumerable<Client> GetClients()
            => Clients.Select(p =>
                new Client
                {
                    ClientId = p.ClientId,
                    ClientName = p.ClientName,
                    ClientSecrets = p.ClientSecrets.Select(s => new Secret(s.Sha256())).ToList(),
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = p.RedirectUris.ToList(),
                    AllowedCorsOrigins = p.AllowedCorsOrigins.ToList(),
                    AllowedScopes = p.AllowedScopes.ToList()
                });

        public IEnumerable<ApiResource> GetApiResources() =>
            ApiResources.Select(p => new ApiResource(p.Name, p.DisplayName)
            { Scopes = p.Scopes });
    }
    public class IdentityServerApiResource
    {
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public List<string> Scopes { get; set; }
    }
    public class IdentityServerApiScope
    {
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public List<string> UserClaims { get; set; }
    }
    public class IdentityServerClient
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public List<string> ClientSecrets { get; set; }
        public List<string> RedirectUris { get; set; }
        public List<string> AllowedCorsOrigins { get; set; }
        public List<string> AllowedScopes { get; set; }
    }
}
