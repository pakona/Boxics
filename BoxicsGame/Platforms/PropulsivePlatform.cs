using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BoxicsGame.Textures;
using FarseerPhysics.Dynamics.Joints;
using BoxicsDataTypes;

namespace BoxicsGame
{
    class PropulsivePlatform
    {
        Body body;
        Vector2 Impulse;
        float width;
        float height;
        public PropulsivePlatform(World world, PropulsivePlatformData propulsivePlatformData)
        {
            width = propulsivePlatformData.Width;
            height = propulsivePlatformData.Height;

            body = BodyFactory.CreateRectangle(world, width, height, 1.0f);
            body.BodyType = BodyType.Kinematic;
            body.Position = propulsivePlatformData.Center;
            body.Rotation = propulsivePlatformData.Rotation;
            body.OnCollision += new OnCollisionEventHandler(OnCollision);

            Impulse = Vector2.Transform(new Vector2(0, -Math.Abs(propulsivePlatformData.Force)), Matrix.CreateRotationZ(propulsivePlatformData.Rotation));
        }

        bool OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            Box box = (Box)fixtureB.UserData;
            box.Body.ApplyLinearImpulse(Impulse);
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2((float)BasicTextures.Pixel.Width / 2, (float)BasicTextures.Pixel.Height / 2);
            spriteBatch.Draw(BasicTextures.Pixel, body.Position, null, Color.Black, body.Rotation, origin,
                new Vector2(width, height),
                SpriteEffects.None, 0f);

            spriteBatch.Draw(BasicTextures.Pixel, body.Position + new Vector2(0.0f, height / 2 + 0.05f / 2), null, Color.Maroon, body.Rotation, origin,
                new Vector2(width * 0.1f, 0.05f),
                SpriteEffects.None, 0f);
        }
    }
}
