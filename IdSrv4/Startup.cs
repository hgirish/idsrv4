using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace IdSrv4 {
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //const string connectionString =
            //    @"Data Source=(LocalDb)\MSSQLLocalDB;database=Test.IdentityServer4.EntityFramework;trusted_connection=yes;";
            const string connectionString = "Data Source=..\\DB\\Test.IdentityServer.db";
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(builder =>        
            builder.UseSqlite(connectionString, sqloptions => sqloptions.MigrationsAssembly(migrationAssembly)));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddConfigurationStore(options =>
                options.ConfigureDbContext = builder =>
                 builder.UseSqlite(connectionString, sqlOptions =>
                 sqlOptions.MigrationsAssembly(migrationAssembly)))
                .AddAspNetIdentity<IdentityUser>()
                .AddDeveloperSigningCredential()
                .AddOperationalStore(options =>
                options.ConfigureDbContext = builder =>
                builder.UseSqlite(connectionString,
                sqlOptions => sqlOptions.MigrationsAssembly(migrationAssembly)));

            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddSerilog();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Map("/api", api =>
            {
                api.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
              
                api.Run(async context =>
                {
                    await context.Response.WriteAsync("API Response");
                });
            });
            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
