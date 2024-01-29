using SqlSugar;

namespace Net6Api.Domain.Helper
{
    public static class SqlsugarSetup
    {
        public static void AddSqlsugarSetup(this IServiceCollection services, IConfiguration configuration,
         string dbName = "ConnectString")
        {
            SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
            {
                //如果是mysql，换成SqlSugar.DbType.MySql
                DbType = SqlSugar.DbType.SqlServer,
                ConnectionString = configuration[dbName],
                IsAutoCloseConnection = true,
            },
                db =>
                {
                    //单例参数配置，所有上下文生效       
                    db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        Console.WriteLine(sql);//输出sql
                    };
                });
            services.AddSingleton<ISqlSugarClient>(sqlSugar);//这边是SqlSugarScope用AddSingleton
        }
    }

}
