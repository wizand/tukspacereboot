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

//comment added to test bitbucket
namespace tukSpace
{
    class ScienceScreen : Screen
    {

        private SpriteFont toolTipFont;
        private Rectangle LongButtonRectangle;
        private Texture2D LongButtonTexture;

        private Texture2D MediumButtonTexture;
        private Rectangle MediumButtonRectangle;

        private Rectangle ShortButtonRectangle;
        private Texture2D ShortButtonTexture;

        private String toolTipText = "Hover over something";

        public ScienceScreen(KeyboardState kState, MouseState mState, Ship pShip, Scenarios.Scenario theWorld)
            : base(kState, mState, pShip, theWorld)
        {
            
        }

        public void Initialize(ContentManager Content)
        {
            toolTipFont = Content.Load<SpriteFont>("wizfont");
            //REMEMBER OFFSETS BASED OFF IMAGE AT 0,0!!!!!!!!!!!!!!!!!!!!!
            //this rectangles represent areas of the cutout that correspond to systems we want
            //the user to interact with. 
            
 ShortButtonTexture = Content.Load<Texture2D>("short");
            ShortButtonRectangle = new Rectangle(0, 0, ShortButtonTexture.Width, ShortButtonTexture.Height);

            MediumButtonTexture = Content.Load<Texture2D>("medium");
            MediumButtonRectangle = new Rectangle(ShortButtonTexture.Width, 0, MediumButtonTexture.Width, MediumButtonTexture.Height);

           
LongButtonTexture = Content.Load<Texture2D>("long");
            LongButtonRectangle = new Rectangle(ShortButtonTexture.Width*2, 0, LongButtonTexture.Width, LongButtonTexture.Height);

            toolTipText = "Select sensor mode";

            base.Initialize();
        }

        public override void HandleInput(GameTime gameTime, KeyboardState kState, MouseState mState)
        {
            //mouse picking (detecting the mouse over an object) works like this
            //create a tiny rectangle at the mouse coordinates that 1x1 pixels
            //see if that rectangle is within any of other boxes which represent areas on the cutout
            //do whatever we want; in this case we just adjust a tooltip
            Point mousePoint = new Point(mState.X, mState.Y);

            if (mState.LeftButton == ButtonState.Released && oldMState.LeftButton == ButtonState.Pressed) //we have a click!
            {
                //now to check for button clicks
                if (ShortButtonRectangle.Contains(mousePoint))
                {
                    pShip.SetSensorMode(SensorMode.SHORT);
                    toolTipText = "Sensor Mode: Short";
                }
                else if (MediumButtonRectangle.Contains(mousePoint))
                {
                    pShip.SetSensorMode(SensorMode.MEDIUM);
                    toolTipText = "Sensor Mode: Medium";
                }
                else if (LongButtonRectangle.Contains(mousePoint))
                {
                    pShip.SetSensorMode(SensorMode.LONG);
                    toolTipText = "Sensor Mode: Long";
                }
            }

            base.HandleInput(gameTime, kState, mState);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ShortButtonTexture, Vector2.Zero, Color.White);
            spriteBatch.Draw(MediumButtonTexture, new Vector2(ShortButtonTexture.Width, 0), Color.White);
            spriteBatch.Draw(LongButtonTexture, new Vector2(MediumButtonTexture.Width*2, 0), Color.White);
            spriteBatch.DrawString(toolTipFont, toolTipText, new Vector2(0,ShortButtonTexture.Height+5), Color.White);
            spriteBatch.End();
            base.Draw(gameTime, spriteBatch);
        }
    }
}
