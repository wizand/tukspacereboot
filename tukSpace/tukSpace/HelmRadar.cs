using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace tukSpace
{
    class HelmRadar : Radar
    {

        //example values:
        // rRange = 500.0f, rScreenRadius = 150.0f, rCenterPos = new Vector(150, 150)
        public HelmRadar(ContentManager Content, string playerDotPath, string enemyDotPath, string radarImagePath,
                         float rRange, float rScreenRadius, Vector2 rCenterPos) 
                 : base(Content, playerDotPath,  enemyDotPath, radarImagePath, rRange, rScreenRadius, rCenterPos)
        {}

        public override void Draw(SpriteBatch spriteBatch, Vector2 playerPos, ref List<Vector2> scanResults)
        {

            foreach (Vector2 thisEnemy in scanResults)
            {
                Vector2 diffVect = thisEnemy - playerPos;
                float distance = diffVect.Length();

                // Check if enemy is within RadarRange
                if (distance < RadarRange)
                {
                    // Scale the distance from world coords to radar coords
                    diffVect *= RadarScreenRadius / RadarRange;

                    // Offset coords from radar's center
                    diffVect += RadarCenterPos;

                    // Draw enemy dot on radar
                    spriteBatch.Draw(EnemyDotImage, diffVect, Color.White);
                }
            }

            // Draw player's dot last
            spriteBatch.Draw(PlayerDotImage, RadarCenterPos, Color.White);     
          
        }

    }
}
