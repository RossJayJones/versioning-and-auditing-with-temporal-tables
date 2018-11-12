using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Host.Infrastructure.Query.Dtos;

namespace Host.Infrastructure.Query
{
    public class GetCustomerByVersionIdQuery
    {
        private readonly IDbConnection _connection;

        public GetCustomerByVersionIdQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<CustomerDto> Execute(int customerId, int versionId)
        {
            var dtos = await _connection.QueryAsync<CustomerDto>(Sql,  new { CustomerId = customerId, VersionId = versionId });
            return dtos.Single();
        }

        private const string Sql = @"

            DECLARE @Timestamp DATETIME;

            SELECT 
                @Timestamp = [Timestamp]
            FROM 
	            [Version]
            WHERE
	            [Id] = @VersionId;

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
    }
}