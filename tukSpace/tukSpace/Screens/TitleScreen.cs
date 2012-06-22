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
    class TitleScreen : Screen
    {
        private Texture2D titleBackdrop;

        public TitleScreen()
            : base()
        { }


        public void Initialize(ContentManager Content)
        {
            titleBackdrop = Content.Load<Texture2D>("title");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(titleBackdrop, new Vector2(0, 20), Color.White);
            spriteBatch.End();
            base.Draw(gameTime, spriteBatch);
        }

        public override void HandleInput(GameTime gameTime, KeyboardState kState, MouseState mState)
        {
            if (kState.IsKeyDown(Keys.Space))
                ReleaseMe = true;

            base.HandleInput(gameTime, kState, mState);
        }
    }
}
