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

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitialGame();
        }
        /// <summary>
        /// 初始化游戏
        /// </summary>
        Random r = new Random();
        public void InitialGame()
        {
            //初始化游戏背景
            SingleObject.GetSingle().AddGameObject(new Background(0,-850,20));
            //初始化游戏玩家
            SingleObject.GetSingle().AddGameObject(new HeroPlane(200, 200, 20, 1, Direction.Up));
            //初始化敌人飞机
            InitialEnemyPlane();
        }
        /// <summary>
        /// 初始化敌人飞机
        /// </summary>
        private void InitialEnemyPlane()
        {
            for (int i = 0; i < 4; i++)
            {
                SingleObject.GetSingle().AddGameObject(new EnemyPlane(r.Next(0, this.Width), -100, r.Next(0, 2)));
            }
            //给百分之10的机率
            if (r.Next(0,101)>90)
            {
                SingleObject.GetSingle().AddGameObject(new EnemyPlane(r.Next(0, this.Width), -100, 2));
            }
        }
        bool IsStar = false;//用来标记游戏是否已经开始

        int PlayTime = 0;//记录游戏时间
        string result = string.Empty;//记录客户端的成绩结果

        bool IsPaintResult = false;//是否绘制结果字符串

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            SingleObject.GetSingle().BG.Draw(e.Graphics);

            if (IsStar)
            {
                SingleObject.GetSingle().DrawGameObject(e.Graphics);
            }
            //绘制各种游戏对象 
            //背景的 玩家飞机的 敌人飞机 子弹的
            
            string s = SingleObject.GetSingle().Score.ToString();
            //绘制游戏的分数 
            e.Graphics.DrawString(s, new Font("微软雅黑", 20, FontStyle.Bold), Brushes.Red, new Point(0, 0));
            if (IsPaintResult)
            {
                e.Graphics.DrawString(result, new Font("宋体", 20, FontStyle.Bold), Brushes.Red, new Point(0, 200));
            }
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
            //不停地判断当前飞机的数量 
            if (SingleObject.GetSingle().listEnemyPlane.Count<=1)
            {
                InitialEnemyPlane();
            }
            SingleObject.GetSingle().PZJC();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //将鼠标的坐标赋值给玩家坐标 
            SingleObject.GetSingle().HP.MoveWithMouse(e);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            SingleObject.GetSingle().HP.MouseDownLeft(e);
        }
        Socket socket;
        private void txtServer_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 客户端进行游戏 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
             socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(txtServer.Text);
            IPEndPoint point = new IPEndPoint(ip, int.Parse(txtPort.Text));
            socket.Connect(point);
            //不停地接受服务器发送过来的消息 
            Thread th = new Thread(Rec);
            th.IsBackground = true;
            th.Start();
        }
        /// <summary>
        /// 接受客户端发送过来的消息 
        /// </summary>
        void Rec()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024 * 1024 * 5];
                    //将接收到的数据放到buffer中
                   int r= socket.Receive(buffer);
                    if (buffer[0] == 1)
                    {
                        //发送的是开始游戏的消息
                        IsStar = true;
                    }
                    else if (buffer[0]==2)
                    {
                        //获得服务器传输过来的结果
                        IsStar = false;

                        result = Encoding.Default.GetString(buffer, 1,r-1);
                        //把结果绘制到paint事件中
                        //绘制结果
                        IsPaintResult = true;
                    }
                }
            }
            catch 
            {

               
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (IsStar)
            {
                PlayTime++;//如果开始游戏，则开始计时 
                if (PlayTime==20)
                {
                    IsStar = false;//到达时间则停止游戏
                    //将结果发送给服务器
                    //把分数转换成字节数组发送给客户端 
               byte[] buffer =  Encoding.Default.GetBytes(   SingleObject.GetSingle().Score.ToString());
                    socket.Send(buffer);
                  
                }
            }
        }
    }
}
