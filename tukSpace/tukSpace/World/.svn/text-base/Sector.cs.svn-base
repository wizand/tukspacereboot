using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace tukSpace
{
    class Sector
    {
        public int borderLength; //our size, the overall shape is a square so just one value is needed.
        public Coordinates5 coords;

        public Sector(Coordinates5 coords, int borderLength)
        {
            this.coords = coords;
            this.borderLength = borderLength;
        }

        public override string ToString()
        {
            char d = '.';
            return coords.X.ToString() + d + coords.Y.ToString() + d + coords.Z.ToString() + d + coords.W.ToString() + d + coords.V.ToString();
        }
    }
}
