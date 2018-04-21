using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdSrv4
{
    internal class Clients
    {
        public static IEnumerable<Client> Get() => new List<Client> {
            new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },
            new Client
            {
                ClientId = "ro.client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },AllowedScopes = {"api1"}
            },
            new Client{
                ClientId = "mvc",
                ClientName ="MVC Client",
                AllowedGrantTypes = GrantTypes.Implicit,
                RedirectUris = {"http://localhost:5002/signin-oidc"},
                PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            },
                new Client{
                ClientId = "mvchybrid",
                ClientName ="MVC Hybrid Client",
                AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                RedirectUris = {"http://localhost:5002/signin-oidc"},
                PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1"
                },
                AllowOfflineAccess = true
            },
                new Client {
                    ClientId = "oauthClient",
                    ClientName = "Example Client Credentials Client Application",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {
                        new Secret("superSecretPassword".Sha256())
                    },
                    AllowedScopes = new List<string>{"customAPI.read", "api1"}
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

                },
                new Client
                {
                    ClientId = "auth-oidc-sample",
                    ClientName = "Angular Auth Oidc Sample",
                    AllowedGrantTypes = GrantTypes.Implicit,
                     AllowedScopes = new[] {"openid", "email", "profile"},
                    RedirectUris = new[]{ "http://localhost:4200" },
                    PostLogoutRedirectUris = new[]{"http://localhost:4200/Unauthorized"},
                    AllowedCorsOrigins = new[]{"http://localhost:4200"},
                    AllowAccessTokensViaBrowser = true,
                    
                }
            };
    }
}
