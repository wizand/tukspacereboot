using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace tukSpace
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    class KobayashiMaru : Scenarios.Scenario
    {

        KeyboardState oldKState;
        KeyboardState kState;
        MouseState oldMState;
        MouseState mState;
        bool gamePaused; //just for testing global key hook

        Ship pShip;

        //putting all ships in the same list so i can mass handle em
        private List<tukSpace.Ship> allShips = new List<tukSpace.Ship>();
        public List<tukSpace.Ship> getAllShips() { return this.allShips; }
        public void setAllShips(List<tukSpace.Ship> allShips) { this.allShips = allShips; }

        HelmScreen helmScreen;
        TitleScreen titleScreen;
        TacNavScreen tacNavScreen;
        EngineeringScreen engScreen;
        ScienceScreen sciScreen;

        public KobayashiMaru(GraphicsDevice gr)
            : base(gr)
        {
        }

        public override void Initialize()
        {
            oldKState = Keyboard.GetState();
            pShip = new Ship(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2, false, 0f, true, null);
            pShip.SetSensorMode(SensorMode.SHORT);
            //Adding few NPShips just for test
            allShips.Add(new Ship(300, 300, true, 90, true, null));
            allShips.Add(new Ship(1200, 100, false, 30, true, null));
            allShips.Add(new Ship(600, 700, true, "enemy1", true, null));

            gamePaused = false;
        }

        public override void LoadContent(ContentManager Content)
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            helmScreen = new HelmScreen(Keyboard.GetState(), Mouse.GetState(), pShip, this, allShips, graphics.Viewport);
            titleScreen = new TitleScreen();
            tacNavScreen = new TacNavScreen(Keyboard.GetState(), Mouse.GetState(), pShip, this, allShips, graphics.Viewport);
            engScreen = new EngineeringScreen(Keyboard.GetState(), Mouse.GetState(), pShip, this, Content.Load<Texture2D>("enterprise_cutout"));
            sciScreen = new ScienceScreen(Keyboard.GetState(), Mouse.GetState(), pShip, this);

            waypointText = Content.Load<Texture2D>("waypoint");
            firepointText = Content.Load<Texture2D>("firepoint");

            //Loading content for the NPShips. Using the overloaded LoadContent method so
            //that if an texture isnt set in the constructor, "enterprise90t" will be used.
            foreach (Ship currentShip in allShips) { currentShip.LoadContent(Content); }

            pShip.LoadContent(Content, "enterprise90tCOLLISION");

            helmScreen.Initialize(Content);
            titleScreen.Initialize(Content);
            tacNavScreen.Initialize(Content);
            engScreen.Initialize(Content);
            sciScreen.Initialize(Content);

            curScreen = titleScreen;
        }

        public override void Update(GameTime gameTime)
        {
            pShip.Update(gameTime);

            foreach (Ship currentShip in allShips)
            {
                currentShip.Update(gameTime);
                if (tukHelper.determineDistance(currentShip.myPosition, pShip.myPosition) > 1000f) //should we still draw the ship or is it too far away?
                {
                    currentShip.visible = false;
                }
                else //i guess we are close enough so lets set it to true
                {
                    currentShip.visible = true;
                }
                if (!pShip.Equals(currentShip))
                {
                    currentShip.collisionRectangle.X -= 10;
                    if (currentShip.collisionRectangle.Contains(new Point((int)pShip.beamController.singleFireTarget.X, (int)pShip.beamController.singleFireTarget.Y)))
                    {
                        currentShip.shieldsUp = !currentShip.shieldsUp;
                        currentShip.shieldPercentage -= 30 * gameTime.ElapsedGameTime.Milliseconds * 0.01f;
                    }
                    //still off.
                    if (RectCollision.Check(currentShip.collisionRectangle, pShip.ROTATION_POINT, currentShip.rotationAngle,
                        pShip.collisionRectangle, pShip.myPosition, pShip.rotationAngle))
                    {
                        currentShip.shieldsUp = !currentShip.shieldsUp;
                    }
                }
            }
            
            if (curScreen.ReleaseMe == true)
            {
                curScreen = helmScreen;
            }
            curScreen.Update(gameTime); //update our current screen
            HandleInput(gameTime); //calls our global input hook, which passes any unprocessed input to the currentitleScreen
        }

        /// <summary>
        /// Handles all input. It checks for any input that is not dependant
        /// on current screen, if nothing is found, the input is passed to the current screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// moved curScreen.HandleInput() to Game1.HandleInput() to allow screen independant input.
        private void HandleInput(GameTime gameTime)
        {
            kState = Keyboard.GetState();
            mState = Mouse.GetState();
            if (kState.IsKeyDown(Keys.Escape))
            {
                if (!oldKState.IsKeyDown(Keys.Escape))
                {
                    gamePaused = !gamePaused;
                    System.Console.WriteLine("-------------\nPaused: " + gamePaused.ToString());
                }
            }
            if (kState.IsKeyDown(Keys.F1))
            {
                if (!oldKState.IsKeyDown(Keys.F1))
                {
                    curScreen = helmScreen;
                }
            }

            if (kState.IsKeyDown(Keys.F2))
            {
                if (!oldKState.IsKeyDown(Keys.F2))
                {
                    curScreen = tacNavScreen;
                }

            }
            if (kState.IsKeyDown(Keys.F3))
            {
                if (!oldKState.IsKeyDown(Keys.F3))
                {
                    curScreen = engScreen;
                }
            }
            if (kState.IsKeyDown(Keys.F4))
            {
                if (!oldKState.IsKeyDown(Keys.F4))
                {
                    curScreen = sciScreen;
                }
            }

            else
            {
                curScreen.HandleInput(gameTime, kState, mState);
            }
            oldKState = kState;
            oldMState = mState;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.Black);
            curScreen.Draw(gameTime, spriteBatch);
        }

        public override void HandleNetworking()
        {
            
        }
    }
}
