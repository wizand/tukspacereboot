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
    class BeamController
    {
        private Ship theShip;

        public List<Vector2> TargetList;
        private int targetIndex;

        private double timer;

        private Vector2 beamPos;
        public Texture2D beamTexture;

        public bool CurrentlyFiring;
        public int FireMode; //0 = auto target list thing, 1 = just one target
        public Vector2 singleFireTarget;

        public BeamController(Ship theShip, Texture2D beamTexture)
        {
            this.theShip = theShip;
            this.beamTexture = beamTexture;
            this.CurrentlyFiring = false;
            TargetList = new List<Vector2>();
            targetIndex = 0;
            timer = 0.0f;
            FireMode = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if ((CurrentlyFiring) && (targetIndex < TargetList.Count) && (FireMode == 0))
            {
                float radians = (float)Math.Atan2(TargetList[targetIndex].Y - theShip.myPosition.Y + 10, TargetList[targetIndex].X - theShip.myPosition.X + 10);

                beamPos.X = theShip.myPosition.X + 5;
                beamPos.Y = theShip.myPosition.Y + 3;
                spriteBatch.Draw(beamTexture, beamPos, null, Color.White, radians, new Vector2(0, 5), 1.0f, SpriteEffects.None, 0f);
            }
            else if ((CurrentlyFiring) && (FireMode == 1))
            {
                beamPos.X = theShip.myPosition.X + 5;
                beamPos.Y = theShip.myPosition.Y + 3;
                float radians = (float)Math.Atan2(singleFireTarget.Y - theShip.myPosition.Y + 10, singleFireTarget.X - theShip.myPosition.X + 10);
                //spriteBatch.Draw(this.beamTexture, beamPos, null, Color.White, radians, new Vector2(0, 5), 1.0f, SpriteEffects.None, 0f);
                spriteBatch.Draw(beamTexture, beamPos, Color.White);
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
            timer = 0;
            targetIndex = 0;
            CurrentlyFiring = false;
        }

    }
}
