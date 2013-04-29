using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using BoxicsDataTypes;
using BoxicsGame.Textures;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace BoxicsGame
{
    class Platform
    {
        Body body;
        float width;
        float height;
        Vector2 from;
        Vector2 to;
        Vector2 center;

        public Platform(World world, PlatformData platformData)
        {
            width = platformData.Width;
            height = platformData.Height;
            from = platformData.From;
            to = platformData.To;
            center = (to + from) / 2;

            body = BodyFactory.CreateRectangle(world, width, height, 1f);
            body.Position = platformData.Center;
            body.BodyType = platformData.IsStatic ? BodyType.Static : BodyType.Kinematic;
            body.LinearVelocity = platformData.IsStatic ? Vector2.Zero : Vector2.Normalize(to - from) * platformData.Speed;
        }

        public void Update()
        {
            if ((body.Position - center).LengthSquared() > (to - center).LengthSquared() && Vector2.Dot(body.Position - center, body.LinearVelocity) > 0)
            {
                body.LinearVelocity = -body.LinearVelocity;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2((float)BasicTextures.Pixel.Width / 2f, (float)BasicTextures.Pixel.Height / 2f);
            spriteBatch.Draw(BasicTextures.Pixel, body.Position, null, Color.Black, body.Rotation, origin, new Vector2(width, height),
                SpriteEffects.None, 0f);
        }
    }
}
