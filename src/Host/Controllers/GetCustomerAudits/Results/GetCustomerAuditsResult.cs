using System;
using System.Collections.Generic;

namespace Host.Controllers.GetCustomerAudits.Results
{
    public class GetCustomerAuditsResult
    {
        public int Id { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public IReadOnlyCollection<string> Messages { get; set; }
    }
}
