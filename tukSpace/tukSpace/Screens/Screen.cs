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
    public class Screen
    {
        protected Scenarios.Scenario theWorld;
        protected KeyboardState oldKState;
        protected MouseState oldMState;

        protected Ship pShip;
        public bool ReleaseMe;
    


        public Screen()
        {
        }

        public Screen(KeyboardState kState, MouseState mState, Ship theShip, Scenarios.Scenario theWorld)
        {
            pShip = theShip;
            oldKState = kState;
            oldMState = mState;
            this.theWorld = theWorld;

        }

        public void Initialize()
        {
            ReleaseMe = false;
        }

        public virtual void HandleInput(GameTime gameTime, KeyboardState kState, MouseState mState)
        {
            oldKState = kState;
            oldMState = mState;
        }



        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
