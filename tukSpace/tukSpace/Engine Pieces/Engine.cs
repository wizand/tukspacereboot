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
    class Engine
    {

        protected float MAX_SPEED;
        public float VelocityStep { get; private set; }
        protected Ship currentShip;

        /// <summary>
        /// Creates an Engine and sets up some base variables
        /// </summary>
        /// <param name="currentShip">The ship we are attached to.</param>
        /// <param name="velocityStep">When we accelerate, how much should it be by?</param>
        /// <param name="MAX_SPEED">What is our top speed?</param>
        public Engine(Ship currentShip, float velocityStep, float MAX_SPEED)
        {
            this.currentShip = currentShip;
            this.VelocityStep = velocityStep;
            this.MAX_SPEED = MAX_SPEED;
        }

        /// <summary>
        /// Determines the new velocity which is used by the engine controller.
        /// Note: Warp and Impulse work differently. Check their methods for
        /// differences.
        /// </summary>
        /// <param name="thrustValue">The amount of thrust to apply.</param>
        /// <param name="coef">Positive or negative thrust?</param>
        /// <returns></returns>
        public virtual float ThrustByValue(float thrustValue, int coef)
        {
            return 0f;
        }
    }
}
