using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Properties;

namespace WindowsFormsApp2
{
    /// <summary>
    /// 玩家子弹类
    /// </summary>
    class HeroZiDan:ZiDan 
    {
        //导入玩家子弹的图片
        private static  Image imgHeroZiDan = Resources.bullet1;

        public HeroZiDan(PlaneFather pf, int speed, int power) : base(pf, power, imgHeroZiDan, pf.X + pf.Width / 4-4, pf.Y ,speed )
        {
            
        }
    }
}
