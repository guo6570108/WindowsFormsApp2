using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Properties;

namespace WindowsFormsApp2
{
    class HeroBoom:Boom
    {
        private Image[] heroImage =
        {
                            Resources.hero_blowup_n1,
                            Resources.hero_blowup_n2,
                            Resources.hero_blowup_n3,
                            Resources.hero_blowup_n4
        };
        public HeroBoom(int x,int y):base(x,y)
        {

        }

        public override void Draw(Graphics g)
        {
            for (int i = 0; i < heroImage.Length ; i++)
            {
                g.DrawImage(heroImage[i], this.X, this.Y);

                SingleObject.GetSingle().RemoveGameObject(this);
            }
        }

        public override void MoveBorder()
        {
            //设计BUg

        }
    }
}
