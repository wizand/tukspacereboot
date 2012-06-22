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
    class ShieldController
    {
        //raise shields
        //lower shields
        //transfer power from one shield to another
        //extend shields
        //change shields frequency?

        public Shield[] theShields { get; private set;} //foward, starboard, port, aft

        public ShieldController(Texture2D verticalShieldTexture, Texture2D horizontalShieldTexture)
        {
            this.theShields = new Shield[] { new Shield(verticalShieldTexture, new Vector2(0,0)),
                                             new Shield(horizontalShieldTexture, new Vector2(0,0)),
                                             new Shield(horizontalShieldTexture, new Vector2(0,0)),
                                             new Shield(verticalShieldTexture, new Vector2(0,0))};
        }

        public void RaiseShields()
        {
            foreach (Shield shield in theShields)
            {
                shield.Raised = true;
            }
        }

        public void LowerShields()
        {
            foreach (Shield shield in theShields)
            {
                shield.Raised = false;
            }
        }

        public void Update()
        {
            foreach (Shield shield in theShields)
            {
                shield.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 shipPos, float rAngle)
        {
            foreach (Shield shield in theShields)
            {
                if (shield.Raised)
                    shield.Draw(spriteBatch, shipPos, rAngle);
            }
        }

        /// <summary>
        /// Loops through all 4 shields to get their efficency
        /// then returns the average.
        /// </summary>
        /// <returns>Average efficency of shields.</returns>
        public float AverageEfficency()
        {
            float efficency = 0.0f;

            foreach (Shield shield in theShields)
            {
                efficency += shield.Efficency;
            }

            return efficency / 4.0f;
        }

        /// <summary>
        /// Transfers powerAmount from shieldOne to 
        /// shieldTwo.
        /// </summary>
        /// <param name="shieldOne">The shield who is transferring power.</param>
        /// <param name="shieldTwo">The shield who is receiving power.</param>
        /// <param name="powerAmount">The amount of power to transfer.</param>
        public void TransferPower(int shieldOne, int shieldTwo, float powerAmount)
        {
            theShields[shieldOne].AvailablePower -= powerAmount;
            theShields[shieldTwo].AvailablePower += powerAmount;
        }
    }
}
