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
    class Universe
    {
        public Quadrant[] quadrants;
        Coordinates2 systems;
        Coordinates2 sectors;

        public Universe(Coordinates2 systems, Coordinates2 sectors)
        {
            this.systems = systems;
            this.sectors = sectors;
            quadrants = new Quadrant[4];
        }

        //this code looks scarier than it is. all we do is loop through possible sector to create it. but
        //we have to go through quadrant->system->sector to do it.
        public void Construct()
        {
            System.Console.WriteLine("Please wait, constructing universe.");
            for (int i = 0; i < 4; i++)
            {
                //create the 4 quadrants
                quadrants[i] = new Quadrant(i);
                quadrants[i].systems = new tukSystem[this.systems.X][];

                //create our systems
                for (int j = 0; j < systems.X; j++)
                {
                    quadrants[i].systems[j] = new tukSystem[this.systems.Y];
                    for (int k = 0; k < systems.Y; k++)
                    {
                        //create our sectors
                        quadrants[i].systems[j][k] = new tukSystem(new Coordinates3(i, j, k));
                        quadrants[i].systems[j][k].sectors = new Sector[this.sectors.X][];
                        for (int m = 0; m < sectors.X; m++)
                        {
                            quadrants[i].systems[j][k].sectors[m] = new Sector[this.sectors.Y];
                            for (int n = 0; n < sectors.Y; n++)
                            {
                                quadrants[i].systems[j][k].sectors[m][n] = new Sector(new Coordinates5(i, j, k, m, n), 800);
                                System.Console.WriteLine(quadrants[i].systems[j][k].sectors[m][n].ToString() + " created.");
                            }
                        }
                    }
                }
            }
            System.Console.WriteLine("Universe constructed.");
        }

        public Sector FetchSector(Coordinates5 coords)
        {
            return quadrants[coords.X].systems[coords.Y][coords.Z].sectors[coords.W][coords.V];
        }

        public Sector FetchNewSector(Coordinates5 coords, int wmod, int vmod)
        {
            coords.W += wmod;
            coords.V += vmod;
            return FetchSector(coords);
        }

        //fuck you dumb implementation
        public Coordinates5 UpdateLocation(Sector curSector, Vector2 curPosition)
        {
            Coordinates5 curCoords = curSector.coords;
            Coordinates5 newCoords = curCoords; //in case this fails for whatever reason, ship won't change its world location

            if (curPosition.X < 0)
            {
                //move to the left
            }
            else if (curPosition.X > curSector.borderLength)
            {
                //move to the right
            }

            if (curPosition.Y < 0)
            {
                //move up
            }
            else if (curPosition.Y > curSector.borderLength)
            {
                //move down
            }

            return newCoords;
        }

    }
}