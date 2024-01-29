using SqlSugar;

namespace Net6Api.Domain
{
    public class UserInfo
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}