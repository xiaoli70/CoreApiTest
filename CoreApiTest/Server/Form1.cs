using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        IPEndPoint? serverIP = null;
        Socket? clientSocket = null;
        byte[]? buffer = null;
        private async void Form1_Load(object sender, EventArgs e)
        {

        }

        async Task asycncFask(Socket clientSocket)
        {

            Create(clientSocket);
            //await Task.Run(() =>
            //{   
            //while (true)
            //    {

            //        // 接收来自客户端的请求
            //        //int size = clientSocket.Receive(buffer);
            //        //this.Invoke(new Action(() => {  
            //        //    this.textBox1.AppendText("\r\n接收到客户端请求： " + Encoding.UTF8.GetString(buffer, 0, size));
            //        //}));

            //    }
            //});
        }
        public async void Create(Socket socket)
        {
            await Task.Run(() =>
            {
                Socket client = socket;
                IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;
                this.Invoke(new Action(() =>
                    {
                        this.textBox1.AppendText($"\r\n{DateTime.Now} 请求对象：[{clientep.Address}:{clientep.Port}]");
                    }));
                while (true)
                {
                    int size = clientSocket.Receive(buffer);
                    this.Invoke(new Action(() =>
                    {
                        this.textBox1.AppendText("\r\n接收到客户端请求： " + Encoding.UTF8.GetString(buffer, 0, size));
                    }));
                    //获得[文件名]   
                    string SendFileName = System.Text.Encoding.Unicode.GetString(FileSocketServer.ReceiveVarData(client));
                    this.Invoke(new Action(() =>
                    {
                        this.textBox1.AppendText($"\r\n{DateTime.Now} 文件名称：[{SendFileName}]");
                    }));
                    //获得[包的大小]   
                    string bagSize = System.Text.Encoding.Unicode.GetString(FileSocketServer.ReceiveVarData(client));

                    //获得[包的总数量]   
                    int bagCount = int.Parse(System.Text.Encoding.Unicode.GetString(FileSocketServer.ReceiveVarData(client)));
                    //获得[最后一个包的大小]   
                    string bagLast = System.Text.Encoding.Unicode.GetString(FileSocketServer.ReceiveVarData(client));

                    //自动创建文件夹
                    string path = $@"{System.AppDomain.CurrentDomain.BaseDirectory}\Log\{clientep.Address}({DateTime.Now.ToString("yyyyMMddhhmmssffffff")})";
                    filename = path;
                    Directory.CreateDirectory(path);
                    //创建一个新文件   
                    FileStream MyFileStream = new FileStream($@"{path}\" + SendFileName, FileMode.Create, FileAccess.Write);
                    this.Invoke(new Action(() =>
                    {
                        this.textBox1.AppendText($"\r\n{DateTime.Now} 创建目标：[已创建]");

                    }));
                    //已发送包的个数   
                    int SendedCount = 0;
                    byte[] data = FileSocketServer.ReceiveVarData(client);
                    {
                        //while (true)
                        //{
                        //    if (data.Length == 0)
                        //    {
                        //        break;
                        //    }
                        //    else
                        //    {
                        //        SendedCount++;
                        //        //将接收到的数据包写入到文件流对象   

                        //        //显示已发送包的个数
                        //    }
                        //}
                    }
                    MyFileStream.Write(data, 0, data.Length);
                    ////关闭文件流   
                    MyFileStream.Close();
                    ////关闭套接字   
                    //client.Close();
                    this.Invoke(new Action(() =>
                    {
                        string s = $@"{path}\" + SendFileName;
                        try
                        {
                            this.pictureBox1.Image = Image.FromFile(s);
                            //this.WindowsMediaPlayer1.URL = s;
                        }
                        catch (Exception)
                        {

                        }
                        this.textBox1.AppendText($"\r\n{DateTime.Now} 传输结果：[成功]");
                    }));
                }
            });

        }
        async Task asycncFasktwo(Socket serverSocket)
        {
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            Socket a = await Task.Run(async () =>
            {
                try
                {
                    clientSocket = await serverSocket.AcceptAsync();
                    this.Invoke(new Action(() =>
                    {
                        this.textBox1.AppendText("\r\n客户端连接成功：" + clientSocket.RemoteEndPoint.ToString());
                    }));
                    return clientSocket;
                }
                catch (Exception)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.textBox1.AppendText("停止接收请求\r\n");
                    }));
                    return null; ;
                    //throw;
                }
            });
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            if (a != null)
                asycncFask(a);

        }
        Socket serverSocket = null;
        private void button1_Click(object sender, EventArgs e)
        {
            string aaa = this.textBox2.Text;
            // 向客户端发送响应数据
            byte[] responseMsg = Encoding.UTF8.GetBytes(aaa);
            clientSocket.Send(responseMsg);
        }
        string filename;
        //启动服务
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.button2.Text == "Start")
            {
                buffer = new byte[1024];

                // 服务器监听IP地址及端口
                serverIP = new IPEndPoint(IPAddress.Parse("192.168.1.102"), 11000);

                // 创建TCP/IP socket
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    this.textBox1.AppendText("启动服务...\r\n");
                    // 绑定到本地端点并启动监听
                    serverSocket.Bind(serverIP);
                    serverSocket.Listen(10);
                    this.textBox1.AppendText("等待客户端连接...\r\n");
                    // 接受客户端请求
                    asycncFasktwo(serverSocket);
                    this.button2.Text = "Stop";

                }
                catch (Exception ex)
                {
                    this.textBox1.AppendText(ex.ToString());
                }
            }
            else
            {
                if (clientSocket != null)
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
                serverSocket.Close();

                this.textBox1.AppendText("关闭服务...\r\n");
                this.button2.Text = "Start";
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileInfo EzoneFile = new FileInfo(this.openFileDialog1.FileName);
                string name = EzoneFile.FullName;
                string file = EzoneFile.Name;
                string fileghit = EzoneFile.Length.ToString();
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start(filename);
            try
            {
                var psi = new System.Diagnostics.ProcessStartInfo() { FileName = filename, UseShellExecute = true };
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception)
            {


            }
        }
    }
}