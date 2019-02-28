using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NamedClientsPattern
{
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
            AddNamedHttpClients(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private void AddNamedHttpClients(IServiceCollection services)
        {
            services.AddHttpClient("FakePosts", client =>
            {
                client.BaseAddress = new Uri(@"https://jsonplaceholder.typicode.com");
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            });

            services.AddHttpClient("GitHub", client =>
            {
                client.BaseAddress = new Uri(@"https://api.github.com");
                // Github API versioning
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                // Github requires a user-agent
                client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
