using System.Collections.Generic;
using Dapper;
using Host.Infrastructure.Query.Dtos;
using Host.Infrastructure.Query.Utility;

namespace Host.Infrastructure.Query
{
    public static class TypeMapperHelper
    {
        public static void Register()
        {
            SqlMapper.AddTypeHandler(new JsonTypeHandler<IReadOnlyCollection<AddressDto>>());
            SqlMapper.AddTypeHandler(new JsonTypeHandler<IReadOnlyCollection<string>>());
        }
    }
}