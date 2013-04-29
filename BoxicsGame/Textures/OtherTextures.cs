using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BoxicsGame.Textures
{
    class OtherTextures
    {
        static Dictionary<string, Texture2D> DB;

        public static void LoadTextures(ContentManager Content)
        {
            DB = new Dictionary<string, Texture2D>(4);
            DB["Box"] = Content.Load<Texture2D>("Sprites/Others/Box");
            DB["BigBox"] = Content.Load<Texture2D>("Sprites/Others/Box");
            DB["Propulsor"] = Content.Load<Texture2D>("Sprites/Others/Propulsor");
        }

        public static Texture2D Get(string key)
        {
            return DB[key];
        }
    }
}
