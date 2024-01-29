using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Net6Api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class VisitorInfoController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetVisitorInfo()
        {
            // 获取访问者的IP地址
            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            Console.WriteLine(ipAddress);
            // 获取访问者的地理位置信息
            string geoLocation = GetGeoLocation(ipAddress);

            // 构造返回结果
            var result = new
            {
                IpAddress = ipAddress,
                GeoLocation = geoLocation
            };

            return Ok(result);
        }

        private string GetGeoLocation(string ipAddress)
        {
            // 这里可以使用第三方的IP地理位置查询服务，也可以使用自己的数据库或其他方式获取地理位置信息
            // 下面的代码只是一个示例，实际应用中需要根据具体情况进行修改

            // 这里使用了一个在线的IP地理位置查询服务（仅供演示，请根据服务条款合法使用）
            string apiUrl = $"https://ipapi.co/{ipAddress}/json/";
            string geoLocation = string.Empty;

            using (var httpClient = new HttpClient())
            {
                try
                {
                    string response = httpClient.GetStringAsync(apiUrl).Result;
                    geoLocation = response;
                }
                catch (Exception ex)
                {
                    // 处理异常
                    geoLocation = $"Error: {ex.Message}";
                }
            }

            return geoLocation;
        }
    }

}
