using MimeKit.Text;
using MimeKit;
using Net6Api.Domain.Helper;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Net6Api.Domain;
using log4net;

namespace Net6Api.Controllers
{
    /// <summary>
    /// 定时任务
    /// </summary>
    public class AutoServices /*: IHostedService, IDisposable*/
    {
        private Timer _timer;
        private EmailInfoConst _emailInfo;
        private readonly ILogger<AutoServices> _logger;
        static readonly HttpClient client = new HttpClient();
        public AutoServices( EmailInfoConst emailInfo, ILogger<AutoServices> logger)
        {
            _emailInfo = emailInfo;
            _logger = logger;
        }
        private async void DoWork(object state)
        {
           
            

            // 读取响应内容并输出到控制台  
            //自定义字段
            log4net.GlobalContext.Properties["UserName"] = "YourOperatorName";
            try
            {
                var url = "http://192.168.1.102:8083/Auth/ActionTest";
                var headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" }
            };
                var content = new StringContent("", Encoding.UTF8, "application/json");

                // 发送POST请求并获取响应  
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode(); // 确保请求成功  
                string responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Post请求{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}_____{responseBody}");
            }
            catch (Exception)
            {
                _logger.LogInformation($"报错");
                
            }
            
            bool fal=GetBook.Getbook();
            if (!fal) {
                TextPart body = new TextPart(TextFormat.Html)
                {
                    Text = "<h1>Tips</h1> "
                };
                body.Text += $"<p>  Update Complete ---{fal} </p>";
                body.Text += $"<p> 请勿回复     </p>";
                string sendResult = EmailHelper.SendEmail(_emailInfo, ".NET6", "li", "1006606849@qq.com", body);
            }
            

            _logger.LogInformation($"定时任务执行______{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}_{fal}");
            await Console.Out.WriteLineAsync($"定时任务执行______{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}_{fal}");
        }

        public async Task  StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("MyBackgroundService正在启动");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("StopAsync");
           return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

    public static class GetBook
    {
        static string searchurl = "http://www.biquge5200.cc//8_8187/191079442.html";// http://www.biquge5200.cc//8_8187/191064114.html
        public static bool Getbook()
        {

            restart: try
            {


                string html = GetHtml(searchurl);
                html = html.Replace("<div id='gc1' class='gcontent1'><script type='text/javascript'>try{ggauto();} catch(ex){}</script></div>", "");
                if (html.Contains("内容正在获取中"))
                    return false;
                return true;
                
            }
            catch (WebException we)
            {

                Console.WriteLine("远程主机强迫关闭了一个现有的连接,重新爬取当前章节。。。");
                goto restart;

            }
        }
        public static string GetHtml(string url = "")
        {
            string htmlCode;
            HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            webRequest.Timeout = 30000;
            webRequest.Method = "GET";
            webRequest.UserAgent = "Mozilla/4.0";
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();
            string contentype = webResponse.Headers["Content-Type"];
            Regex regex = new Regex("charset\\s*=\\s*[\\W]?\\s*([\\w-]+)", RegexOptions.IgnoreCase);
            if (webResponse.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
            {
                using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                {
                    using (var zipStream = new System.IO.Compression.GZipStream(streamReceive, System.IO.Compression.CompressionMode.Decompress))
                    {
                        if (regex.IsMatch(contentype))
                        {
                            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                            Encoding encoding = System.Text.Encoding.GetEncoding("GB2312");

                            //Encoding ending = Encoding.GetEncoding(regex.Match(contentype).Groups[1].Value.Trim());
                            using (StreamReader sr = new System.IO.StreamReader(zipStream, encoding))
                            {
                                htmlCode = sr.ReadToEnd();
                            }
                        }
                        else
                        {
                            using (StreamReader sr = new System.IO.StreamReader(zipStream, Encoding.UTF8))
                            {
                                htmlCode = sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
            else
            {
                using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(streamReceive, Encoding.Default))
                    {
                        htmlCode = sr.ReadToEnd();
                    }
                }
            }
            return htmlCode;
        }

    }


}
