using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Host.Infrastructure.Domain.TypeConfigurations
{
    public class CustomerVersionTypeConfiguration : IEntityTypeConfiguration<CustomerVersion>
    {
        public void Configure(EntityTypeBuilder<CustomerVersion> builder)
        {
            builder.Property<int>("Id").UseSqlServerIdentityColumn();
        }
    }

}