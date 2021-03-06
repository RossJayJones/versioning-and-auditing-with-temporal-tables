﻿using System;
using System.Collections.Generic;

namespace Host.Infrastructure.Query.Dtos
{
    public class CustomerAuditDto
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public IReadOnlyCollection<string> Messages { get; set; }
    }
}