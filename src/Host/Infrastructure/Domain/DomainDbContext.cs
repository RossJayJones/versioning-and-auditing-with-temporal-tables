using Host.Infrastructure.Domain.TypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Host.Infrastructure.Domain
{
    public class DomainDbContext : DbContext
    {
        public DomainDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AddressTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AuditTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VersionTypeConfiguration());
        }
    }
}
