using System;
using System.Collections.Generic;

namespace Host.Controllers.GetCustomer.Results
{
    public class GetCustomerAuditResult
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public ICollection<string> Messages { get; set; }
    }
}
