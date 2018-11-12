using System.Collections.Generic;
using Domain;
using Host.Infrastructure.Domain.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Host.Infrastructure.Domain.TypeConfigurations
{
    public class AuditTypeConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> builder)
        {
            builder.Property<int>("Id").UseSqlServerIdentityColumn();
            builder.Property(p => p.Timestamp).ValueGeneratedOnAdd();
            builder.Property(p => p.Messages).HasConversion(new JsonValueConverter<IReadOnlyCollection<string>>(new JsonSerializerSettings()));
        }
    }
}
