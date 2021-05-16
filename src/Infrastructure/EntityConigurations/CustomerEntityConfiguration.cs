using CodeChallenge.CustomerService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeChallenge.CustomerService.Infrastructure.EntityConfigurations
{
    internal class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.IsBlocked).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}