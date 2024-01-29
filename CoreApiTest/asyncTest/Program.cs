// See https://aka.ms/new-console-template for more information
using asyncTest;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using System.Net;

string proxyIp = "60.28.196.211";
int proxyPort = 80;


// 创建HttpClientHandler并设置代理
var handler = new HttpClientHandler
{
Proxy = new WebProxy(proxyIp, proxyPort)
{
UseDefaultCredentials = false // 如果代理需要身份验证，请设置为true，并提供相应的用户名和密码
},
UseProxy = true
};

// 创建HttpClient并设置Handler
using (var httpClient = new HttpClient(handler))
{
    
    // 设置要访问的网站URL
    string targetUrl = "https://www.baidu.com/";

    try
    {
        // 发送GET请求
        HttpResponseMessage response = await httpClient.GetAsync(targetUrl);

        // 处理响应
        if (response.IsSuccessStatusCode)
        {
            string content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Response Content: " + content);
        }
        else
        {
            Console.WriteLine("Error: " + response.StatusCode);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Exception: " + ex.Message);
    }
}



    DataSet GetSingle(string SQLString)
{
    string connectionString = "server=155.126.191.138;database=PG_DDS_new2;User Id=sa;pwd=Sa123456";

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        DataSet ds = new DataSet();
        try
        {
            connection.Open();
            SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
            command.Fill(ds, "ds");
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
}





List<string> list = new List<string> { "A", "B", "C", "D" };

// 输出原始List  
Console.WriteLine("原始List:");
foreach (var item in list)
{
    Console.WriteLine(item);
}

// 使用Reverse方法将List倒序排列  
list.Reverse();

// 输出倒序排列后的List  
Console.WriteLine("\n倒序排列后的List:");
foreach (var item in list)
{
    Console.WriteLine(item);
}

new StartExe().StartExe1("E:\\软件\\DongRuan\\Codematic.exe");

string ss = "32w";
int bb = Convert.ToInt32(ss);

//Console.WriteLine("Hello, World!");
string a = "";
string b="";
var user = new User();
await weituo(x=> {
    x.Name = "likun";
    b = x.Password;
    user = x;
});
Console.WriteLine($"输出a：{a}");
Console.ReadLine();



async Task asyo() {

    await Task.Run(() =>
    {
        for (int i = 0; i < 1000; i++)
        {
            Console.WriteLine("2");
        }

    });
}

async Task weituo(Action<User> action)
{

    await Task.Run(() =>
    {
        string ac = "";
        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine(i);
            ac += i.ToString()+"-";
        }
        action.Invoke(new User() {Name="li",Password="123" });
    });
}


async Task<int> dadanys() {
    int inde=await Task.Run(() => {
        Thread.Sleep(5000);
        return 12;
    });
    Console.WriteLine("耗时");
    return inde;
   
}



async Task dadanys1(int a)
{
    Thread.Sleep(1000);
    Console.WriteLine("耗时2--"+a);
}

public interface ITest{

    int MinNum();

}
class User
{
    public string? Name {  get; set; }

    public string? Password { get; set; }
}
public class MyClass : ITest
{
    
    public int MinNum()
    {
        return 1+1;
    }
}

public class BBTY {
    public readonly ITest itesk;
   
    //public BBTY(ITest itesk)
    //{
    //    this.itesk = itesk;
       
    //}
    public void gitInt() {
         Console.WriteLine($"输出结果：{itesk.MinNum()}");
        
    }
}

