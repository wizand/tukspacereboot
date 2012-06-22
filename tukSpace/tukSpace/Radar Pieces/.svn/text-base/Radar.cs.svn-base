using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace tukSpace
{
    class Radar
    {
        protected Texture2D PlayerDotImage;
        protected Texture2D EnemyDotImage;
        protected Texture2D WayPointImage;
        public Texture2D RadarImage;

        // Local coords of the radar image's center, used to offset image when being drawn
        private Vector2 RadarImageCenter;

        // Distance that the radar can "see"
        protected float RadarRange;

        // Radius of radar circle on the screen
        protected float RadarScreenRadius;

        //How much should we scale distance down?
        protected float RADAR_SCALE_VALUE = 0.75f;

        // This is the center position of the radar hud on the screen. 
        protected static Vector2 RadarCenterPos;

        public Radar(ContentManager Content, string playerDotPath, string enemyDotPath, string radarImagePath,
            float rRange, float rScreenRadius, Vector2 rCenterPos)
        {
            PlayerDotImage = Content.Load<Texture2D>(playerDotPath);
            EnemyDotImage = Content.Load<Texture2D>(enemyDotPath);
            WayPointImage = Content.Load<Texture2D>("waypointDot"); //yea yea i need to fix but so tired...
            RadarImage = Content.Load<Texture2D>(radarImagePath);

            RadarImageCenter = new Vector2(RadarImage.Width / 2.0f, RadarImage.Height / 2.0f);

            RadarRange = rRange;

            //RadarScreenRadius = rScreenRadius;
            RadarScreenRadius = RadarImage.Width / 2 + 35;

            RadarCenterPos = rCenterPos;
        }


        public virtual void Draw(SpriteBatch spriteBatch, Vector2 playerPos, ref List<Vector2> scanResults, ref List<Vector2> wayPoints)
        {
            Console.WriteLine("DEBUG: Radar Draw-method called. ");
        }


    }
}
