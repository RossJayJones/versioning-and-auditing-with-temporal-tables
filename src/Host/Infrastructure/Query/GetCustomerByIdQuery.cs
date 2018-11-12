using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Host.Infrastructure.Query.Dtos;

namespace Host.Infrastructure.Query
{
    public class GetCustomerByIdQuery
    {
        private readonly IDbConnection _connection;

        public GetCustomerByIdQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<CustomerDto> Execute(int customerId, int? auditId)
        {
            var sql = string.Concat(auditId == null ? Sql.ById : Sql.ByAuditId, Sql.Audits);
            using (var multi = await _connection.QueryMultipleAsync(sql, new
            {
                CustomerId = customerId,
                AuditId = auditId
            }))
            {
                var customer = (await multi.ReadAsync<CustomerDto>()).Single();
                customer.Audits = (await multi.ReadAsync<CustomerAuditDto>()).ToList();
                return customer;
            }
        }

        private class Sql
        {
            public const string ByAuditId = @"

                DECLARE @Timestamp DATETIME;

                SELECT 
                    @Timestamp = [Timestamp]
                FROM 
	                [Audit]
                WHERE
	                [Id] = @AuditId;

                SELECT
                    [Id],
                    [Name],
                    [Addresses]
                FROM
                    [v_Customer]
                FOR
                    SYSTEM_TIME AS OF @Timestamp
                WHERE
                    [Id] = @CustomerId;
            ";

            public const string ById = @"

                SELECT
                    [Id],
                    [Name],
                    [Addresses]
                FROM
                    [v_Customer]
                WHERE
                    [Id] = @CustomerId;
            ";

            public const string Audits = @"

                SELECT
                    [Id],
                    [Timestamp],
                    [Messages]
                FROM
                    [Audit]
                WHERE
                    [CustomerId] = @CustomerId;

            ";
        }
    }
}
