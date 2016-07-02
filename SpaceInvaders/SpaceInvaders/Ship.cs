using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    abstract class Ship
    {
        protected bool dead;
        protected Graphics graphic;
        protected Point bounds;
        protected Image image;
        protected Rectangle pos;
        public Ship(Graphics graphic, Point bounds)
        {
            this.bounds = bounds;
            this.graphic = graphic;
            dead = false;
        }
        public void Draw() // Draws the image
        {
            graphic.DrawImage(image, pos.X, pos.Y);
        }
        public Rectangle Pos // encapsulated field
        {
            get
            {
                return pos;
            }
        }

        public bool Dead //encapsulated field
        {
            get
            {
                return dead;
            }

            set
            {
                dead = value;
            }
        }
    }
}
