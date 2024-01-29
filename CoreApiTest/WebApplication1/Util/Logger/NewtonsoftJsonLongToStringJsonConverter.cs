﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Net6Api.Util.Logger
{
    public class NewtonsoftJsonLongToStringJsonConverter : JsonConverter<long>
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
        public override long ReadJson(JsonReader reader, Type objectType, long existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jt = JValue.ReadFrom(reader);
            return jt.Value<long>();
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, long value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
        }
    }
    public class NewtonsoftJsonNullableLongToStringJsonConverter : JsonConverter<long?>
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
        public override long? ReadJson(JsonReader reader, Type objectType, long? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jt = JValue.ReadFrom(reader);
            return jt.Value<long?>();
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, long? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value?.ToString());
        }
    }

}
