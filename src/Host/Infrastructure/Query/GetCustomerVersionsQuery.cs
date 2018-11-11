using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Host.Infrastructure.Query.Dtos;

namespace Host.Infrastructure.Query
{
    public class GetCustomerVersionsQuery
    {
        private readonly IDbConnection _connection;

        public GetCustomerVersionsQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IReadOnlyCollection<CustomerVersionDto>> Execute(int customerId)
        {
            var dtos = await _connection.QueryAsync<CustomerVersionDto>(Sql, new { CustomerId = customerId });
            return dtos.ToList();
        }

        private const string Sql = @"
            SELECT
                [Id],
                [Timestamp],
                [Message]
            FROM
                [CustomerVersion]
            WHERE
                [CustomerId] = @CustomerId
        ";
    }
}
