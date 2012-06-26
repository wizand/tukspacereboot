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

namespace tukSpace.Scenarios
{
    class Scenario
    {
        public GraphicsDevice graphics;
        //waypoint system
        public Texture2D waypointText;
        public List<Vector2> waypointList = new List<Vector2>();

        //firing system
        public Texture2D firepointText;
        public List<Vector2> firepointList = new List<Vector2>();
        public Screen curScreen { get; protected set; }
        public Scenario(GraphicsDevice gr)
        {
            graphics = gr;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public virtual void Initialize()
        {
        }
        public virtual void Update(GameTime gameTime)
        {
        }
        public virtual void LoadContent(ContentManager Content)
        {
        }
    }

}
