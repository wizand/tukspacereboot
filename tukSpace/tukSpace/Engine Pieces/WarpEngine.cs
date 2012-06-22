using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace tukSpace
{
    class WarpEngine : Engine
    {
        /// <summary>
        ///  Just a wrapper for the Engine() constructor, all values
        ///  are passed along.
        /// </summary>
        /// <param name="currentShip">The ship we are attached to</param>
        /// <param name="velocity">Are we starting with a speed?</param>
        /// <param name="maxSpeed">What is our top speed?</param>
        public WarpEngine(Ship currentShip, int velocity, int maxSpeed)
            : base(currentShip, velocity, maxSpeed) { }

        /// <summary>
        /// Creates the instance of EngineController and sets up some initial variables
        /// including warp and impulse engine creation.
        /// </summary>
        /// <return>What kind of engine are we? Warp or impulse.</return>
        public override string ToString() { return "WARP"; }

        /// <summary>
        /// Warp acceleration is different than impulse. Acceleration occurs
        /// in instataneous steps as opposed to time (impulse).
        /// So lets just increase our speed by 1 warp factor
        /// </summary>
        /// <param name="thrustValue">Not used but kept for compatibility with Engine.</param>
        /// <param name="coef">Positive or negative thrust?</param>
        /// <returns>The new velocity for the engineController</returns>
        public override float ThrustByValue(float thrustValue, int coef)
        {
            return MathHelper.Clamp((float)currentShip.engineController.CurrentVelocity + (coef * VelocityStep), -1 * MAX_SPEED, MAX_SPEED);
        }

    }
}