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
    //a collection of helper methods
    static class tukHelper
    {
        /// <summary>
        /// Returns the distance between two objects in pixels
        /// </summary>
        /// <param name="objDistance">The location of the object.</param>
        /// <param name="playerDistance">The location of the player.</param>
        /// <returns>The distance between the two given vectors in pixels.</returns>
        public static float determineDistance(Vector2 objDistance, Vector2 playerDistance)
        {
            Vector2 diffVect = objDistance - playerDistance;
            float distance = diffVect.Length();
            return distance;
        }

        //how far apart are the angles?
        //VectorA is always ship.
        public static float AngleBetweenTwoVectors(Vector2 shipPos, Vector2 obj, float rotAngle)
        {
            //float m = (obj.Y - shipPos.Y) / (obj.X - shipPos.X);
             float tempAngle = (float)Math.Atan2(obj.Y - shipPos.Y, obj.X - obj.Y);

             tempAngle = MathHelper.ToDegrees(tempAngle - rotAngle);

             if (tempAngle >= 360)
             {
                 tempAngle -= 360;
             }
             else if (tempAngle <= -360)
             {
                 tempAngle += 360;
             }

             if (tempAngle < 0)
             {
                 tempAngle += 360;
             }
             return tempAngle;
        }



        /// //////////////////////////////////////
        /// PETE'S VECTOR STUFF //////////////////
        /// //////////////////////////////////////
        /// 

        // return the magnitude of the vector.
        public static double Magnitude(Vector2 vector) {
            if (vector.X != 0 && vector.Y != 0)
                {
                    double temp = Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));

                    Console.WriteLine("Calculating vector magnitude: " + temp);
                    if (temp > 0) return temp;
                    return temp * -1;
                }
            return 0;
        }


        public static double DotProduct(Vector2 v1, Vector2 v2) {

            double temp = v1.X * v2.X;
            temp = temp + (v1.Y * v2.Y);
            Console.WriteLine("Calculating v1·v2: " + temp);
            return temp;
        }

        public static double AngleBetween(Vector2 intersection, Vector2 v1End, Vector2 v2End) {

            //to components
            Vector2 v1 = new Vector2(v1End.X - intersection.X, v1End.Y - intersection.Y);
            Vector2 v2 = new Vector2(v2End.X - intersection.X, v2End.Y - intersection.Y);
            
            //debug stuff
            Console.WriteLine("Calculating v1 components: (" + v1.X + " " + v1.Y + ")");
            Console.WriteLine("Calculating v2 components: (" + v2.X + " " + v2.Y + ")");

            double tmp1 = Magnitude(v1) * Magnitude(v2);
            Console.WriteLine("mag1*mag2 " +  tmp1);
            tmp1 = DotProduct(v1,v2) / tmp1;
            Console.WriteLine("dp / mag1*mag2 " + tmp1);

            // arccos(A·B / |A||B|) * 180/Pi
            return Math.Acos(DotProduct(v1, v2)/(Magnitude(v1)*Magnitude(v2)) ) * (180/MathHelper.Pi);
        
        }


        //////////////////////////////////////////////////
        //////////////New Angle Code/////////////////////

        public static float FindAngleToTurn(Vector2 shipPos, Vector2 targetPos)
        {
            float radians = (float)Math.Atan2(targetPos.Y - shipPos.Y, targetPos.X - shipPos.X);
            return MathHelper.ToDegrees(radians);
        }
    }
}
