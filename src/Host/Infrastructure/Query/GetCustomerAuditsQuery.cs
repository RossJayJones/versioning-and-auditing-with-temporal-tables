using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Host.Infrastructure.Query.Dtos;

namespace Host.Infrastructure.Query
{
    public class GetCustomerAuditsQuery
    {
        private readonly IDbConnection _connection;

        public GetCustomerAuditsQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IReadOnlyCollection<CustomerAuditDto>> Execute(int customerId)
        {
            var dtos = await _connection.QueryAsync<CustomerAuditDto>(Sql, new { CustomerId = customerId });
            return dtos.ToList();
        }

        private const string Sql = @"
            SELECT
                [Id],
                [Timestamp],
                [Messages]
            FROM
                [CustomerAudit]
            WHERE
                [CustomerId] = @CustomerId
        ";
    }
}
