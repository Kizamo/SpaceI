using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    abstract class Projectile
    {
        protected Rectangle pos;
        protected int speed;
        protected bool dead;
        private Graphics graphic;
        protected Image image;
        protected Rectangle startPos;

        public Projectile(Graphics graphic, Rectangle startPos)
        {
            this.graphic = graphic;
            this.startPos = startPos;
            dead = false;
        }
        abstract public void Move();
        public void Draw() // Draws the image
        {
            graphic.DrawImage(image, pos.X, pos.Y);
        }
        public bool CheckForHit(Rectangle checkPos) // Checks if the provided rectangle is touching the projectiles position. Returns true and sets dead to true if it is.
        {
            bool hit = false;
            if ((pos.X <= checkPos.Right) && (pos.Right >= checkPos.X) && (pos.Y <= checkPos.Bottom) && (pos.Bottom >= checkPos.Y))
            {
                hit = true;
                dead = true;
            }
            return hit;
        }
        public bool Dead //encapsulated field
        {
            get
            {
                return dead;
            }
        }
    }
}
