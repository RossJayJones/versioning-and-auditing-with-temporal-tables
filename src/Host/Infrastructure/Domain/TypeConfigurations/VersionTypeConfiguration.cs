using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Host.Infrastructure.Domain.TypeConfigurations
{
    public class VersionTypeConfiguration : IEntityTypeConfiguration<Version>
    {
        public void Configure(EntityTypeBuilder<Version> builder)
        {
            builder.Property<int>("Id").UseSqlServerIdentityColumn();
            builder.Property(p => p.Timestamp).ValueGeneratedOnAdd();
        }
    }

}