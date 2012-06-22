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
    class Shield
    {
        public bool Raised { get; set; } //am I up?
        public float AvailablePower { get; set;} //0.0 - 1.0
        public float DamageTaken { get; set;} //how much damage have I absorbed?
        public float Damaged { get; set;} //how damaged am i?
        public float Efficency { get; private set;} //how efficently am I running? am I wasting energy?
        
        private Texture2D myTexture;
        private Vector2 myDrawOffset;

        public Shield(Texture2D myTexture, Vector2 myDrawOffset)
        {
            this.myTexture = myTexture;
            this.myDrawOffset = myDrawOffset;
            Raised = false;
            //will be configured later as power systems get developed
            AvailablePower = 1.0f;
            Damaged = 0.0f;
            Efficency = 1.0f;

            //just start the counter at 0
            DamageTaken = 0.0f;
        }

        /// <summary>
        /// Updates all shield data that has been affected by past tick.
        /// </summary>
        public void Update()
        {
            //increment damagetaken counter, and 
            //will probably be some equation to recalculate the efficency of shields
        }

        /// <summary>
        /// Draw the Shield.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to use.</param>
        /// <param name="myPosition">Position of the parent ship</param>
        /// <param name="rotationAngle">Rotation angle of the parent ship (radians)</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 myPosition, float rotationAngle)
        {
            //will be changed to use myDrawOffset
            spriteBatch.Draw(myTexture, myPosition, null, Color.White, rotationAngle,
                                 new Vector2(27,11), 1.0f, SpriteEffects.None, 0f);
        }
      
    }
}
