namespace Net6Api.Domain
{
    public class LoginDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        /// <example>System</example>
        public string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        /// <example>123456</example>
        public string Password { get; set; }


    }
}