using System;

namespace Domain
{
    public class CustomerVersion
    {
        internal CustomerVersion()
        {
            
        }

        internal CustomerVersion(string message) : this()
        {
            Message = message;
            Timestamp = DateTimeOffset.Now;
        }

        public string Message { get; set; }

        public DateTimeOffset Timestamp { get; private set; }
    }
}
