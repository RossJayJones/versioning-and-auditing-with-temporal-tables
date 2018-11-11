using System;
using System.Data;
using Dapper;
using Newtonsoft.Json;

namespace Host.Infrastructure.Query.Utility
{
    public class JsonTypeHandler<T> : SqlMapper.TypeHandler<T>
    {
        /// <summary>
        /// Writes are not supported on the query side
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        public override void SetValue(IDbDataParameter parameter, T value)
        {
            throw new NotImplementedException();
        }

        public override T Parse(object value)
        {
            return JsonConvert.DeserializeObject<T>(value.ToString());
        }
    }
}
