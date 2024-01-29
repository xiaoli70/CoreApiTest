using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // 创建一个 TCP/IP socket.
        Socket clientSocket;
        // 连接到远程 endpoint
        IPEndPoint remoteEP;
        private void Form1_Load(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            remoteEP = new IPEndPoint(IPAddress.Parse("192.168.1.102"), 11000);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 向服务器端发起连接请求
            clientSocket.Connect(remoteEP);

            this.textBox1.AppendText("\r\nSocket 连接到 " + clientSocket.RemoteEndPoint.ToString());
            TaskAsync();
        }

        async Task TaskAsync()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    byte[] responseMsg = new byte[1024];
                    int responseSize = clientSocket.Receive(responseMsg);
                    this.Invoke(new Action(() =>
                    {
                        this.textBox1.AppendText("\r\n接收到的响应： " + Encoding.UTF8.GetString(responseMsg, 0, responseSize));
                    }));
                }
            });
        }


        private void button1_Click(object sender, EventArgs e)
        {
            statrsend();
            //string val = this.textBox2.Text;
            //byte[] msg1 = Encoding.UTF8.GetBytes(val);
            //clientSocket.Send(msg1);
        }
        string name,file,beys;

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileInfo EzoneFile = new FileInfo(this.openFileDialog1.FileName);
                file = EzoneFile.FullName;
                name = EzoneFile.Name;
                beys = EzoneFile.Length.ToString();
            }

        }

        public async void statrsend()
        {
            //创建一个文件对象   
            FileInfo EzoneFile = new FileInfo(file);
            //打开文件流   
            FileStream EzoneStream = EzoneFile.OpenRead();
            //包的大小   
            int PacketSize = int.Parse(beys);
            //包的数量   
            int PacketCount = (int)(EzoneStream.Length / ((long)PacketSize));
            //最后一个包的大小   
            int LastDataPacket = (int)(EzoneStream.Length - ((long)(PacketSize * PacketCount)));
            //指向远程服务端节点   
            //IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("192.168.1.102"), 11000);
            ////创建套接字   
            //Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //连接到发送端   
            //client.Connect(remoteEP);
            //获得客户端节点对象   
            IPEndPoint clientep = (IPEndPoint)clientSocket.RemoteEndPoint;
            //获得客户端的IP地址   
            //this.textBox7.Text=clientep.Address.ToString();   
            string val = this.textBox2.Text;
            byte[] msg1 = Encoding.UTF8.GetBytes(val);
            clientSocket.Send(msg1);

            //发送[文件名]到客户端   
            FileClientSocket.SendVarData(clientSocket, System.Text.Encoding.Unicode.GetBytes(EzoneFile.Name));
            //发送[包的大小]到客户端   
            FileClientSocket.SendVarData(clientSocket, System.Text.Encoding.Unicode.GetBytes(PacketSize.ToString()));
            //发送[包的总数量]到客户端   
            FileClientSocket.SendVarData(clientSocket, System.Text.Encoding.Unicode.GetBytes(PacketCount.ToString()));
            //发送[最后一个包的大小]到客户端   
            FileClientSocket.SendVarData(clientSocket, System.Text.Encoding.Unicode.GetBytes(LastDataPacket.ToString()));
            //数据包   
            byte[] data = new byte[PacketSize];
            //开始循环发送数据包   
            for (int i = 0; i < PacketCount; i++)
            {
                //从文件流读取数据并填充数据包   
                EzoneStream.Read(data, 0, data.Length);
                //发送数据包   
                FileClientSocket.SendVarData(clientSocket, data);
            }

            //如果还有多余的数据包,则应该发送完毕!   
            if (LastDataPacket != 0)
            {
                data = new byte[LastDataPacket];
                EzoneStream.Read(data, 0, data.Length);
                FileClientSocket.SendVarData(clientSocket, data);
            }
            ////关闭套接字   
            //clientSocket.Close();
            ////关闭文件流   
            //EzoneStream.Close();
            MessageBox.Show("文件传输完毕!");



        }

    }
}