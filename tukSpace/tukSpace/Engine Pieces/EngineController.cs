using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tukSpace
{
    class EngineController
    {
        private WarpEngine warp;
        private ImpulseEngine impulse;
        private Engine currentEngine;
        private Ship currentShip;

        public float CurrentVelocity { get; private set; }
        private float lastVelocity;

        /// <summary>
        /// Creates the instance of EngineController and sets up some initial variables
        /// including warp and impulse engine creation.
        /// </summary>
        /// <param name="currentShip">The ship we are attached to.</param>
        public EngineController(Ship currentShip)
        {
            //for now the Thrust methods are made in the warp/impulse Engine classes
            //but im thinking that maybe they should be implemented here and just fetch
            //the needed attributes from those classes instead of carrying around the ship object

            Console.WriteLine("DEBUG: EngineController constructor called.");
            warp = new WarpEngine(currentShip, 5, 20);
            impulse = new ImpulseEngine(currentShip, 1, 5);

            this.currentShip = currentShip;
            this.currentEngine = impulse;
        }

        /// <summary>
        /// Toggles the current engine to impulse and resumes last impulse velocity
        /// (either 0, or the last velocity before warp was engaged).
        /// </summary>
        public void EngageImpulseDrive()
        {
            currentEngine = impulse;
            CurrentVelocity = lastVelocity;
        }

        /// <summary>
        /// Toggles the current engine to warp and stores the last impulse velocity.
        /// The engine immediately accelerates to warp factor 1.
        /// </summary>
        public void EngageWarpDrive()
        {
            currentEngine = warp;
            lastVelocity = CurrentVelocity;
            CurrentVelocity = warp.VelocityStep;
        }

        /// <summary>
        /// Are we using warp drive?
        /// </summary>
        /// <returns>True if warp is being used; False is impulse.</returns>
        public bool WarpOn()
        {
            if (currentEngine == warp) return true; 
             return false; 
        }

        /// <summary>
        /// Switches to impulse from warp or warp from impulse.
        /// </summary>
        public void ChangeWarpState()
        {

            if (currentEngine == warp) { EngageImpulseDrive(); }
            else { EngageWarpDrive(); }

        }

        /// <summary>
        /// Wrapper to call the current engine's ThrustByValue().
        /// The return value becomes our new velocity.
        /// </summary>
        /// <param name="thrustValue">How much is thrust being increased by?</param>
        /// <param name="coef">Positive or negative thrust?</param>
        public void ThrustByValue(float thrustValue, int coef)
        {
            CurrentVelocity = currentEngine.ThrustByValue(thrustValue, coef);
        }

        /// <summary>
        /// Drops out of warp and brings us to a complete stop.
        /// </summary>
        public void AllStop()
        {
            if (WarpOn()) EngageImpulseDrive();
            CurrentVelocity = 0;
        }

        /// <summary>
        /// Returns the current engine.toString()
        /// </summary>
        /// <returns>Name of current engine.</returns>
        public override string ToString()
        {
            return currentEngine.ToString();
        }

    }
}
