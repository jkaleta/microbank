using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using microbank.Data;
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
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var connectionStringConfig = builder.Build();
            var connectionString = connectionStringConfig.GetConnectionString("microbank_connection");

            var injectedConnectionString = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING");

            services.AddMvc();

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(connectionString);
            using(var context = new MicroBankContext(optionsBuilder.Options)){
                //context.Database.Migrate();
            }

            services.AddDbContext<MicroBankContext>(options => options.UseSqlServer(injectedConnectionString));
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
