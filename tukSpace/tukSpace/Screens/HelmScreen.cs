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

using Lidgren.Network;

namespace tukSpace
{
    class HelmScreen : Screen
    {
        float TIME_BASED_KEY_MOD = 0.01f; //uses to smoothen input by making time between updates a smaller delta for key input
        SpriteFont font;
        SpriteFont smallerFont;

        // ///////////////////////////
        // ///// RADAR STUFF ////// //
        HelmRadar hRadar;
        Vector2 RadarTopLeft; //use to calculate offset for radar coords

        //////////////////////////////
        // ///// HUD AND BG TEXTURES ///// //   
        Texture2D centerCircle;
        Texture2D bgGrid;

        Texture2D topFrame;
        Texture2D topGauge;
        Texture2D leftFrame;
        Texture2D rightFrame;
        Texture2D bottomFrame; 
        Texture2D upperLeft;
        Texture2D upperRight;
        Texture2D lowerLeft;
        Texture2D lowerRight;

        //Warp button
        Texture2D warpOff;
        Texture2D warpOn;
        Texture2D warpStatus;

        //Used to get the window width and height
        Viewport viewPort;
        Camera2D cam;

        // Place to store the list of NPShips
        List<Ship> allShips;

        public HelmScreen(KeyboardState kState, MouseState mState, Ship pShip, Scenarios.Scenario theWorld, List<Ship> allShips, Viewport viewPort)
            : base(kState, mState, pShip, theWorld)
        {
            System.Console.WriteLine("DEBUG: HelmScreen constructor accessed");
            this.allShips = allShips;
            this.viewPort = viewPort;
        }

        public void Initialize(ContentManager Content)
        {
            //camera
            cam = new Camera2D(viewPort);

            //Radar related:
            RadarTopLeft = new Vector2(viewPort.Width - 210, 10); //top right corner
            hRadar = new HelmRadar(Content, "playerDot", "enemyDot", "HelmRadarImage",
                     650.0f, 120.0f, new Vector2(RadarTopLeft.X + 100, RadarTopLeft.Y + 100));

            //FONTS
            font = Content.Load<SpriteFont>("impulse");
            smallerFont = Content.Load<SpriteFont>("wizfont");

            warpOn = Content.Load<Texture2D>("Warp_on");
            warpOff = Content.Load<Texture2D>("Warp_off");
            warpStatus = warpOff;

            //Hud components:
            topFrame = Content.Load<Texture2D>("top_frame");
            topGauge = Content.Load<Texture2D>("top_gauge");
            rightFrame = Content.Load<Texture2D>("right_frame");
            leftFrame = Content.Load<Texture2D>("left_frame");
            bottomFrame = Content.Load<Texture2D>("bottom_frame");

            bgGrid = Content.Load<Texture2D>("BgGrid");
            centerCircle = Content.Load<Texture2D>("BgCenterCircle");

            //Corner sprites:
            upperLeft = Content.Load<Texture2D>("lu_corner");
            upperRight = Content.Load<Texture2D>("ru_corner");
            lowerLeft = Content.Load<Texture2D>("Helm_model2");
            lowerRight = Content.Load<Texture2D>("rl_corner");

            base.Initialize();
        }

        /* //////////////////////////////////////////
           ///////////// INPUT HANDLING ///////////// */
         
        public override void HandleInput(GameTime gameTime, KeyboardState kState, MouseState mState)
        {
            //first 2 handle rotation
            //next 2 handle thrust: if impulse engine is in use, time based acceleration
            //                    : if warp engine is in use, check for a keypress to accelerate by 1 warp factor

            if (kState.IsKeyDown(Keys.Left))
            {
                pShip.RotateByValue((float)(gameTime.ElapsedGameTime.Milliseconds * TIME_BASED_KEY_MOD), -1);
            }
            else if (kState.IsKeyDown(Keys.Right))
            {
                pShip.RotateByValue((float)(gameTime.ElapsedGameTime.Milliseconds * TIME_BASED_KEY_MOD), 1);
            }

            if (pShip.engineController.WarpOn())
            {
                if (kState.IsKeyDown(Keys.Up))
                {
                    if (!base.oldKState.IsKeyDown(Keys.Up))
                    {
                        pShip.ThrustByValue(1.0f, 1);
                    }
                }
                else if (kState.IsKeyDown(Keys.Down))
                {
                    if (!base.oldKState.IsKeyDown(Keys.Down))
                    {
                        pShip.ThrustByValue(1.0f, -1);
                    }
                }
            }
            else
            {
                if (kState.IsKeyDown(Keys.Up))
                    pShip.ThrustByValue((float)(gameTime.ElapsedGameTime.Milliseconds * TIME_BASED_KEY_MOD), 1);
                else if (kState.IsKeyDown(Keys.Down))
                    pShip.ThrustByValue((float)(gameTime.ElapsedGameTime.Milliseconds * TIME_BASED_KEY_MOD), -1);
            }

            //Regular keypress checks
             //makes the first ship in allShips move.
            if (kState.IsKeyDown(Keys.I))
            {
                allShips[0].ThrustByValue((float)(gameTime.ElapsedGameTime.Milliseconds * TIME_BASED_KEY_MOD), -1);
            }
            else if (kState.IsKeyDown(Keys.W))
            {
                if (!base.oldKState.IsKeyDown(Keys.W))
                {
                    pShip.engineController.ChangeWarpState();

                    if (pShip.engineController.WarpOn()) { warpStatus = warpOn; }
                    else warpStatus = warpOff;
                }
            }

            else if (kState.IsKeyDown(Keys.A))
            {
                if (!base.oldKState.IsKeyDown(Keys.A))
                {
                    pShip.engineController.AllStop();
                    warpStatus = warpOff;
                }
            }


            if (mState.ScrollWheelValue != oldMState.ScrollWheelValue)
                cam.Zoom += (float)(mState.ScrollWheelValue - oldMState.ScrollWheelValue) * .001f;

           //being hacked in for testing

            //Rectangle mousePointer = new Rectangle(mState.X, mState.Y, pShip.myTexture.Width, pShip.myTexture.Height);
            Rectangle mousePointer = new Rectangle(mState.X, mState.Y, 1, 1);
            
            if (mState.LeftButton == ButtonState.Released && oldMState.LeftButton == ButtonState.Pressed)
            {
                if (RectCollision.Check(mousePointer,Vector2.Zero,0f,pShip.collisionRectangle,Vector2.Zero,pShip.rotationAngle))
                    pShip.shieldsUp = !pShip.shieldsUp;
            }
            

            base.HandleInput(gameTime, kState, mState);
        }


