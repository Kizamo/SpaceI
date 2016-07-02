using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Missile : Projectile
    {
        private const int SPEED = 40;
        private const int XSIZE = 9;
        private const int YSIZE = 18;
        private int lifespan;
        public Missile(Graphics graphic, Rectangle startPos, int lifespan)
            :base (graphic, startPos)
        {
            speed = SPEED;
            this.lifespan = lifespan;
            image = Properties.Resources.goodMissile;
            pos = new Rectangle(startPos.X + startPos.Width / 2 - (XSIZE / 2), startPos.Y + YSIZE, XSIZE, YSIZE);
        }
        public override void Move() //moves the missile up the screen if lifespan is greater than 0. Sets dead to true if its not.
        {
            if (lifespan > 0)
            {
                pos.Y = pos.Y - speed;
                lifespan--;
            }
            else
            {
                dead = true;
            }
        }

    }
}
