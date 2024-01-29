using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.Encodings.Web;

namespace Net6Api.Util.Logger
{
    public class JsonElementConverter : JsonConverter<System.Text.Json.JsonElement>
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="hasExistingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override System.Text.Json.JsonElement ReadJson(JsonReader reader, Type objectType, System.Text.Json.JsonElement existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = JValue.ReadFrom(reader).Value<string>();
            return (System.Text.Json.JsonElement)System.Text.Json.JsonSerializer.Deserialize<object>(value);
        }

        public override void WriteJson(JsonWriter writer, System.Text.Json.JsonElement value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
