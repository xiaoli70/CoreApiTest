﻿namespace Net6Api.Util.Logger
{
    public sealed class LogContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public LogContext()
        {
        }

        /// <summary>
        /// 日志上下文数据
        /// </summary>
        public IDictionary<object, object> Properties { get; set; }
    }
}
