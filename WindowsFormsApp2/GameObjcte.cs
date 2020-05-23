using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    enum Direction
    {
        Up,
        Down,
        Left,
        Right

    }
  abstract  class   GameObjcte
    {
        #region 属性成员
        public int X { get; set; }

        public int Y { get; set; }
        public int Speed { get; set; }
        public int Lift { get; set; }
        public Direction Dir { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public GameObjcte(int x, int y, int speed, int lift, int height, int width, Direction dir)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            this.Lift = lift;
            this.Height = height;
            this.Width = width;
            this.Dir = dir;
        } 
        #endregion

       
        /// <summary>
        /// 游戏移动的方法
        /// </summary>
        public virtual  void Move()
        {
            switch (this.Dir)
            {
                case Direction.Up:
                    this.Y -= Speed;
                    break;
                case Direction.Down:
                    this.Y += Speed;
                    break;
                case Direction.Left:
                    this.X -= Speed;
                    break;
                case Direction.Right:
                    this.X += Speed;
                    break;
                 
            }
        }

        /// <summary>
        /// 绘制游戏对象的方法
        /// </summary>
        /// <param name="g"></param>
        public abstract void Draw(Graphics g);


        /// <summary>
        /// 当游戏对象移动到窗体边缘的时候，对游戏对象坐标的一个处理方式
        /// </summary>
        public abstract void MoveBorder();

        /// <summary>
        /// 获得当前对象获得的矩形,用于碰撞检测
        /// </summary>
        public  Rectangle  GetRectangle()
        {
            return new Rectangle(this.X, this.Y, this.Width, this.Height);
        }
        
    }
}
