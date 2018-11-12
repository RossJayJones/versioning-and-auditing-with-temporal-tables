using System.Collections.Generic;

namespace Host.Infrastructure.Query.Dtos
{
    public class CustomerDto
    {
        public CustomerDto()
        {
            Addresses = new List<AddressDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IReadOnlyCollection<AddressDto> Addresses { get; set; }

        public IReadOnlyCollection<CustomerAuditDto> Audits { get; set; }
    }
}
