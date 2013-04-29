using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BoxicsDataTypes;
using BoxicsGame.Textures;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace BoxicsGame
{
    class BoxArea
    {
        public Box BoxAlive { get; private set; }
        List<Box> DyingBoxes { get; set; }
        World world;
        Vector2 position;
        float width;
        float height;
        float boxScale;
        Rectangle boundingBox;

        public BoxArea(World world, BoxAreaData boxAreaData)
        {
            this.world = world;
            this.position = boxAreaData.Position;
            this.width = boxAreaData.Width;
            this.height = boxAreaData.Height;
            this.boxScale = boxAreaData.BoxScale;
            this.DyingBoxes = new List<Box>();

            boundingBox = new Rectangle((int)(BoxicsGame.ViewScale * position.X),
                (int)(BoxicsGame.ViewScale * position.Y),
                (int)(BoxicsGame.ViewScale * width),
                (int)(BoxicsGame.ViewScale * height));
        }

        public bool Intersect(Vector2 vector2)
        {
            return boundingBox.Intersects(new Rectangle((int)(BoxicsGame.ViewScale * vector2.X), (int)(BoxicsGame.ViewScale * vector2.Y), 1, 1));
        }

        public void RecreateBoxAt(Vector2 position, Vector2 velocity)
        {
            if (BoxAlive != null)
            {
                world.RemoveBody(BoxAlive.Body);
                DyingBoxes.Add(BoxAlive);
            }
            BoxAlive = new Box(world, position, velocity, boxScale);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BasicTextures.Pixel, position, null, Color.DarkOliveGreen, 0.0f,
                Vector2.Zero, new Vector2(width, height), SpriteEffects.None, 0.0f);
        }
    }
}
