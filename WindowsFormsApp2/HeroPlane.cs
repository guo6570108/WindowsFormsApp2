using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Properties;

namespace WindowsFormsApp2
{
    class HeroPlane : PlaneFather
    {
        private static  Image imgHero = Resources.hero1;//导入玩家图片飞机

        //调用父类构造函数
        public HeroPlane(int x, int y ,int speed ,int lift ,Direction dir):base(x,y,imgHero,lift,speed,dir)
        { }

        public override void Draw(Graphics g)
        {
            this.MoveBorder();
            g.DrawImage(imgHero, this.X, this.Y,Width/2,Height/2);
        }

        public override void Fire()
        {
            //玩家发射子弹 
            SingleObject.GetSingle().AddGameObject(new HeroZiDan(this, 20, 1));
        }

        public override void MoveBorder()
        {
            if (this.X<=0)
            {
                this.X = 0;
            }
            if (this.Y<=0)
            {
                this.Y = 0;
            }
            if (this.X>=480-this.Width/2)
            {
                this.X = 480 - this.Width/2;
            }
            if (this.Y>=812-this.Height/2 )
            {
                this.Y = 812 - this.Height/2;
            }
        }


        public void MoveWithMouse(MouseEventArgs e)
        {
            this.X = e.X;
            this.Y = e.Y;
        }

        public void MouseDownLeft(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Fire();
            }
        }

        public override void IsOver()
        {
            //玩家死亡
            SingleObject.GetSingle().AddGameObject(new HeroBoom(this.X,this.Y));
        }
    }
}
