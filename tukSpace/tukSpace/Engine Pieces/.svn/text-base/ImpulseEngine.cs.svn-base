using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace tukSpace
{
    class ImpulseEngine : Engine
    {
        /// <summary>
        ///  Just a wrapper for the Engine() constructor, all values
        ///  are passed along.
        /// </summary>
        /// <param name="currentShip">The ship we are attached to</param>
        /// <param name="velocity">Are we starting with a speed?</param>
        /// <param name="maxSpeed">What is our top speed?</param>
        public ImpulseEngine(Ship currentShip, int velocity, int maxSpeed)
            : base(currentShip, velocity, maxSpeed) { }

        /// <summary>
        /// Creates the instance of EngineController and sets up some initial variables
        /// including warp and impulse engine creation.
        /// </summary>
        /// <return>What kind of engine are we? Warp or impulse.</return>
        public override string ToString() { return "IMPULSE"; }

        /// <summary>
        /// Apply thrust by adding the VelocityStep value modified by the thrustValue
        /// and coef. A check is also done to ensure MAX_SPEED isn't exceeded.
        /// </summary>
        /// <param name="thrustValue">How much thrust should we be applying?</param>
        /// <param name="coef">Positive or negative thrust?</param>
        /// <returns>The new velocity for the engineController</returns>
        public override float ThrustByValue(float thrustValue, int coef)
        {
            float newVelocityDelta = VelocityStep * coef * thrustValue;
            return MathHelper.Clamp(currentShip.engineController.CurrentVelocity
                                    + newVelocityDelta, -1 * MAX_SPEED, MAX_SPEED); ;
        }
    }
}



