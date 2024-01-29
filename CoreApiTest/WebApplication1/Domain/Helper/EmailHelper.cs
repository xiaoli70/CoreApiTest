using MailKit.Net.Smtp;
using MimeKit;

namespace Net6Api.Domain.Helper
{
    /// <summary>
    /// 发送邮箱
    /// </summary>
    [Serializable]
    public static class EmailHelper
    {
        public static string SendEmail(EmailInfoConst emailInfo, string title, string receiveNmae, string receiveEmail, TextPart body)
        {
            try
            {
                MimeMessage message = new MimeMessage();
                //发件人
                message.From.Add(new MailboxAddress("TEST", emailInfo.Username));
                //收件人
                message.To.Add(new MailboxAddress(receiveNmae, receiveEmail));
                //标题
                message.Subject = title;
                //生成一个支持Html的TextPart
                //TextPart body = new TextPart(TextFormat.Html)
                //{
                //    Text = "<h1>测试内容</h1> "
                //};

                //创建Multipart添加附件
                Multipart multipart = new Multipart("mixed");
                multipart.Add(body);

                //正文
                message.Body = multipart;

                using (SmtpClient client = new SmtpClient())
                {
                    //Smtp服务器
                    client.Connect(emailInfo.SmtpServer, emailInfo.Port, true);
                    if (client.IsConnected)
                    {
                        //登录
                        client.Authenticate(emailInfo.Username, emailInfo.Password);
                        //发送
                        string result = client.Send(message);
                    }

                    //断开
                    client.Disconnect(true);
                    return "发送邮件成功";
                }
            }
            catch (Exception ex)
            {
                return "发送失败";
            }
        }
    }
}
