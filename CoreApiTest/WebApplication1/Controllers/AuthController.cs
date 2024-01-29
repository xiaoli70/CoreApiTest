using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Net6Api.Domain;
using Net6Api.Domain.Helper;
using Net6Api.Filter;
using NSwag.Annotations;
using SqlSugar;
using System.Collections;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Net6Api.Controllers
{
    /// <summary>
    /// 鉴权相关
    /// </summary>
    [Route("Auth/[Action]")]
    [OpenApiTag("主页")]

    public class AuthController : BaseControllers
    {
        readonly IConfiguration _config;
        private readonly ISqlSugarClient _SqlSugarDB;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IConfiguration config,ISqlSugarClient sqlSugarClient, ILoggerFactory loggerFactory,ILogger<AuthController> logger)
        {

            _config = config;
            _SqlSugarDB = sqlSugarClient;
            _loggerFactory = loggerFactory;
           // _logger= logger;
            _logger=this._loggerFactory.CreateLogger<AuthController>();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginView), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            try
            {
                // 查询用户信息
                var user = await _SqlSugarDB.Queryable<UserInfo>()
                    .Where(a => a.UserName == dto.LoginName && a.Password == dto.Password)
                    .FirstAsync();

                // 校验用户信息
                if (user != null)
                {
                    var view = new LoginView
                    {
                        Expires = DateTime.Now.AddMinutes(10)
                    };

                    // 生成 JWT Token
                    var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Name, dto.LoginName),
                    new Claim(ClaimTypes.Role, "Admin") // 请确保角色名称正确
                };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        issuer: "net6api",
                        audience: "net6api",
                        claims: claims,
                        expires: view.Expires,
                        signingCredentials: creds);

                    view.Name = dto.LoginName;
                    view.Token = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(JsonView(view));
                }

                return Ok(JsonView("用户名密码错误"));
            }
            catch (Exception ex)
            {
                // 记录异常信息，可以根据具体情况选择日志记录方式
                Console.WriteLine($"An error occurred during login: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }



        #region

        ///// <summary>
        ///// 用户登录
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[AllowAnonymous]
        //[HttpPost("login")]
        //[ProducesResponseType(typeof(LoginView), StatusCodes.Status200OK)]
        //public async Task<IActionResult> LoginAsync(LoginDto dto)
        //{
        //    //_loggerFactory.CreateLogger<AuthController>().LogInformation("测试中、。。。。。。");
        //    //_logger.LogInformation("111");
        //    #region 校验用户信息，假设此处我们已经校验成功
        //    var user=await _SqlSugarDB.Queryable<UserInfo>().Where(a => a.UserName == dto.LoginName&&a.Password==dto.Password).FirstAsync();
        //    #endregion
        //    if (user != null) { 
        //    var view = new LoginView
        //    {
        //        Expires = DateTime.Now.AddMinutes(10)
        //    };
        //        var claims = new[] { 
        //            new Claim(ClaimTypes.NameIdentifier, ""),
        //            new Claim(ClaimTypes.Name,dto.LoginName),
        //            new Claim(ClaimTypes.Role,"Amin") 
        //        };
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    var token = new JwtSecurityToken(
        //        issuer: "net6api",
        //        audience: "net6api",
        //        claims: claims,
        //        expires: view.Expires,
        //        signingCredentials: creds);
        //        view.Name = dto.LoginName;
        //    view.Token = new JwtSecurityTokenHandler().WriteToken(token);
        //        return Ok(JsonView(view));
        //    }
        //    return Ok(JsonView("用户名密码错误"));
        //}
        #endregion
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ActionTestAsync() {
            List<AddressDto> list= new List<AddressDto>();
            for (int i = 0; i < 5; i++)
            {
                AddressDto dto = new(new Random().Next(), new Random().Next());
                list.Add(dto);
            }
            var spring = Seasons.Spring;
            var startingOnEquinox = Seasons.Spring | Seasons.Autumn;
            var theYear = Seasons.All;

            Thread.Sleep(50000);

            return Ok(JsonView(list));
        }
        /// <summary>
        /// huoqustring
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public String GetString() => "0";
        /// <summary>
        /// 下载文档
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [CustomExceptionFilter]
        public async Task<int> RetrieveDocsHomePage()
        {
            
            var client = new HttpClient();
            byte[] content = await client.GetByteArrayAsync("https://learn.microsoft.com/");
            string str = System.Text.Encoding.Default.GetString(content);
            GlobalContext.Properties["UserName"] = UseStaticHttpContext.UserName;
            _logger.LogInformation("执行方法");
            //Console.WriteLine($"{nameof(RetrieveDocsHomePage)}: Finished downloading.");
            return content.Length;
        }


    }
}
