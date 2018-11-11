using System;

namespace Host.Infrastructure.Query.Dtos
{
    public class CustomerVersionDto
    {
        public int Id { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public string Message { get; set; }
    }
}
