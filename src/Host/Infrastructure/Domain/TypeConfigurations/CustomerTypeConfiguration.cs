using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Host.Infrastructure.Domain.TypeConfigurations
{
    public class CustomerTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(p => p.Id).UseSqlServerIdentityColumn();
            builder.HasMany(p => p.Versions).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Audits).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}