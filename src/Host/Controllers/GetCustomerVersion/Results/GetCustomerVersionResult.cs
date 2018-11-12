using System.Collections.Generic;

namespace Host.Controllers.GetCustomerVersion.Results
{
    public class GetCustomerVersionResult
    {
        public string Name { get; set; }

        public ICollection<GetCustomerVersionAddressResult> Addresses { get; set; }
    }
}
