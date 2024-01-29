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
        // ����һ�� TCP/IP socket.
        Socket clientSocket;
        // ���ӵ�Զ�� endpoint
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
            // ��������˷�����������
            clientSocket.Connect(remoteEP);

            this.textBox1.AppendText("\r\nSocket ���ӵ� " + clientSocket.RemoteEndPoint.ToString());
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
                        this.textBox1.AppendText("\r\n���յ�����Ӧ�� " + Encoding.UTF8.GetString(responseMsg, 0, responseSize));
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
            //����һ���ļ�����   
            FileInfo EzoneFile = new FileInfo(file);
            //���ļ���   
            FileStream EzoneStream = EzoneFile.OpenRead();
            //���Ĵ�С   
            int PacketSize = int.Parse(beys);
            //��������   
            int PacketCount = (int)(EzoneStream.Length / ((long)PacketSize));
            //���һ�����Ĵ�С   
            int LastDataPacket = (int)(EzoneStream.Length - ((long)(PacketSize * PacketCount)));
            //ָ��Զ�̷���˽ڵ�   
            //IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("192.168.1.102"), 11000);
            ////�����׽���   
            //Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //���ӵ����Ͷ�   
            //client.Connect(remoteEP);
            //��ÿͻ��˽ڵ����   
            IPEndPoint clientep = (IPEndPoint)clientSocket.RemoteEndPoint;
            //��ÿͻ��˵�IP��ַ   
            //this.textBox7.Text=clientep.Address.ToString();   
            string val = this.textBox2.Text;
            byte[] msg1 = Encoding.UTF8.GetBytes(val);
            clientSocket.Send(msg1);

            //����[�ļ���]���ͻ���   
            FileClientSocket.SendVarData(clientSocket, System.Text.Encoding.Unicode.GetBytes(EzoneFile.Name));
            //����[���Ĵ�С]���ͻ���   
            FileClientSocket.SendVarData(clientSocket, System.Text.Encoding.Unicode.GetBytes(PacketSize.ToString()));
            //����[����������]���ͻ���   
            FileClientSocket.SendVarData(clientSocket, System.Text.Encoding.Unicode.GetBytes(PacketCount.ToString()));
            //����[���һ�����Ĵ�С]���ͻ���   
            FileClientSocket.SendVarData(clientSocket, System.Text.Encoding.Unicode.GetBytes(LastDataPacket.ToString()));
            //���ݰ�   
            byte[] data = new byte[PacketSize];
            //��ʼѭ���������ݰ�   
            for (int i = 0; i < PacketCount; i++)
            {
                //���ļ�����ȡ���ݲ�������ݰ�   
                EzoneStream.Read(data, 0, data.Length);
                //�������ݰ�   
                FileClientSocket.SendVarData(clientSocket, data);
            }

            //������ж�������ݰ�,��Ӧ�÷������!   
            if (LastDataPacket != 0)
            {
                data = new byte[LastDataPacket];
                EzoneStream.Read(data, 0, data.Length);
                FileClientSocket.SendVarData(clientSocket, data);
            }
            ////�ر��׽���   
            //clientSocket.Close();
            ////�ر��ļ���   
            //EzoneStream.Close();
            MessageBox.Show("�ļ��������!");



        }

    }
}