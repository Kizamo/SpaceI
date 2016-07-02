using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Bomb : Projectile
    {
        private Point size;
        public Bomb(Graphics graphic, Rectangle startPos, int speed, Image image, Point size)
            :base (graphic, startPos)
        {
            this.size = size;
            this.speed = speed;
            this.image = image;
            pos = new Rectangle(startPos.X + startPos.Width / 2 - (size.X / 2), startPos.Y + size.Y, size.X, size.Y);
        }
        public override void Move() //Moves the bomb down the screen.
        {
            pos.Y = pos.Y + speed;
        }
        public void OffScreen(Point bounds) // sets dead to true if the bomb is out of the screen boundary.
        {
            if (pos.Y >= bounds.Y)
            {
                dead = true;
            }
        }
    }
}
