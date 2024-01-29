// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("开始下载文件...");

// 异步执行下载操作，允许同时下载多个文件
Task downloadTask1 = DownloadFileAsync("https://example.com/file1.txt", "File1.txt");
Task downloadTask2 = DownloadFileAsync("https://example.com/file2.txt", "File2.txt");

// 在等待下载的同时，执行其他操作
Console.WriteLine("执行其他操作...");

// 等待下载任务完成
await Task.WhenAll(downloadTask1, downloadTask2);

Console.WriteLine("所有文件下载完成！");


static async Task DownloadFileAsync(string url, string fileName)
{
    using (HttpClient client = new HttpClient())
    {
        // 模拟异步下载文件
        byte[] fileData = await client.GetByteArrayAsync(url);

        // 模拟将文件保存到磁盘
        Console.WriteLine($"下载完成：{fileName}");
    }
}

#region
//byte[] buffer = new byte[1024];

//// 服务器监听IP地址及端口
//IPEndPoint serverIP = new IPEndPoint(IPAddress.Parse("192.168.1.102"), 11000);

//// 创建TCP/IP socket
//Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

//try
//{
//    // 绑定到本地端点并启动监听
//    serverSocket.Bind(serverIP);
//    serverSocket.Listen(10);

//    Console.WriteLine("等待客户端连接...");

//    // 接受客户端请求
//    Socket clientSocket = serverSocket.Accept();

//    Console.WriteLine("客户端连接成功：" + clientSocket.RemoteEndPoint.ToString());
//    asycncFask(clientSocket);

//    Console.WriteLine("执行");


//    while (true) {
//       string aaa= Console.ReadLine();
//        // 向客户端发送响应数据
//        byte[] responseMsg = Encoding.UTF8.GetBytes(aaa);
//        clientSocket.Send(responseMsg);

//    }


//    // 关闭连接
//    clientSocket.Shutdown(SocketShutdown.Both);
//    clientSocket.Close();
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.ToString());
//}


//async Task asycncFask(Socket clientSocket) {
//    await Task.Run(() =>
//    {
//        while (true)
//        { // 接收来自客户端的请求
//            int size = clientSocket.Receive(buffer);
//            Console.WriteLine("接收到客户端请求： " + Encoding.UTF8.GetString(buffer, 0, size));
//        }
//    });


//}
#endregion