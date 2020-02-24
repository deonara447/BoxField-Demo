using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BoxField
{
    class Box
    {
        public SolidBrush boxBrush;
        public int x, y, size;
        Random randGen = new Random();

        public Box(int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;

            int randValue = randGen.Next(1, 3);

            if (randValue == 1)
            {
                boxBrush = new SolidBrush(Color.Red);
            }
            else if (randValue == 2)
            {
                boxBrush = new SolidBrush(Color.Orange);
            }
        }

        public Box(SolidBrush _boxBrush, int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;
            boxBrush = _boxBrush;
        }

        public void Fall()
        {
            y = y + 3;
        }

        public void Move(string direction)
        {
            if (direction == "left")
            {
                x = x - 5;
            }

            if (direction == "right")
            {
                x = x + 5;
            }

        }

    }
}
