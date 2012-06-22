using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tukSpace
{
    class Coordinates2 //Holds an integer pair
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinates2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return X.ToString() + ":" + Y.ToString();
        }
    }
}
