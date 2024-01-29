using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using Net6Api.Domain;
using Net6Api.Domain.Helper;

namespace Net6Api.Controllers
{
    /// <summary>
    /// 测试方法 
    /// </summary>
    [Route("SendMail")]
    public class SendMailController : BaseControllers
    {
        private EmailInfoConst _emailInfo;

        public SendMailController(EmailInfoConst emailInfo)
        {
            _emailInfo = emailInfo;
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <returns></returns>
        [HttpPost("Str")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Send(string Str)
        {
            try
            
            {
                TextPart body = new TextPart(TextFormat.Html)
                {
                    Text = "<h1>测试邮件</h1> "
                };
                body.Text += $"<p> "+Str+"    </p>";
                body.Text += $"<p> 请勿回复     </p>";
                string sendResult = EmailHelper.SendEmail(_emailInfo, "邮箱测试", "li", "1006606849@qq.com", body);

                return Ok(JsonView(true,"发送成功"));
            }
            catch (Exception e)
            {

                //Log.Error("添加异常：" + e.Message);
                return Ok(JsonView("发送失败"));
            }
        }
    }
}