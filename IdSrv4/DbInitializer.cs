using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Linq;

namespace IdSrv4
{
    public class DbInitializer
    {
        private readonly ConfigurationDbContext _configurationDbContext;

        public DbInitializer(ConfigurationDbContext configurationDbContext            )
        {
            _configurationDbContext = configurationDbContext;
        }

        public void  InitializeData()
        {
            var apiResources = Resources.GetApiResources();
            foreach (var x in apiResources) {
                AddApiResource(x);
            }
            var findIdentityResource = _configurationDbContext.IdentityResources.SingleOrDefault(
                x => x.Name == "role");
            if (findIdentityResource == null)
            {
                var roleResource = new IdentityServer4.EntityFramework.Entities.IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<IdentityClaim>
                    {
                        new IdentityClaim
                        {
                            Type = "role"
                        }
                    }
                };
                _configurationDbContext.IdentityResources.Add(roleResource);
            }
            AddResouce("profile");
            AddResouce("openid");
            AddResouce("email");

            var findClients = _configurationDbContext.Clients.SingleOrDefault(c =>
            c.ClientId == "openIdConnectClient");

            if (findClients == null)
            {
                IdentityServer4.EntityFramework.Entities.Client entity =
                    new IdentityServer4.EntityFramework.Entities.Client();
                entity.ClientId = "openIdConnectClient";
                entity.ClientName = "Example Implicit Client Application";
                entity.AllowedScopes = new List<ClientScope>()
                {
                    new ClientScope
                    {
                       Scope = "openid"
                    },
                    new ClientScope
                    {
                        Scope="profile"
                    },
                    new ClientScope
                    {
                        Scope="email"
                    },
                    new ClientScope
                    {
                        Scope="role"
                    },
                    new ClientScope
                    {
                        Scope="customAPI.write"
                    }

                };
                entity.RedirectUris = new List<IdentityServer4.EntityFramework.Entities.ClientRedirectUri>
                {
                    new IdentityServer4.EntityFramework.Entities.ClientRedirectUri
                    {
                        RedirectUri = "https://localhost:44330/signin-oidc"
                    }
                };
                entity.PostLogoutRedirectUris = new List<IdentityServer4.EntityFramework.Entities.ClientPostLogoutRedirectUri>
                {
                    new IdentityServer4.EntityFramework.Entities.ClientPostLogoutRedirectUri
                    {
                        PostLogoutRedirectUri = "https://localhost:44330"
                    }
                };
                entity.AllowedGrantTypes = new List<IdentityServer4.EntityFramework.Entities.ClientGrantType>
                {
                    new ClientGrantType
                    {
                        GrantType = "implicit"
                    }
                };



                _configurationDbContext.Clients.Add(entity);

                _configurationDbContext.SaveChanges();

                // var client2 = clients.FirstOrDefault(x => x.ClientId == "oauthClient");



            }
            var findClient2 = _configurationDbContext.Clients
                 .SingleOrDefault(c => c.ClientId == "oauthClient");
            if (findClient2 == null)
            {
                IdentityServer4.EntityFramework.Entities.Client entity2 =
                    new IdentityServer4.EntityFramework.Entities.Client();
                entity2.ClientId = "oauthClient";
                entity2.ClientName = "Example Client Credentials Client Application";
                entity2.AllowedGrantTypes = new List<ClientGrantType>
                    {
                        new ClientGrantType
                        {
                            GrantType = "client_credentials"
                        }
                    };
                entity2.ClientSecrets = new List<ClientSecret>
                    {
                       new ClientSecret
                       {
                           Value = "superSecretPassword".Sha256()
                       }
                    };
                entity2.AllowedScopes = new List<ClientScope>
                    {
                        new ClientScope
                        {
                            Scope = "customAPI.read"
                        }
                    };

                _configurationDbContext.Add(entity2);
                _configurationDbContext.SaveChanges();


            }
        }
        private void AddApiResource(IdentityServer4.Models.ApiResource model)
        {
            if (_configurationDbContext.ApiResources.SingleOrDefault(x=>x.Name == model.Name) == null) {
                var apiResource = new IdentityServer4.EntityFramework.Entities.ApiResource {
                    Name = model.Name,
                    Description = model.Description,
                    DisplayName = model.DisplayName

                };
                foreach (var claim in model.UserClaims) {
                    apiResource.UserClaims.Add(new ApiResourceClaim {
                        Type = claim
                    });
                }
                foreach (var secret in model.ApiSecrets) {
                    apiResource.Secrets.Add(new ApiSecret {
                        Value = secret.Value
                    });
                }
                foreach (var scope in model.Scopes) {
                    apiResource.Scopes.Add(new ApiScope {
                        Name = scope.Name
                    });
                }
                _configurationDbContext.ApiResources.Add(apiResource);
                _configurationDbContext.SaveChanges();
            }
        }
        private void AddResouce(string resouceName)
        {
            if (_configurationDbContext.IdentityResources.SingleOrDefault(x => x.Name == resouceName) == null)
            {
                var profileResouce = new IdentityServer4.EntityFramework.Entities.IdentityResource
                {
                    Name = resouceName
                };
                _configurationDbContext.IdentityResources.Add(profileResouce);
                _configurationDbContext.SaveChanges();
            }
        }
    }
}
