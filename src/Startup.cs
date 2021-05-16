using CodeChallenge.CustomerService.Infrastructure.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.CustomerService.Customers;
using CodeChallenge.CustomerService.Repositories;
using CodeChallenge.CustomerService.Services;
using HotChocolate.Types;

namespace CodeChallenge.CustomerService
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
            services.AddPooledDbContextFactory<CustomerDbContext>(options => options.UseSqlServer("Server=localhost;Database=master;User Id=sa;Password=Your_strong_password1;"));
            services.AddScoped<CustomerDbContext>(p => p.GetRequiredService<IDbContextFactory<CustomerDbContext>>().CreateDbContext());
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService.Services.CustomerService>();

            services.AddGraphQLServer()
                .AddType<CustomerStatusType>()
                .AddQueryType<CustomerQueriesType>()
                .AddMutationType<CustomerMutationsType>()
                .AddType<EmailAddressType>()
                .AddType<NonEmptyStringType>()
                .AddProjections();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGraphQL();
                });
        }
    }
}
