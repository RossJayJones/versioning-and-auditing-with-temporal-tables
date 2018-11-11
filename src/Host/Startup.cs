using System.Data;
using System.Data.SqlClient;
using Host.Infrastructure.Domain;
using Host.Infrastructure.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Host
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            TypeMapperHelper.Register();
               var configuration = ConfigurationHelper.CreateConfiguration();
            services.AddDbContext<DomainDbContext>(opts => opts.UseSqlServer(configuration["sql"]));
            services.AddTransient<GetCustomersQuery>();
            services.AddTransient<GetCustomerAuditsQuery>();
            services.AddTransient<GetCustomerVersionsQuery>();
            services.AddTransient<GetCustomerByAuditIdQuery>();
            services.AddTransient<GetCustomerByVersionIdQuery>();
            services.Add(new ServiceDescriptor(typeof(IDbConnection), p => new SqlConnection(configuration["sql"]), ServiceLifetime.Scoped));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
