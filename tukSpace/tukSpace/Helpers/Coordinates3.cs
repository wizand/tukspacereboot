using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tukSpace
{
    public class Coordinates3 : Coordinates2
    {
        public int Z { get; set; }

        public Coordinates3(int x, int y, int z)
            : base(x, y)
        {
            this.Z = z;
        }

        public override string ToString()
        {
            return base.ToString() + ":" + Z.ToString();
        }
    }
}
