using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Properties;

namespace WindowsFormsApp2
{
    class ZiDan:GameObjcte 
    {
        private Image imgZiDan;
        public int Power { get; set; }
        public ZiDan(PlaneFather pf, int power, Image img, int x, int y, int speed) : base(x, y, speed, 0, img.Height, img.Width, pf.Dir)
        {
            this.imgZiDan = img;
            this.Power = power;
            
        }

        public override void Draw(Graphics g)
        {
           this. Move();
            g.DrawImage(imgZiDan, this.X, this.Y,this.Width/2,this.Height/2);
        }

        public override void MoveBorder()
        {
            if (this.Y<=0||this.Y>=812)
            {
                //将子弹对象移除
                SingleObject.GetSingle().RemoveGameObject(this);
            }
        }
    }
}
