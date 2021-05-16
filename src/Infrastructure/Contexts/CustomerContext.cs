using CodeChallenge.CustomerService.Data;
using CodeChallenge.CustomerService.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.CustomerService.Infrastructure.Contexts
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
        }
    }
}