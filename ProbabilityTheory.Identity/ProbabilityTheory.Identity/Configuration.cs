using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace ProbabilityTheory.Identity
{
    public static class Configuration
    {
        public static ClientSettings Settings { get; set; } = new ClientSettings();
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope(Settings.Scope, "Web API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource(Settings.Scope, "Web API",
                    new [] { JwtClaimTypes.Name })
                {
                    Scopes = { Settings.Scope }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = Settings.ClientId,
                    ClientName = Settings.ClientName,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        Settings.Host + "/signin-oidc"
                    },
                    AllowedCorsOrigins =
                    {
                        Settings.Host
                    },
                    PostLogoutRedirectUris =
                    {
                        Settings.Host + "/signout-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        Settings.Scope
                    },
                    AllowAccessTokensViaBrowser = true,
                }
            };
    }
}
