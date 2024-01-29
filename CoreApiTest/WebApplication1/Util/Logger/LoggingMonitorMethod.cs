﻿using Net6Api.Domain;

namespace Net6Api.Util.Logger
{
    public sealed class LoggingMonitorMethod
    {
        /// <summary>
        /// 方法名称
        /// </summary>
        /// <remarks>完全限定名</remarks>
        public string FullName { get; set; }

        /// <summary>
        /// 是否记录返回值
        /// </summary>
        /// <remarks>bool 类型，默认输出</remarks>
        public bool WithReturnValue { get; set; } = true;

        /// <summary>
        /// 设置返回值阈值
        /// </summary>
        /// <remarks>配置返回值字符串阈值，超过这个阈值将截断，默认全量输出</remarks>
        public int ReturnValueThreshold { get; set; } = 0;

        /// <summary>
        /// 配置 Json 输出行为
        /// </summary>
        public JsonBehavior JsonBehavior { get; set; } = JsonBehavior.None;

        /// <summary>
        /// 配置序列化忽略的属性名称
        /// </summary>
        public string[] IgnorePropertyNames { get; set; }

        /// <summary>
        /// 配置序列化忽略的属性类型
        /// </summary>
        public Type[] IgnorePropertyTypes { get; set; }

        /// <summary>
        /// JSON 输出格式化
        /// </summary>
        public bool JsonIndented { get; set; } = false;

        /// <summary>
        /// 序列化属性命名规则（返回值）
        /// </summary>
       
    }
}
