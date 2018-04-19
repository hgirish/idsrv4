using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdSrv4
{
    public class DbInitializer
    {
        private readonly ConfigurationDbContext _configurationDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IServiceProvider _services;

        public DbInitializer(IServiceProvider services)
        {
            _configurationDbContext = services.GetRequiredService<ConfigurationDbContext>();
            _userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            _services = services;
        }

        public async Task InitializeDataAsync()
        {
            

            InsertClients();

            InsertIdentityResources();

            InsertApiResources();

            await InsertUsers();

        }

    

        private async Task InsertUsers()
        {
            var users = Users.Get();
            foreach (var user in users)
            {
                var exists = await _userManager.FindByNameAsync(user.Username);
                if (exists == null)
                {


                    var identityUser = new IdentityUser(user.Username)
                    {
                        Id = user.SubjectId
                    };
                    await _userManager.CreateAsync(identityUser, user.Password);
                    await _userManager.AddClaimsAsync(identityUser, user.Claims.ToList());
                }
            }
        }

        private void InsertApiResources()
        {
            var apiResources = Resources.GetApiResources();
            foreach (var apiResource in apiResources)
            {
                if (!_configurationDbContext.ApiResources.Any(x => x.Name == apiResource.Name))
                {
                    _configurationDbContext.ApiResources.Add(apiResource.ToEntity());
                }

            }
            _configurationDbContext.SaveChanges();
        }

        private void InsertIdentityResources()
        {
            var identityResources = Resources.GetIdentityResources();
            foreach (var identityResource in identityResources)
            {
                if (_configurationDbContext.IdentityResources.SingleOrDefault(
                    i => i.Name == identityResource.Name) == null)
                {
                    _configurationDbContext.IdentityResources.Add(identityResource.ToEntity());
                }
            }
            _configurationDbContext.SaveChanges();
        }

        private void InsertClients()
        {
            var clients = Clients.Get();
            foreach (var client in clients)
            {
                if (_configurationDbContext.Clients.SingleOrDefault(
                    c => c.ClientId == client.ClientId) == null)
                {
                    _configurationDbContext.Clients.Add(client.ToEntity());
                }
            }
            _configurationDbContext.SaveChanges();
        }

        private void Migrate()
        {
            _services.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
            _configurationDbContext.Database.Migrate();
            _services.GetRequiredService<ApplicationDbContext>().Database.Migrate();
        }




    }
}
