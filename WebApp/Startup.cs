using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using microbank.Data.Models;
using microbank.Data;
using microbank.Services;

using System.IO;

namespace microbank
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
            services.AddMvc();

            var connectionString = _getConnectionString();
            
            _migrateDatabase(connectionString);

            services.AddDbContext<MicroBankContext>(options => options.UseSqlServer(connectionString));	

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MicroBankContext>()
                .AddDefaultTokenProviders();

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                });

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();
        }

        private static void _migrateDatabase(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new MicroBankContext(optionsBuilder.Options))
            {
                context.Database.Migrate();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();
        }

        private string _getConnectionString(){
            // Get connection string from injected env variable
            // fallback to the connection string from appconfig
            var injectedConnectionString = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING");

            if(!string.IsNullOrWhiteSpace(injectedConnectionString))
                return injectedConnectionString;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var connectionStringConfig = builder.Build();
            var connectionString = connectionStringConfig.GetConnectionString("microbank_connection");

            return connectionString;
        }
    }
}
