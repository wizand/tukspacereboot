using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tukSpace
{
    public class Coordinates5 : Coordinates4
    {
        public int V { get; set; }

        public Coordinates5(int x, int y, int z, int w, int v)
            : base(x, y, z, w)
        {
            this.V = v;
        }

        public override string ToString()
        {
            return base.ToString() + ":" + V.ToString();
        }
    }
}
