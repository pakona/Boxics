using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BoxicsGame.Textures;
using FarseerPhysics.Factories;
using BoxicsDataTypes;

namespace BoxicsGame
{
    class Fan
    {
        Body body;
        float width = 0.1f, height = 0.4f;
        public Fan(World world, FanData fanData)
        {
            body = BodyFactory.CreateRectangle(world, width, height, 1.0f);
            body.Position = fanData.Center;
            body.BodyType = BodyType.Static;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2((float)BasicTextures.Pixel.Width / 2, (float)BasicTextures.Pixel.Height / 2);
            spriteBatch.Draw(BasicTextures.Pixel, body.Position, null, Color.Black, body.Rotation, origin, new Vector2(width, height),
                SpriteEffects.None, 0.0f);

            spriteBatch.Draw(BasicTextures.Pixel, body.Position + new Vector2(-width, height * 0.4f), null, Color.Maroon, body.Rotation, 
                origin, new Vector2(width, height * 0.75f), SpriteEffects.None, 0.0f);

            spriteBatch.Draw(BasicTextures.Pixel, body.Position + new Vector2(-width, height * 0.4f + height * 0.75f * 0.5f), null, Color.Black, body.Rotation,
                origin, new Vector2(height/2, width/2), SpriteEffects.None, 0.0f);
        }
    }
}
