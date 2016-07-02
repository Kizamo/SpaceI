using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class EnemyShip : Ship
    {
        private Point size;
        private bool isKing;
        private Point startPos;
        private int speed;

        public EnemyShip(Graphics graphic, Point bounds, Point startPos, Image image, Point size, bool isKing, int speed)
            :base (graphic, bounds)
        {
            this.size = size;
            this.isKing = isKing;
            this.image = image;
            this.startPos = startPos;
            this.speed = speed;
            pos = new Rectangle(startPos.X, startPos.Y, size.X, size.Y);
        }
        public void Move(bool dir, bool drop) //Moves the enemy ship right if dir is true or left if its false. Moves theship down if drop is true.
        {
            if (dir)
            {
                pos.X = pos.X + speed;
            }
            else
            {
                pos.X = pos.X - speed;
            }
            if (drop)
            {
                pos.Y = pos.Y + speed;
            }
        }
        public bool HitWall() //checks if the X location is out of the screen boundary and returns true if it is
        {
            bool hit = false;
            if(pos.X <= 0)
            {
                hit = true;
            }
            else
            {
                if(pos.Right >= bounds.X)
                {
                    hit = true;
                }
            }
            return hit;
        }
        public bool IsKing // encapsulated field
        {
            get
            {
                return isKing;
            }
        }
    }
}
