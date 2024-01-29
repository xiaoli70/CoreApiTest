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

            //        // �������Կͻ��˵�����
            //        //int size = clientSocket.Receive(buffer);
            //        //this.Invoke(new Action(() => {  
            //        //    this.textBox1.AppendText("\r\n���յ��ͻ������� " + Encoding.UTF8.GetString(buffer, 0, size));
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
                        this.textBox1.AppendText($"\r\n{DateTime.Now} �������[{clientep.Address}:{clientep.Port}]");
                    }));
                while (true)
                {
                    int size = clientSocket.Receive(buffer);
                    this.Invoke(new Action(() =>
                    {
                        this.textBox1.AppendText("\r\n���յ��ͻ������� " + Encoding.UTF8.GetString(buffer, 0, size));
                    }));
                    //���[�ļ���]   
                    string SendFileName = System.Text.Encoding.Unicode.GetString(FileSocketServer.ReceiveVarData(client));
                    this.Invoke(new Action(() =>
                    {
                        this.textBox1.AppendText($"\r\n{DateTime.Now} �ļ����ƣ�[{SendFileName}]");
                    }));
                    //���[���Ĵ�С]   
                    string bagSize = System.Text.Encoding.Unicode.GetString(FileSocketServer.ReceiveVarData(client));

                    //���[����������]   
                    int bagCount = int.Parse(System.Text.Encoding.Unicode.GetString(FileSocketServer.ReceiveVarData(client)));
                    //���[���һ�����Ĵ�С]   
                    string bagLast = System.Text.Encoding.Unicode.GetString(FileSocketServer.ReceiveVarData(client));

                    //�Զ������ļ���
                    string path = $@"{System.AppDomain.CurrentDomain.BaseDirectory}\Log\{clientep.Address}({DateTime.Now.ToString("yyyyMMddhhmmssffffff")})";
                    filename = path;
                    Directory.CreateDirectory(path);
                    //����һ�����ļ�   
                    FileStream MyFileStream = new FileStream($@"{path}\" + SendFileName, FileMode.Create, FileAccess.Write);
                    this.Invoke(new Action(() =>
                    {
                        this.textBox1.AppendText($"\r\n{DateTime.Now} ����Ŀ�꣺[�Ѵ���]");

                    }));
                    //�ѷ��Ͱ��ĸ���   
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
                        //        //�����յ������ݰ�д�뵽�ļ�������   

                        //        //��ʾ�ѷ��Ͱ��ĸ���
                        //    }
                        //}
                    }
                    MyFileStream.Write(data, 0, data.Length);
                    ////�ر��ļ���   
                    MyFileStream.Close();
                    ////�ر��׽���   
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
                        this.textBox1.AppendText($"\r\n{DateTime.Now} ��������[�ɹ�]");
                    }));
                }
            });

        }
        async Task asycncFasktwo(Socket serverSocket)
        {
#pragma warning disable CS8600 // �� null �����������Ϊ null ��ֵת��Ϊ�� null ���͡�
            Socket a = await Task.Run(async () =>
            {
                try
                {
                    clientSocket = await serverSocket.AcceptAsync();
                    this.Invoke(new Action(() =>
                    {
                        this.textBox1.AppendText("\r\n�ͻ������ӳɹ���" + clientSocket.RemoteEndPoint.ToString());
                    }));
                    return clientSocket;
                }
                catch (Exception)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.textBox1.AppendText("ֹͣ��������\r\n");
                    }));
                    return null; ;
                    //throw;
                }
            });
#pragma warning restore CS8600 // �� null �����������Ϊ null ��ֵת��Ϊ�� null ���͡�
            if (a != null)
                asycncFask(a);

        }
        Socket serverSocket = null;
        private void button1_Click(object sender, EventArgs e)
        {
            string aaa = this.textBox2.Text;
            // ��ͻ��˷�����Ӧ����
            byte[] responseMsg = Encoding.UTF8.GetBytes(aaa);
            clientSocket.Send(responseMsg);
        }
        string filename;
        //��������
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.button2.Text == "Start")
            {
                buffer = new byte[1024];

                // ����������IP��ַ���˿�
                serverIP = new IPEndPoint(IPAddress.Parse("192.168.1.102"), 11000);

                // ����TCP/IP socket
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    this.textBox1.AppendText("��������...\r\n");
                    // �󶨵����ض˵㲢��������
                    serverSocket.Bind(serverIP);
                    serverSocket.Listen(10);
                    this.textBox1.AppendText("�ȴ��ͻ�������...\r\n");
                    // ���ܿͻ�������
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

                this.textBox1.AppendText("�رշ���...\r\n");
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