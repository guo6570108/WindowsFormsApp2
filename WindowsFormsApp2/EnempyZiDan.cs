using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Properties;

namespace WindowsFormsApp2
{
    class EnempyZiDan:ZiDan
    {
        private static Image img = Resources.bullet21;
        //描述飞机的类型
        public int Type { get; set; }

        public EnempyZiDan(PlaneFather pf ,int type ):base(pf,GetPowerType(type),img,pf.X+pf.Width/2,pf.Y+pf.Height,GetSpeedType(type))
        {
            this.Type = type;
        }

        public static int GetPowerType(int Type)
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
        public static int GetSpeedType(int Type)
        {
            switch (Type)
            {
                case 0:
                    return 40;
                case 1:
                    return 30;
                case 2:
                    return 20;

            }
            return 0;
        }
    }
}
