using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Properties;

namespace WindowsFormsApp2
{
    class Background : GameObjcte
    {
        private static  Image img = Resources.background;

        public Background(int x,int y ,int speed ):base(x,y,speed,0,img.Height ,img.Width,Direction.Down)
        {

        }

        public override void Draw(Graphics g)
        {
            MoveBorder();
            g.DrawImage(img,this.X , this.Y);
        }

        public override void MoveBorder()
        {
            this.Y += this.Speed;
            if (this.Y>=0)
            {
                this.Y = -850;
            }
        }
    }
}
