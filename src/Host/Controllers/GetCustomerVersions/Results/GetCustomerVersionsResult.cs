using System;

namespace Host.Controllers.GetCustomerVersions.Results
{
    public class GetCustomerVersionsResult
    {
        public int Id { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public string Message { get; set; }
    }
}
