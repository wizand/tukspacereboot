using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tukSpace
{
    public class Coordinates4 : Coordinates3
    {

        public int W { get; set; }

        public Coordinates4(int x, int y, int z, int w)
            : base(x, y, z)
        {
            this.W = w;
        }

        public override string ToString()
        {
            return base.ToString() + ":" + W.ToString();
        }
    }
}
