using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Joints;
using BoxicsGame.Textures;
using BoxicsDataTypes;

namespace BoxicsGame
{
    class SwingPlatform
    {
        float width;
        float height;
        Body body, anchor;

        public SwingPlatform(World world, SwingPlatformData swingPlatformData) 
        {
            width = swingPlatformData.Width;
            height = swingPlatformData.Height;

            body = BodyFactory.CreateRectangle(world, width, height, 1.0f);
            body.Position = swingPlatformData.Center;
            body.BodyType = BodyType.Dynamic;
            anchor = BodyFactory.CreateCircle(world, 0.1f, 1.0f);
            anchor.Position = swingPlatformData.Center;
            anchor.BodyType = BodyType.Static;

            RevoluteJoint revoluteJoint = new RevoluteJoint(body, anchor, new Vector2(0.0f, 0.0f), new Vector2(0.0f, 0.0f));
            revoluteJoint.LowerLimit = -swingPlatformData.MaxAngle;
            revoluteJoint.UpperLimit = swingPlatformData.MaxAngle;
            revoluteJoint.LimitEnabled = true;
            revoluteJoint.MaxMotorTorque = 10.0f;
            revoluteJoint.MotorSpeed = 0.0f;
            revoluteJoint.MotorEnabled = true;
            world.AddJoint(revoluteJoint);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2((float)BasicTextures.Pixel.Width / 2, (float)BasicTextures.Pixel.Height / 2);
            spriteBatch.Draw(BasicTextures.Pixel, body.Position, null, Color.Black, body.Rotation, origin, new Vector2(width, height),
                SpriteEffects.None, 0.0f);

            origin = new Vector2((float)BasicTextures.Circle.Width / 2, (float)BasicTextures.Circle.Height / 2);
            spriteBatch.Draw(BasicTextures.Circle, anchor.Position, null, Color.Maroon, anchor.Rotation, origin,
                0.1f / BasicTextures.Circle.Width,
                SpriteEffects.None, 0.0f);
        }
    }
}
