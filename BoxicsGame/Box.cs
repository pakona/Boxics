using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using BoxicsGame.Textures;


namespace BoxicsGame
{
    class Box
    {
        public Body Body { get; private set; } 
        Texture2D texture;
        float size = 0.6f;
        static Random rand = new Random();

        public Vector2 Center { get { return Body.Position; } }

        public Box(World world, Vector2 center, Vector2 velocity, float scale)
        {
            size = size * scale;
            texture = scale < 2 ? OtherTextures.Get("Box") : OtherTextures.Get("BigBox");

            Body = BodyFactory.CreateRectangle(world, size, size, 1f);
            Body.Position = center;
            Body.BodyType = BodyType.Dynamic;
            Body.LinearVelocity = velocity;
            Body.AngularVelocity = (float)(rand.NextDouble() * 2 - 1) * (float)Math.PI / 10;
            Body.Restitution = 0.1f;

            Body.FixtureList[0].UserData = this;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            spriteBatch.Draw(texture, Body.Position, null, Color.Black, Body.Rotation, origin,
                size / texture.Width,
                SpriteEffects.None, 0f);
        }
    }
}
