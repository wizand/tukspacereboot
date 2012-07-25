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
    public enum SensorMode { SHORT, MEDIUM, LONG };
    public class Ship
    {
        //some constants
        //public Vector2 ROTATION_POINT = new Vector2(25,7); //origin of rotation
        public Vector2 ROTATION_POINT = Vector2.Zero;

        public int myID { get; private set; } //our id for networking mainly

        //changed the myTexture to public so it can be used in drawing individual enemies
        public Texture2D myTexture;
        public Vector2 myPosition;
        public bool visible; //if true, ship will be drawn to screen (whether or not ship is on it)

        private string textureName;

        public float rotationAngle { get; private set;} //in radians
        private float rotationStep; //in degrees? 

        //Handles the engine and movement
        public EngineController engineController { get; private set;}

        public BeamController beamController { get; private set; }

        //hacking in for phaser hit detection
        public float shieldPercentage = 100;
        public bool Destroyed = false;

        public bool beamOut;
        private Texture2D beamTexture;

        public bool shieldsUp;
        public Texture2D shieldOverlay;

        public BoundingBox myBox;
        public Sector myLocation;
        public Rectangle collisionRectangle;

        public SensorMode mySensorMode;

        public Ship()
        {
            myPosition = new Vector2(500, 500);
            rotationStep = 30.0f;
            rotationAngle = 0.0f;
            beamOut = false;
            shieldsUp = false;
            engineController = new EngineController(this);
            beamController = new BeamController(this, beamTexture);
            
        }

        //public EngineController GetEngineController() { return engineController; }

        //Making few constructors for faster use. First one is to make up basic ship with 
        //(x,Y) given as parameters

        public Ship(int posX, int posY, bool shields, float rotationAngle, bool visible, Sector myLocation)
        {
            System.Console.WriteLine("DEBUG: Ship constructor with texture option accessed with no texture name, using default.");
            myPosition = new Vector2(posX, posY);
            rotationStep = 30.0f;
            this.rotationAngle = rotationAngle;
            beamOut = false;
            shieldsUp = shields;
            engineController = new EngineController(this);
            textureName = "default";
            this.visible = visible;
            this.myLocation = myLocation;
            beamController = new BeamController(this, beamTexture);

        }

        //This one is to choose texture on the construction phase
        public Ship(int posX, int posY, bool shields, string texture, bool visible, Sector myLocation)
        {
            System.Console.WriteLine("DEBUG: Ship constructor with texture option accessed with texture name " + texture);
            myPosition = new Vector2(posX, posY);
            rotationStep = 30.0f;
            rotationAngle = 0.0f;
            beamOut = false;
            shieldsUp = shields;
            engineController = new EngineController(this);
            textureName = texture;
            this.visible = visible;
            this.myLocation = myLocation;
            beamController = new BeamController(this, beamTexture); 
            
        }

        //Overloading the LoadContent -method for the new constructor use. 
        //Setting the enterprise texture as default.
        public void LoadContent(ContentManager content)
        {

            if (Equals(textureName, "default"))
            {
                System.Console.WriteLine("DEBUG: LoadContent myTexture = " + textureName);
                myTexture = content.Load<Texture2D>("enterprise90t");
            }
            else
            {
                System.Console.WriteLine("DEBUG: LoadContent myTexture = " + textureName);
                myTexture = content.Load<Texture2D>(textureName);
            }
            //Rectangle collisionRectangle = new Rectangle((int)myPosition.X, (int)myPosition.Y, myTexture.Width, myTexture.Height);
            beamTexture = content.Load<Texture2D>("phaser");
            shieldOverlay = content.Load<Texture2D>("shieldOverlay");
            Rectangle collisionRectangle = new Rectangle((int)myPosition.X, (int)myPosition.Y, myTexture.Width, myTexture.Height);

            mySensorMode = SensorMode.SHORT;
        }

        public void LoadContent(ContentManager content, String texture)
        {
            myTexture = content.Load<Texture2D>(texture);
            beamTexture = content.Load<Texture2D>("phaser");
            shieldOverlay = content.Load<Texture2D>("shieldOverlay");
            beamController.beamTexture = this.beamTexture; //quick hack
            //just hacked in
            //collsionRectangle = new RotatedRectangle(new Rectangle((int)myPosition.X, (int)myPosition.Y, myTexture.Width, myTexture.Height), rotationAngle);
            //collsionRectangle.Origin = new Vector2(27, 11);
            Rectangle collisionRectangle = new Rectangle((int)myPosition.X, (int)myPosition.Y, myTexture.Width, myTexture.Height);
        }

        //rotates the ship by a given value modified by rotationStep. 
        //value is modfied by handleInput() via HelmScreen
        public void RotateByValue(float rotationValue, int coef)
        {
            float newRotation = (float)(rotationStep * rotationValue) * coef;
            newRotation += MathHelper.ToDegrees(rotationAngle);
            if (newRotation >= 360)
            {
                newRotation -= 360;
            }
            else if (newRotation <= -360)
            {
                newRotation += 360;
            }

            if (newRotation < 0)
            {
                newRotation += 360;
            }

            rotationAngle = (float)(Math.Round(MathHelper.ToRadians(newRotation)));
            rotationAngle = MathHelper.ToRadians(newRotation);
        }

        //updates ship position and associated components
        public void Update(GameTime gameTime)
        {
            if (shieldPercentage <= 0)
            {
                Destroyed = true;
                return;
            }
            myPosition.X += (float)(engineController.CurrentVelocity * Math.Cos(rotationAngle));
            myPosition.Y += (float)(engineController.CurrentVelocity * Math.Sin(rotationAngle));
            myBox = new BoundingBox(new Vector3(myPosition.X, myPosition.Y, 0),
                        new Vector3(myPosition.X + myTexture.Width, myPosition.Y + myTexture.Height, 0));

            //update collision rectangle
            //SOMETHING IS WRONG, BOX ISNT CLOSE TO EXACT. TOO SMALL MAYBE????????????????????????????????

            collisionRectangle = new Rectangle((int)myPosition.X, (int)myPosition.Y, myTexture.Width, myTexture.Height);

            beamController.Update(gameTime);
            
        }

        //27,11 is the center of sprite saucer. this allows for proper rotation along a fixed point
        //instead of top left corner
        //no offset atm for collision testing
        //when updating, don't forget shield
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Destroyed)
                return;
            DrawShield(spriteBatch);
            spriteBatch.Draw(myTexture, myPosition, null, Color.White, rotationAngle,
                    ROTATION_POINT, 1.0f, SpriteEffects.None, 0f);

            beamController.Draw(spriteBatch);
        }

        //shield is misaligned.
        public void DrawShield(SpriteBatch spriteBatch)
        {
            if (shieldsUp == true)
            {
                spriteBatch.Draw(shieldOverlay, myPosition, null, Color.White, rotationAngle,
                                 ROTATION_POINT, 1.0f, SpriteEffects.None, 0f);
            }
        }

        public void ThrustByValue(float thrustValue, int coef)
        {
            engineController.ThrustByValue(thrustValue, coef);
        }

        public float GetRotation()
        {
            return MathHelper.ToDegrees(rotationAngle);

        }

        public void SetSensorMode(SensorMode newMode)
        {
            mySensorMode = newMode;
        }

        public Vector2 GetSensorRange()
        {
            if (mySensorMode == SensorMode.SHORT)
            {
                return new Vector2(0f, 650f);
            }
            else if (mySensorMode == SensorMode.MEDIUM)
            {
                return new Vector2(650f, 1300f);
            }
            else
            {
                return new Vector2(1300f, 2000);
            }
        }
    }
}
