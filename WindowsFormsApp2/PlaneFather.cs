using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Properties;

namespace WindowsFormsApp2
{
   abstract  class PlaneFather : GameObjcte
    {
        private Image ImagePlan;
        public PlaneFather(int x,int y,Image img ,int left,int speed,Direction dir):base(x,y,speed,left,img.Height,img.Width ,dir)
        {
            this.ImagePlan = img;
        }



        public abstract void Fire();
        //判断当前的飞机是否死亡 
        public abstract void IsOver();
    }
}
