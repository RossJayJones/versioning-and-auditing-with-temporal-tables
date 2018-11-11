using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Host.Infrastructure.Query.Dtos;

namespace Host.Infrastructure.Query
{
    public class GetCustomerByAuditIdQuery
    {
        private readonly IDbConnection _connection;

        public GetCustomerByAuditIdQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<CustomerDto> Execute(int customerId, int auditId)
        {
            var dtos = await _connection.QueryAsync<CustomerDto>(Sql, new { CustomerId = customerId, VersionId = auditId });
            return dtos.Single();
        }

        private const string Sql = @"
            SELECT
                [Id],
                [Name],
                JSON_QUERY((
	                SELECT
		                [Address].[Line],
		                [Address].[Suburb],
		                [Address].[City],
		                [Address].[Province],
		                [Address].[Code]
                    FROM
                        [Address]
                    WHERE
                        [Address].[CustomerId] = [Customer].[Id]
	                FOR JSON PATH
                )) AS [Addresses]
            FROM
                [Customer]
            WHERE
                [Id] = @CustomerId
        ";
    }
}