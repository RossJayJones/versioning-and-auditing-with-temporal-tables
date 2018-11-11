using System.Collections.Generic;

namespace Host.Controllers.GetCustomer.Results
{
    public class GetCustomerResult
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<GetCustomerAddressResult> Addresses { get; set; }
    }
}
