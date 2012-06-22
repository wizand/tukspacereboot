﻿using System;
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
    class EngineeringScreen : Screen
    {
        private Texture2D mainCutout;

        private SpriteFont toolTipFont;
        private Rectangle WarpNacelleRectangle;
        private Rectangle MainImpulseRectangle;

        private String toolTipText = "Hover over something";

        public EngineeringScreen(KeyboardState kState, MouseState mState, Ship pShip, Game1 theWorld, Texture2D engCutout)
            : base(kState, mState, pShip, theWorld)
        {
            mainCutout = engCutout;
        }

        public void Initialize(ContentManager Content)
        {
            toolTipFont = Content.Load<SpriteFont>("wizfont");
            //REMEMBER OFFSETS BASED OFF IMAGE AT 0,0!!!!!!!!!!!!!!!!!!!!!
            WarpNacelleRectangle = new Rectangle(0, 60, 200, 30);
            MainImpulseRectangle = new Rectangle(261, 28, 311, 15);
            base.Initialize();
        }

        public override void HandleInput(GameTime gameTime, KeyboardState kState, MouseState mState)
        {
            Point mousePoint = new Point(mState.X, mState.Y);
            if (WarpNacelleRectangle.Contains(mousePoint))
            {
                toolTipText = "Warp Nacelles";
            }
            else if (MainImpulseRectangle.Contains(mousePoint))
            {
                toolTipText = "Main Impulse Engine";
            }
            else
            {
                toolTipText = "Hover over something";
            }

            base.HandleInput(gameTime, kState, mState);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(mainCutout, Vector2.Zero, Color.White);

            spriteBatch.DrawString(toolTipFont, toolTipText, new Vector2(30, 201), Color.White);
            spriteBatch.DrawString(toolTipFont, oldMState.X.ToString() + ", " + oldMState.Y.ToString(), new Vector2(30, 300), Color.White);
            spriteBatch.End();
            base.Draw(gameTime, spriteBatch);
        }
    }
}
