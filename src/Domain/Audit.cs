using System;
using System.Collections.Generic;

namespace Domain
{
    public class Audit
    {
        private List<string> _messages;

        internal Audit()
        {
            _messages = new List<string>();
        }

        public DateTime Timestamp { get; private set; }

        public IReadOnlyCollection<string> Messages => _messages;

        internal void AddMessage(string message)
        {
            _messages.Add(message);
        }
    }
}
