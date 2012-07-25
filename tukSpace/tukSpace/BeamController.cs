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
    public class BeamController
    {
        private Ship theShip;

        public List<Vector2> TargetList;
        private int targetIndex;

        private double timer;

        public Vector2 beamPos;
        public Texture2D beamTexture;

        public bool CurrentlyFiring;
        public int FireMode; //0 = auto target list thing, 1 = just one target
        public Vector2 singleFireTarget;

        Vector2 beamScale = Vector2.Zero;
        public float beamRotation;

        public Rectangle collisionRectangle;

        public BeamController(Ship theShip, Texture2D beamTexture)
        {
            this.theShip = theShip;
            this.beamTexture = beamTexture;
            this.CurrentlyFiring = false;
            TargetList = new List<Vector2>();
            targetIndex = 0;
            timer = 0.0f;
            FireMode = 0;
            collisionRectangle = new Rectangle(0, 0, 0, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentlyFiring)
            {
                 beamRotation = 0.0f;
                Vector2 targetPosition = Vector2.Zero;

            if ((targetIndex < TargetList.Count) && (FireMode == 0))
            {
                targetPosition = TargetList[targetIndex];
            }
            else if (FireMode == 1)
            {
                targetPosition = singleFireTarget;
            }
                beamRotation = (float)Math.Atan2(targetPosition.Y - theShip.myPosition.Y + 10, targetPosition.X - theShip.myPosition.X + 10);
                beamPos.X = theShip.myPosition.X + 5;
                beamPos.Y = theShip.myPosition.Y + 3;
                //beam is a bit shy of cursor
                beamScale.X = Vector2.Distance(targetPosition, theShip.myPosition) / beamTexture.Width;
                beamScale.Y = 1f;
            spriteBatch.Draw(this.beamTexture, beamPos, null, Color.White, beamRotation, new Vector2(0, 5), beamScale, SpriteEffects.None, 0f);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (CurrentlyFiring)
            {
                if ((targetIndex >= TargetList.Count) || (TargetList[targetIndex] == null) )
                {
                    EndFiring();
                }
                else
                {
                    if (timer < 1.0)
                    {
                        timer += gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (timer >= 1.0)
                    {
                        if ((targetIndex <= (TargetList.Count - 1)))
                        {
                            timer = 0;
                            targetIndex++;
                        }
                        else
                        {
                            EndFiring();
                        }
                    }
                }
                //beam collision box goes here.
                collisionRectangle = new Rectangle((int)beamPos.X, (int)beamPos.Y, (int)beamScale.X, (int)beamScale.Y);
            }

        }

        public void BeginFiring()
        {
            if (TargetList.Count == 0)
            {
                // no reason to fire
            }
            else
            {
                this.CurrentlyFiring = true;

            }
        }

        public void ManualFireStart(Vector2 target)
        {
            this.CurrentlyFiring = true;
            FireMode = 1;
            singleFireTarget = target;
        }

        public void ManualFireStop()
        {
            this.CurrentlyFiring = false;
            FireMode = 0;
        }

        public void EndFiring()
        {
            beamScale = Vector2.Zero;
            timer = 0;
            targetIndex = 0;
            CurrentlyFiring = false;
        }

    }
}
