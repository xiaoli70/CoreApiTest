using Newtonsoft.Json.Serialization;

namespace Net6Api.Util.Logger
{
    internal sealed class CamelCasePropertyNamesContractResolverWithIgnoreProperties : CamelCasePropertyNamesContractResolver
    {
        /// <summary>
        /// 被忽略的属性名称
        /// </summary>
        private readonly string[] _names;

        /// <summary>
        /// 被忽略的属性类型
        /// </summary>
        private readonly Type[] _type;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="names"></param>
        /// <param name="types"></param>
        public CamelCasePropertyNamesContractResolverWithIgnoreProperties(string[] names, Type[] types)
        {
            _names = names ?? Array.Empty<string>();
            _type = types ?? Array.Empty<Type>();
        }
    }
    internal sealed class DefaultContractResolverWithIgnoreProperties : DefaultContractResolver
    {
        /// <summary>
        /// 被忽略的属性名称
        /// </summary>
        private readonly string[] _names;

        /// <summary>
        /// 被忽略的属性类型
        /// </summary>
        private readonly Type[] _type;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="names"></param>
        /// <param name="types"></param>
        public DefaultContractResolverWithIgnoreProperties(string[] names, Type[] types)
        {
            _names = names ?? Array.Empty<string>();
            _type = types ?? Array.Empty<Type>();
        }
    }
    }
