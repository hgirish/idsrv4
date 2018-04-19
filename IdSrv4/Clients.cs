using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdSrv4
{
    internal class Clients
    {
        public static IEnumerable<Client> Get() => new List<Client> {
                new Client {
                    ClientId = "oauthClient",
                    ClientName = "Example Client Credentials Client Application",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {
                        new Secret("superSecretPassword".Sha256())
                    },
                    AllowedScopes = new List<string>{"customAPI.read"}
                },
                new Client
                {
                    ClientId = "openIdConnectClient",
                    ClientName = "Example Implicit Client Application",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "role",
                        "customAPI.write"
                    },
                    RedirectUris = new List<string>{"https://localhost:44330/signin-oidc"},
                    PostLogoutRedirectUris = new List<string>{"https://localhost:44330"}
                },
                new Client
                {
                    ClientId = "angular-spa",
                    ClientName = "Angular 5 Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new List<string>{"openid","profile","api1"},
                    RedirectUris = new List<string>{"http://localhost:4200/auth-callback","http://localhost:4200/silent-refresh.html"},
                    PostLogoutRedirectUris = new List<string>{"http://localhost:4200/"},
                    AllowedCorsOrigins = new List<string>{"http://localhost:4200"},
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    ClientId = "mean-rsvp",
                    ClientName = "Angular Mean RSVP spa",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new[] {"openid", "profile"},
                    RedirectUris = new[]{ "http://localhost:4200/callback" },
                    PostLogoutRedirectUris = new[]{"http://localhost:4200/"},
                    AllowedCorsOrigins = new[]{"http://localhost:4200"},
                    AllowAccessTokensViaBrowser = true

                }
            };
    }
}