        //a better draw order will need to be established eventually.
        //player ship now draws last so its on top of everything else.
        //camera now added. First we use the sprite batch to draw objects that are relative to the player ship with funky settings
        //for camera. then we end that batch to start a new basic one for drawing hud. i'm not sure how to handle background with
        //this. perhaps make the background redraw as we move off screen?
        //then we end 
        //-----------------------------------------
        //i am not sure why this works how it does. the background is drawn first, i don't understand why it moves with the camera. i know its what we want
        //but i just changed the draw order a tad to avoid issues with flickering
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //test out this camera
            //quick hack to make camera work.
            cam.Pos = pShip.myPosition;

            //should just draw background
            spriteBatch.Begin();
            DrawBackground(spriteBatch);
            spriteBatch.End();

           spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(spriteBatch.GraphicsDevice));

                //Method to handle all NPShips drawing
                DrawNPShip(spriteBatch);
            
                //draw waypoints
                foreach (Vector2 currentWayPoint in theWorld.waypointList) spriteBatch.Draw(theWorld.waypointText, currentWayPoint, Color.White);
                foreach (Vector2 currentFirePoint in theWorld.firepointList) spriteBatch.Draw(theWorld.firepointText, currentFirePoint, Color.White);
                //Player ship last
                pShip.Draw(spriteBatch);

            spriteBatch.End();
            spriteBatch.Begin();

                //Drawing the radar related stuff. Needs to be drawn before the HUD so the radar sits nicely in 
                //the current HUD draft.
                DrawRadar(spriteBatch);

