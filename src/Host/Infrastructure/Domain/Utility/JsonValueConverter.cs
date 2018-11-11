using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Host.Infrastructure.Domain.Utility
{
    public class JsonValueConverter<TModel> : ValueConverter<TModel, string>
    {
        public JsonValueConverter(JsonSerializerSettings settings)
            : base(data => ToJson(data, settings), data => ToModel(data, settings))
        {
        }

        static string ToJson(TModel model, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(model, settings);
        }

        static TModel ToModel(string json, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<TModel>(json, settings);
        }
    }
}
