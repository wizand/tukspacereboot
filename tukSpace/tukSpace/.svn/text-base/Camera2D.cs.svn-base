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

    //taken from http://www.david-amador.com/2009/10/xna-camera-2d-with-zoom-and-rotation/
    class Camera2D
    {
        protected float _zoom; // Camera Zoom
        public Matrix _transform; // Matrix Transform
        public Vector2 _pos; // Camera Position
        protected float _rotation; // Camera Rotation

        protected Viewport viewport; //current viewport

        public Camera2D(Viewport viewport)
        {
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
            this.viewport = viewport;
        }

        // Sets and gets zoom
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }
        // Get set position
        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; _pos.X -= 12; } //ship offset
        }

        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            _transform = Matrix.Identity *     // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                          Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));


            //viewport.scale(1, -1) * translate(viewport.Width/2, viewport.Height/2)
              //  _transform = Matrix.Identity * Matrix.CreateScale(new Vector3(1,-1,0)) * Matrix.CreateTranslation(new Vector3(viewport.Width/2, viewport.Height/2, 0));
            return _transform;
        }

        /// <summary>
        /// Converts a location in local space to world space.
        /// It is mainly used to get mouse coordinates.
        /// </summary>
        /// <param name="position">Local space location</param>.
        public Vector2 ToWorldLocation(Vector2 position)
        {
            return Vector2.Transform(position, Matrix.Invert(_transform));
        }

        /// <summary>
        /// Converts a location in world space to local space.
        /// The inverse of ToWorldLocation().
        /// </summary>
        /// <param name="position">World space location</param>
        public Vector2 ToLocalLocation(Vector2 position)
        {
            return Vector2.Transform(position, _transform);
        }
    }
}
