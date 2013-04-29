using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using BoxicsDataTypes;
using BoxicsGame.Textures;

namespace BoxicsGame
{
    class Instruction
    {
        Texture2D sprite;
        Vector2 position;
        float width;
        float height;

        public Instruction(InstructionData instructionData)
        {
            sprite = InstructionTextures.Get(instructionData.Name);
            position = instructionData.Position;
            width = instructionData.Width;
            height = instructionData.Height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,
                position,
                null,
                Color.White,
                0.0f,
                Vector2.Zero,
                new Vector2(width / sprite.Width, height / sprite.Height),
                SpriteEffects.None,
                0.0f);
        }
    }
}
