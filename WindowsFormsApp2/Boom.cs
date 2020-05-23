using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
   abstract   class Boom : GameObjcte
    {
        public Boom(int x,int y ):base(x,y,0,0,0,0,Direction.Up)
        { 
            
        }
    }
}
