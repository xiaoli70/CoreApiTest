using CoreApiTest.IClass;
using CoreApiTest.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoreApiTest.Controllers
{

    [Route("[controller]/[action]")]
    public class WeatherForecastController : BaseControllers
    {


        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUser _user;
        private readonly JwtHelper _jwtHelper;

        public WeatherForecastController(IUser user, JwtHelper jwtHelper)
        {
            //_logger = logger;
            _user = user;
            _jwtHelper = jwtHelper;
        }




        [HttpGet(Name = "GetWeatherForecastS")]
        public string GetS(string s)
        {
            string aa = _user.GetUser(s);
            return aa;
        }
        /// <summary>
        /// ��ȡToken
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<string> GetToken()
        {
            Log.Error("-----------��ȡToken");
            return _jwtHelper.CreateToken();
        }

       /// <summary>
       /// ���Է���
       /// </summary>
       /// <param name="tests">����</param>
       /// <returns></returns>
        [HttpPost]
        public ActionResult<string> GetTest(string tests)
        {
            return "Test Authorize"+ tests;
        }

    }
}