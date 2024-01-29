
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net6Api.Domain;
using Net6Api.Domain.Helper;
using SqlSugar;
using System.Data.SqlClient;
using System.Security.Claims;

namespace Net6Api.Controllers
{
    /// <summary>
    /// 地址相关接口
    /// </summary>
    [Route("address")]
    public class AddressController : BaseControllers
    {
        private readonly ISqlSugarClient _SqlSugarDB;

        private readonly IHttpContextAccessor _httpContextAccessor;


        // 通过注入的IHttpContextAccessor获取`HttpContext.User(ClaimsPrinciple)`中对应的Claims信息
        public string? UserName => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
        /// <summary>
        /// 依赖注入
        /// </summary>
        public AddressController(ISqlSugarClient SqlSugarDB, IHttpContextAccessor httpContextAccessor)
        {
            _SqlSugarDB = SqlSugarDB;

            _httpContextAccessor = httpContextAccessor;
        }


        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <param name="size">每页条数</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListAsync(int page = 1, int size = 15)
        {
            string ss = UseStaticHttpContext.UserName;
            List<LoginDto> list = new List<LoginDto>();
            for (int i = 0; i < 5; i++)
            {
                LoginDto login = new LoginDto();
                login.LoginName = i.ToString();
                login.Password = i.ToString() + "password";
                list.Add(login);
            }
            var aa = list.Where(a => a.LoginName == "3").ToList();

            var a = _SqlSugarDB
                .Queryable<UserInfo>()
                .ToList();

            int count = 0;

            var page1 = _SqlSugarDB
                .Queryable<UserInfo>()
                .ToPageList(page, size, ref count);

            return Ok(JsonView(page1, count));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync(UserInfo dto)
        {
            try
            {

                var res = _SqlSugarDB.Insertable(dto).ExecuteCommand();
                return Ok(JsonView(res));
            }
            catch (Exception e)
            {

                //Log.Error("添加异常：" + e.Message);
                return Ok(JsonView("添加异常"));
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Id">编号</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EditAsync(UserInfo userInfo)
        {
            try
            {
                #region

                ////开启事务
                //string connectionString = "Data Source=ZEROTPC003\\MSSQLSERVER2022;Initial Catalog=MyFristDB; User ID=sa;Password=sa123456;";
                //SqlConnection connection = new SqlConnection(connectionString);

                //try
                //{
                //    //打开数据库连接
                //    connection.Open();

                //    //定义SQL语句
                //    string sql = "INSERT INTO [User] (UserName,PassWord) VALUES ('John','Male');";

                //    //创建SqlCommand对象
                //    SqlCommand command = new SqlCommand(sql, connection);

                //    //执行SQL语句
                //    int rowsAffected = command.ExecuteNonQuery();
                //    Console.WriteLine("Rows Affected: " + rowsAffected);
                //}
                //catch (Exception ex)
                //{
                //    //处理异常
                //    Console.WriteLine(ex.Message);
                //}
                //finally
                //{
                //    //关闭数据库连接
                //    connection.Close();
                //}
                #endregion
                var res = _SqlSugarDB.Updateable(new UserInfo() { UserName = "System", Password = "123456" }).Where(o => o.ID == userInfo.ID).ExecuteCommand();

                return Ok(JsonView(res));
            }
            catch (Exception e)
            {

                //Log.Error("修改异常：" + e.Message);
                return Ok(JsonView("修改异常"));
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id">编号</param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            try
            {
                //开启事务
                var res = _SqlSugarDB.Deleteable<UserInfo>().Where(it => it.ID == Id).ExecuteCommand();

                return Ok(JsonView(res));
            }
            catch (Exception e)
            {

                //Log.Error("删除异常：" + e.Message);
                return Ok(JsonView("删除异常"));
            }
        }

    }
}
