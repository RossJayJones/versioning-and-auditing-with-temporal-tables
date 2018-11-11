using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Host.Infrastructure.Query.Dtos;

namespace Host.Infrastructure.Query
{
    public class GetCustomersQuery
    {
        private readonly IDbConnection _connection;

        public GetCustomersQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IReadOnlyCollection<CustomerDto>> Execute()
        {
            var dtos = await _connection.QueryAsync<CustomerDto>(Sql);
            return dtos.ToList();
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
        ";
    }
}
