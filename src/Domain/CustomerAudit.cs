using System;
using System.Collections.Generic;

namespace Domain
{
    public class CustomerAudit
    {
        private List<string> _messages;

        internal CustomerAudit()
        {
            _messages = new List<string>();
            Timestamp = DateTimeOffset.Now;
        }

        public DateTimeOffset Timestamp { get; private set; }

        public IReadOnlyCollection<string> Messages => _messages;

        internal void AddMessage(string message)
        {
            _messages.Add(message);
        }
    }
}
