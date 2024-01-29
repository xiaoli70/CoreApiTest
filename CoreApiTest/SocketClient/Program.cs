// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Hello, World!");
// 创建一个 TCP/IP socket.
Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

// 连接到远程 endpoint
IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("192.168.1.102"), 11000);

try
{
    // 向服务器端发起连接请求
    clientSocket.Connect(remoteEP);

    Console.WriteLine("Socket 连接到 " + clientSocket.RemoteEndPoint.ToString());

    // 发送数据到服务器端
    byte[] msg = Encoding.ASCII.GetBytes("Hello from client");
    clientSocket.Send(msg);
    TaskAsync();
    while (true)
    {
     byte[] responseMsg = new byte[1024];
        int responseSize = clientSocket.Receive(responseMsg);
        Console.WriteLine("接收到的响应： " + Encoding.UTF8.GetString(responseMsg, 0, responseSize));
    }
    // 从服务器端接收响应
   

    // 关闭连接
    clientSocket.Shutdown(SocketShutdown.Both);
    clientSocket.Close();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}




async Task TaskAsync() {
    await Task.Run(() =>
    {
        string val = "0";
        while (val != "1")
        {
            Console.WriteLine("输入内容");
            val = Console.ReadLine();
            byte[] msg1 = Encoding.UTF8.GetBytes(val);
            clientSocket.Send(msg1);
        }
    });

}