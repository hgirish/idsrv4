using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SampleApi {
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddJsonFormatters()
                .AddAuthorization();

            services.AddCors();
            services.AddDistributedMemoryCache();

            services.AddAuthentication(
                IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(
                options => {
                    options.Authority = Constants.Constants.Authority;
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "api1";
                    options.ApiSecret = "superSecretPassword";                   

                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(policy => {
                policy.WithOrigins("http://localhost:28895",
                    "http://localhost:7017");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.WithExposedHeaders("WWW-Authenticate");
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
