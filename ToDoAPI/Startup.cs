using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repositories;
using Infrastructure.Mongo;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ToDoAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			// Add framework services.
			services.AddMvc()
                    .AddJsonOptions(x => x.SerializerSettings.Formatting = Formatting.Indented);
            //services.AddAuthorization();
			services.AddScoped<IItemRepository, ItemInMemoryRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IItemService, ItemService>();
			services.AddScoped<IUserService, UserService>();
			services.AddSingleton<IJwtHandler, JwtHandler>();
			services.Configure<JwtSettings>(Configuration.GetSection("jwt"));
            services.Configure<MongoSettings>(Configuration.GetSection("mongo"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            MongoConfigurator.Initialize();

            var jwtSettings = app.ApplicationServices.GetService<IOptions<JwtSettings>>();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                 AutomaticAuthenticate = true,
                 TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidIssuer = jwtSettings.Value.Issuer,
                     ValidateAudience = false,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Key))
                 }
                
            });

            app.UseMvc();
        }
    }
}
