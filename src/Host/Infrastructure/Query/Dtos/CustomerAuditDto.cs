using System;
using System.Collections.Generic;

namespace Host.Infrastructure.Query.Dtos
{
    public class CustomerAuditDto
    {
        public int Id { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public IReadOnlyCollection<string> Messages { get; set; }
    }
}