using System;

namespace Domain
{
    public class Version
    {
        internal Version()
        {
            
        }

        internal Version(string message) : this()
        {
            Message = message;
        }

        public string Message { get; set; }

        public DateTime Timestamp { get; private set; }
    }
}
