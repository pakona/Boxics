using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BoxicsGame.Textures
{
    static class BasicTextures
    {
        public static Texture2D Pixel;
        public static Texture2D DotSquare;
        public static Texture2D Circle;

        public static void LoadTextures(ContentManager Content)
        {
            Pixel = Content.Load<Texture2D>("Sprites/Textures/Pixel");
            DotSquare = Content.Load<Texture2D>("Sprites/Textures/DotSquare");
            Circle = Content.Load<Texture2D>("Sprites/Textures/Circle");
        }
    }
}
