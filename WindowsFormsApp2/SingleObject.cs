using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    /// <summary>
    /// 单例类
    /// </summary>
    class SingleObject
    {
        #region 单例类
        private SingleObject()
        { }
        private static SingleObject _single = null;

        public static SingleObject GetSingle()
        {
            if (_single == null)
            {
                _single = new SingleObject();
            }
            return _single;
        }
        #endregion

        #region 游戏对象
        //在单例类中存储背景对象
        public Background BG { get; set; }
        //玩家对象
        public HeroPlane HP { get; set; }
        //敌人对象有很多需要泛型集合来声明
        public List<EnemyPlane> listEnemyPlane = new List<EnemyPlane>();
        //子弹也不仅仅只有一个 也需要个集合来存

        public List<HeroZiDan> listHeroZiDan = new List<HeroZiDan>();
        //储存敌人子弹 
        public List<EnempyZiDan> listEnempyZiDan = new List<EnempyZiDan>();
        //储存爆炸集合
        public List<EnemyBoom> listEnemBoom = new List<EnemyBoom>();
        //玩家爆炸集合 
        public List<HeroBoom> listHeroBoom = new List<HeroBoom>();


        #endregion

        //写一个函数 将游戏对象们添加到游戏场景中
        public void AddGameObject(GameObjcte go)
        {
            if (go is Background)
            {
                //如果传进来的是背景的对象，则赋值给SingleObject类中的BG属性
                this.BG = go as Background;

                
            }
            else if (go is HeroPlane)
            {
                this.HP = go as HeroPlane;
            }
            else if (go is EnemyPlane)
            {
                this.listEnemyPlane.Add(go as EnemyPlane);
            }
            else if (go is HeroZiDan)
            {
                this.listHeroZiDan.Add(go as HeroZiDan);
            }
            else if  (go is EnempyZiDan)
            {
                this.listEnempyZiDan.Add(go as EnempyZiDan);
            }
            else if (go is EnemyBoom)
            {
                this.listEnemBoom.Add(go as EnemyBoom);
            }
            else if (go is HeroBoom)
            {
                this.listHeroBoom.Add(go as HeroBoom);
            }
        }
        //从游戏中将游戏对象移除
        public void RemoveGameObject(GameObjcte go)
        {
            if (go is EnemyPlane)
            {
                //将当前这架敌人飞机移除                  
                listEnemyPlane.Remove(go as EnemyPlane);
            }
           else if (go is HeroZiDan)
            {
                listHeroZiDan.Remove(go as HeroZiDan);
            }
            else if (go is EnempyZiDan)
            {
                listEnempyZiDan.Remove(go as EnempyZiDan);
            }
            else if (go is EnemyBoom)
            {
                listEnemBoom.Remove(go as EnemyBoom);
            }
            else if ( go is HeroBoom)
            {
                listHeroBoom.Remove(go as HeroBoom);
            }
        }
        public  void DrawGameObject(Graphics g)
        {
            //把所有游戏对象绘制到窗体的draw函数中 
           // this.BG.Draw(g);
            this.HP.Draw(g);
            //将每一架飞机都绘制到窗体上
            for (int i = 0; i < listEnemyPlane.Count; i++)
            {
               this. listEnemyPlane[i].Draw(g);
            }
            for (int i = 0; i < listHeroZiDan.Count; i++)
            {
                this.listHeroZiDan[i].Draw(g);
            }
            for (int i = 0; i < listEnempyZiDan.Count; i++)
            {
                this.listEnempyZiDan[i].Draw(g);
            }
            for (int i = 0; i < listEnemBoom.Count; i++)
            {
                this.listEnemBoom[i].Draw(g);
            }
            for (int i = 0; i < listHeroBoom.Count; i++)
            {
                this.listHeroBoom[i].Draw(g);
            }
        }

        //声明一个属性来储存玩家分数
        public int Score { get; set; }
        /// <summary>
        /// 碰撞检测 
        /// </summary>
        public void PZJC()
        {
            #region 检查玩家子弹是否击中敌人
            for (int i = 0; i < listHeroZiDan.Count; i++)
            {
                //玩家打出了一发子弹，进入循环，判断是否打中飞机 
                for (int j = 0; j < listEnemyPlane.Count; j++)
                {
                    if (listHeroZiDan[i].GetRectangle().IntersectsWith(listEnemyPlane[j].GetRectangle()))
                    {
                        //如果成立则表示玩家子弹击中了敌人敌人
                        //敌人的生命值减小
                        listEnemyPlane[j].Lift -= listHeroZiDan[i].Power;
                        //判断敌人生命值是否为0
                        if (listEnemyPlane[j].Lift==0)
                        {
                            CountScore(j);
                        }
                        listEnemyPlane[j].IsOver();
                        //移除子弹
                        listHeroZiDan.Remove(listHeroZiDan [i]);
                        break;
                    }
                }
            }
            #endregion


            #region 检查敌人子弹是否击中玩家飞机
            for (int i = 0; i < listEnempyZiDan.Count; i++)
            {
                if (listEnempyZiDan[i].GetRectangle().IntersectsWith(this.HP.GetRectangle()))
                {
                    this.HP.IsOver();
                    //移除敌人子弹
                    listEnempyZiDan.Remove(listEnempyZiDan[i]);
                }
            }
            #endregion


            #region 检查玩家是否和敌人发生了碰撞
            for (int i = 0; i < listEnemyPlane.Count; i++)
            {
                if (listEnemyPlane[i].GetRectangle().IntersectsWith(this.HP.GetRectangle()))
                {
                    listEnemyPlane[i].Lift = 0;
                    CountScore(i);
                    listEnemyPlane[i].IsOver();
                }
            }
            #endregion

            #region 检查玩家子弹是否打中敌人子弹
            for (int i = 0; i < listHeroZiDan.Count; i++)
            {
                for (int j = 0; j < listEnempyZiDan.Count; j++)
                {
                    if (listHeroZiDan[i].GetRectangle().IntersectsWith(listEnempyZiDan[j].GetRectangle()))
                    {
                        listEnempyZiDan.Remove(listEnempyZiDan[j]);

                        listHeroZiDan.Remove(listHeroZiDan[i]);
                        break;
                    }
                }
            }
            #endregion
        }

        private void CountScore(int j)
        {
            switch (listEnemyPlane[j].EnemyType)
            {
                case 0:
                    this.Score += 100;
                    break;
                case 1:
                    this.Score += 200;
                    break;
                case 2:
                    this.Score += 300;
                    break;
            }
        }
    }
}
