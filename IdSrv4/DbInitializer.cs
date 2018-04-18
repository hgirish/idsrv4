using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using System;
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

            var identityResources = Resources.GetIdentityResources();
            foreach (var item in identityResources) {
                AddIdentityResource(item);
            }

            var clients = Clients.Get();
            foreach (var client in clients) {
                AddClient(client);
            }  
        }

        private void AddClient(IdentityServer4.Models.Client model)
        {
         if(_configurationDbContext.Clients.SingleOrDefault(
             c=>c.ClientId == model.ClientId) == null) {
                var entity = new IdentityServer4.EntityFramework.Entities.Client {
                    ClientName = model.ClientName,
                    ClientId = model.ClientId
                };
                foreach (var grant in model.AllowedGrantTypes) {
                    if (entity.AllowedGrantTypes == null) {
                        entity.AllowedGrantTypes = new List<ClientGrantType>();
                    }
                    entity.AllowedGrantTypes.Add(new ClientGrantType {
                        GrantType = grant
                    });
                }
                foreach (var secret in model.ClientSecrets) {
                    if (entity.ClientSecrets == null) {
                        entity.ClientSecrets = new List<ClientSecret>();
                    }
                    entity.ClientSecrets.Add(new ClientSecret {
                        Value = secret.Value
                    });
                }
                foreach (var scope in model.AllowedScopes) {
                    if (entity.AllowedScopes == null) {
                        entity.AllowedScopes = new List<ClientScope>();
                    }
                    entity.AllowedScopes.Add(new ClientScope {
                        Scope = scope
                    });
                }
                foreach (var redirectUrl in model.RedirectUris) {
                    if (entity.RedirectUris == null) {
                        entity.RedirectUris = new List<ClientRedirectUri>();
                    }
                    entity.RedirectUris.Add(new ClientRedirectUri {
                        RedirectUri = redirectUrl
                    });
                }
                foreach (var postRedirectUri in model.PostLogoutRedirectUris) {
                    if (entity.PostLogoutRedirectUris == null) {
                        entity.PostLogoutRedirectUris = new List<ClientPostLogoutRedirectUri>();
                    }
                    entity.PostLogoutRedirectUris.Add(new ClientPostLogoutRedirectUri {
                        PostLogoutRedirectUri = postRedirectUri
                    });
                }

                _configurationDbContext.Clients.Add(entity);
                _configurationDbContext.SaveChanges();
            } 
        }

        private void AddIdentityResource(IdentityServer4.Models.IdentityResource model)
        {
            if (_configurationDbContext.IdentityResources.SingleOrDefault(
                x=>x.Name == model.Name)==null) {

                var identityResource = new IdentityServer4.EntityFramework.Entities.IdentityResource {
                    Name = model.Name

                };
                foreach (var item in model.UserClaims) {
                    if (identityResource.UserClaims == null) {
                        identityResource.UserClaims = new List<IdentityClaim>();
                    }
                    identityResource.UserClaims.Add(new IdentityClaim {
                        Type = item
                    });
                }
                _configurationDbContext.IdentityResources.Add(identityResource);
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
                    if (apiResource.UserClaims == null) {
                        apiResource.UserClaims = new List<ApiResourceClaim>();
                    }
                    apiResource.UserClaims.Add(new ApiResourceClaim {
                        Type = claim
                    });
                }
                foreach (var secret in model.ApiSecrets) {
                    if (apiResource.Secrets == null) {
                        apiResource.Secrets = new List<ApiSecret>();
                    }
                    apiResource.Secrets.Add(new ApiSecret {
                        Value = secret.Value
                    });
                }
                foreach (var scope in model.Scopes) {
                    if (apiResource.Scopes == null) {
                        apiResource.Scopes = new List<ApiScope>();
                    }
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
