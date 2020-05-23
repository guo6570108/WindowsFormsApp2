using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //创建一个监听的Socket
            Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //获得IP地址的端口号 
            IPAddress ip = IPAddress.Parse(txtServer.Text);
            //获得端口号
            IPEndPoint point = new IPEndPoint(ip, int.Parse(txtPort.Text));
            //绑定 开始监听   
            socketWatch.Bind(point);
            //设置监听队列
            socketWatch.Listen(10);

            //显示消息 
            ShowMsg("正在等待客户端进入游戏！！！！！！");

            Thread th = new Thread(Listen);
            th.IsBackground = true;
            th.Start(socketWatch);

        }
        Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();//存储客户端的IP地址和服务器之间与之通信的Socket

        Dictionary<string, int> dicScore = new Dictionary<string, int>();//存储客户端的IP地址和成绩

        /// <summary>
        /// 接受客户端连接 
        /// </summary>
        /// <param name="o"></param>
        void  Listen(object o )
        {
            Socket socketWatch  = o as Socket;
            while (true)
            {
                Socket socketSend = socketWatch.Accept();
                //将远程客户端的IP地址以及跟客户通讯的Socket存储到集合中
                dicSocket.Add(socketSend.RemoteEndPoint.ToString(), socketSend);
                //RemoteEndPoint 获得客户端的ip地址和端口号
                ShowMsg(socketSend.RemoteEndPoint .ToString()+"已经进入游戏！！！！");

                //接受客户端发送过来的消息 
                Thread th2 = new Thread(rec);
                th2.IsBackground = true;
                th2.Start(socketSend);
            }
        }
        //不停地接受客户端发送过来的消息 
        void  rec(object o)
        {
            Socket socketSend = o as Socket;
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024 * 1024 * 3];

                    int r = socketSend.Receive(buffer);
                    //接受玩家分数
                    string strScore = Encoding.Default.GetString(buffer, 0, r);
                    //类型转换
                    int score = Convert.ToInt32(strScore);
                    //把数据添加到dicSocer集合中
                    dicScore.Add(socketSend.RemoteEndPoint.ToString(), score);
                    //对dicsore进行排序
                    Compare();
                }
            }
            catch 
            {

                
            }
         
        }
        void Compare()
        {
            //对dicScore进行一个降序排序 
            List<KeyValuePair<string,int >> list = 
            dicScore.OrderByDescending(n => n.Value).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                string result = "您是本次打飞机大赛第" + (i + 1) + "名，您的总成绩是" + list[i].Value;
                byte[] buffer = Encoding.Default.GetBytes(result);
                List<byte> listByte = new List<byte>();
                listByte.Add(2);
                listByte.AddRange(buffer);
                byte[] newbuffer = listByte.ToArray();
                //发送 
                dicSocket[list[i].Key].Send(newbuffer );
            }
        }

        void ShowMsg(string str)
        {
            txtLog.AppendText(str +  "\r\n");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            byte[] buffer = new byte[1];
            buffer[0] = 1;
            //服务器应用程序给客户端发送开始游戏的消息
            foreach (KeyValuePair<string,Socket > kv in  dicSocket)
            {
                kv.Value.Send(buffer);
            }
        }
    }
}