                //Created a method to handle all HUD related drawings
                DrawHUD(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime, spriteBatch);
        }


        //Method for drawing enemy ships in the allShips list. I took it to another method just to
        //make room for possible changes in the enemy ships.
        private void DrawHUD(SpriteBatch currentSB)
        {

            //Frames        
            for (int i = 0; i < 2; i++)
            {
                //TOP
                currentSB.Draw(topFrame, new Vector2(upperLeft.Width + (topFrame.Width * i), 0), Color.White);
                //BOTTOM
                currentSB.Draw(bottomFrame, new Vector2(lowerLeft.Width + (bottomFrame.Width * i), viewPort.Height - bottomFrame.Height), Color.White);
                //LEFT   :: tweaking the position below
                // currentSB.Draw(leftFrame, new Vector2(0, upperLeft.Height + (leftFrame.Height * i)), Color.White);
                //RIGHT
                currentSB.Draw(rightFrame, new Vector2(viewPort.Width - rightFrame.Width, upperRight.Height + (rightFrame.Height * i)), Color.White);
            }
            //LEFT
            currentSB.Draw(leftFrame, new Vector2(0, -50), Color.White);

            //a lil space to the top for coordinates maybe?
            currentSB.Draw(topGauge, new Vector2((viewPort.Width / 2) - (topGauge.Width / 2), 0), Color.White);

            //drawing the distance and _relative_ heading to the first waypoint if it exists
            if (theWorld.waypointList.Count > 0)
            {
                //System.Console.WriteLine("WP Distance: " + tukHelper.determineDistance(waypointList[0], pShip.myPosition));
                float tempAngle = AngleBetweenHeadingAndWaypoint();
                currentSB.DrawString(smallerFont, (tukHelper.determineDistance(theWorld.waypointList[0], pShip.myPosition).ToString() + "//" + tempAngle.ToString()),
                    new Vector2((viewPort.Width / 2) - 28, (topGauge.Height / 2) - 8), Color.White);
            }

            //Helm thing to lower left corner
            currentSB.Draw(lowerLeft, new Vector2(0, viewPort.Height - lowerLeft.Height), Color.White);
            //Corner sprites:
            currentSB.Draw(upperLeft, new Vector2(0, 0), Color.White);
            currentSB.Draw(upperRight, new Vector2(viewPort.Width - upperRight.Width, 0), Color.White);
            currentSB.Draw(lowerRight, new Vector2(viewPort.Width - lowerRight.Width, viewPort.Height - lowerRight.Height), Color.White);

            //drawing the coordinates to the helm hud thingie, later with better font
            currentSB.DrawString(smallerFont, (int)pShip.myPosition.X + "",
                new Vector2((lowerLeft.Width / 2) - 7,
                            viewPort.Height - (lowerLeft.Height / 2) - 25), Color.White);

            currentSB.DrawString(smallerFont, (int)pShip.myPosition.Y + "",
                new Vector2((lowerLeft.Width / 2) + 70,
                            viewPort.Height - (lowerLeft.Height / 2) - 25), Color.White);

            //draw velocity
            currentSB.DrawString(smallerFont, Math.Round(pShip.engineController.CurrentVelocity,2).ToString(),
                new Vector2((lowerLeft.Width / 2) + 85,
                            viewPort.Height - (lowerLeft.Height / 2) + 95), Color.White);

            //draw heading
            currentSB.DrawString(smallerFont, Math.Round(pShip.GetRotation()).ToString(),
                new Vector2((lowerLeft.Width / 2) + 85,
                            viewPort.Height - (lowerLeft.Height / 2) + 50), Color.White);

            //Warp state indicator
            currentSB.Draw(warpStatus, new Vector2(28, viewPort.Height - lowerLeft.Height + 21), Color.White);
            //mouse location
            string mouseCoords = oldMState.X.ToString() + "," + oldMState.Y.ToString();
            currentSB.DrawString(smallerFont, mouseCoords, Vector2.Zero, Color.White);
            string shipDimensions = pShip.myTexture.Width.ToString() + ", " + pShip.myTexture.Height;
            currentSB.DrawString(smallerFont, shipDimensions, new Vector2(0, 25), Color.White);
        }


        //Method for drawing the background
        private void DrawBackground(SpriteBatch currentSB)
        {
            //Grid and center circle
            currentSB.Draw(centerCircle, new Vector2(leftFrame.Width + viewPort.Width / 2 - centerCircle.Width / 2,
                                                     viewPort.Height / 2 - centerCircle.Height / 2), Color.White);
            for (int i = 0; i < 2; i++) currentSB.Draw(bgGrid, new Vector2(0 - 34, 0 + (i * bgGrid.Height) - 32 - i), Color.White);
            for (int i = 0; i < 2; i++) currentSB.Draw(bgGrid, new Vector2(bgGrid.Width - 34 - 1, 0 + (i * bgGrid.Height) - 32 - i), Color.White);
        }

        //Method for drawing enemy ships in the allShips list. 
        //Only draws ship if Ship.amIVisible is set to true.
        private void DrawNPShip(SpriteBatch currentSB)
        {
            foreach (Ship currentShip in allShips)
            {
                if (currentShip.visible == true) currentShip.Draw(currentSB);
            }
        }


        private void DrawRadar(SpriteBatch currentSB)
        {

            //draw the radar background
            currentSB.Draw(hRadar.RadarImage, RadarTopLeft, Color.White);

            //Im using allShips lists ships as an simulation for the future scan results thing.       
            List<Vector2> scanResults = new List<Vector2>();
            foreach (Ship ship in allShips)
            {
                if (ship.visible == true) //if we aren't drawing the ship, then for now lets not show it in radar either
                {
                    scanResults.Add(ship.myPosition);
                }
            }

            hRadar.Draw(currentSB, pShip.myPosition, ref scanResults, ref theWorld.waypointList);
        }


        //Method name says it all. The idea was to call the tukhelper method with ship coordinates, 
        //next waypoint coordinate and the coordinates from somewhere from the direction of heading.

        public float AngleBetweenHeadingAndWaypoint()
        {
            if (theWorld.waypointList.Count >0)
            {
                /****
                 * seems to work now, saved your old code for legacy. although this function needs to be cleaned up, as does
                 * tukHelper. we made a mess!
                 * 
                 * 
                //using the same lenght vector than the distance between wp and ship
                double distance = tukHelper.determineDistance(theWorld.waypointList[0], pShip.myPosition);

                //this might very well be the problem. my brain is freezing out now.
                Vector2 headingCoords = new Vector2((float)(distance * Math.Cos(pShip.rotationAngle)), (float)(distance * Math.Sin(pShip.rotationAngle)));

                Console.WriteLine("New coords in Heading direction: " + headingCoords.X + " " + headingCoords.Y);
                Console.WriteLine("Distance to the Wp: " + distance);

                Vector2 waypointCoords = theWorld.waypointList[0];

                //return tukHelper.AngleBetween(pShip.myPosition, waypointCoords, headingCoords);
                 * ***/

                return tukHelper.FindAngleToTurn(pShip.myPosition, theWorld.waypointList[0]);
            }
            else
            {
                return 0.0f;
            }
        }
    }
}
