using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using WindowsFormsApp2.Properties;

namespace WindowsFormsApp2
{

   class EnemyPlane : PlaneFather
    {
        private static Image img1 = Resources.enemy0;
        private static  Image img2 = Resources.enemy1;
        private static  Image img3 = Resources.enemy2;
        //调用父类构造函数
        public EnemyPlane(int x, int y, int type) : base(x, y, GetImageWithType(type),
            GetLiftWithType(type),GetSpeedWithType(type),Direction.Down)
        {
            this.EnemyType = type;
        }
        //敌人飞机的类型 0,1,2
        public int EnemyType { get; set; }
        //根据不同的飞机返回不同的图片，生命以及速度
        /// <summary>
        /// 根据敌人的飞机类型返回不同的图片
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static  Image GetImageWithType(int Type)
        {
            switch (Type)
            {
                case 0:
                    return img1;
                case 1:
                    return img2;
                case 2:
                    return img3;
            }
            return null;
        }

        public static  int GetLiftWithType(int Type)
        {
            switch (Type)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 3;
            }
            return 0;
        }

        public static  int GetSpeedWithType(int Type)
        {
            switch (Type)
            {
                case 0:
                    return 10;
                case 1:
                    return 5;
                case 2:
                    return 2;

            }
            return 0;
        }
        //将自己绘制到图片上 
        public override void Draw(Graphics g)
        {
          
            //让飞机移动
            this.Move();
            //diaoyongxiaohui
            this.MoveBorder();
            switch (EnemyType)
            {
                case 0:
                    g.DrawImage(img1, this.X, this.Y);
                    break;
                case 1:
                    g.DrawImage(img2, this.X, this.Y  );
                    break;
                case 2:
                    g.DrawImage(img3, this.X, this.Y);
                    break;
            }
            //给敌人飞机一个机率来发射子弹
            if (r.Next(0,101)>90)
            {
                //开火
                Fire();
            }
        }
        Random r = new Random();
        public override void MoveBorder()
        {
            if (this.Y>=812)
            {
                //将敌人飞机销毁
                SingleObject.GetSingle().RemoveGameObject(this);
               
            }
            //小飞机的坐标>200后
            if (this.EnemyType==0 && this.Y >=200)
            {
                //表示小飞机在左边
                if (this.X>=0 && this.X<=240)
                {
                    this.X += r.Next(0, 200);

                }
                else
                {
                    this.X -= r.Next(0, 50);
                }
              
            }
           
        }

        public override void Fire()
        {
            //敌人发射子弹
            SingleObject.GetSingle().AddGameObject(new EnempyZiDan(this,this.EnemyType));
        }

        public override void IsOver()
        {
            if (this.Lift ==0)
            {
                //爆炸效果 
                SingleObject.GetSingle().AddGameObject(new EnemyBoom(this.X, this.Y, this.EnemyType));
                //移除敌人飞机
                SingleObject.GetSingle().RemoveGameObject(this);
            }
        }
    }
}
