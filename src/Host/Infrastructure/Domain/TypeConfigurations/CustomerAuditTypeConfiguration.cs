using System.Collections.Generic;
using Domain;
using Host.Infrastructure.Domain.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Host.Infrastructure.Domain.TypeConfigurations
{
    public class CustomerAuditTypeConfiguration : IEntityTypeConfiguration<CustomerAudit>
    {
        public void Configure(EntityTypeBuilder<CustomerAudit> builder)
        {
            builder.Property<int>("Id").UseSqlServerIdentityColumn();
            builder.Property(p => p.Messages).HasConversion(new JsonValueConverter<IReadOnlyCollection<string>>(new JsonSerializerSettings()));
        }
    }
}
