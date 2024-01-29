using Microsoft.AspNetCore.Mvc;


namespace Net6Api.Controllers
{
    /// <summary>
    /// 图片
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        /// <summary>
        /// 图片转base64
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        [HttpGet]
        public string ConvertoBase(string imagePath)
        {
            using (var fileStream = new FileStream(imagePath, FileMode.Open))
            {
                var buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, buffer.Length);
                return Convert.ToBase64String(buffer);
            }
        }
    }
}
