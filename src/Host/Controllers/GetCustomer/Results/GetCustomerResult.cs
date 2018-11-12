using System.Collections.Generic;

namespace Host.Controllers.GetCustomer.Results
{
    public class GetCustomerResult
    {
        public string Name { get; set; }

        public ICollection<GetCustomerAddressResult> Addresses { get; set; }

        public ICollection<GetCustomerAuditResult> Audits { get; set; }
    }
}
