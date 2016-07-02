using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class MotherShip : Ship
    {
        private const int SIZE = 52;

        public MotherShip(Graphics graphic, Point bounds)
            : base(graphic, bounds)
        {
            image = Properties.Resources.motherShip;
            pos = new Rectangle(bounds.X / 2, bounds.Y - SIZE, SIZE, SIZE);
        }
        public void Move(int mouseX) // moves the mother ship to the provided X location. If the location is out of the screen bounds the mother ship's location is set to the edge of the bounds
        {
            pos.X = mouseX -( SIZE) / 2;
            if (pos.X + SIZE > bounds.X)
            {
                pos.X = bounds.X - SIZE;
            }
            else
            {
                if (pos.X < 0)
                {
                    pos.X = 0;
                }
            }
        }
    }
}
