using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BoxicsGame.Textures
{
    static class ParticleTextures
    {
        static Dictionary<string, Texture2D> DB;

        public static void LoadTextures(ContentManager Content)
        {
            DB = new Dictionary<string, Texture2D>(4);
            DB["Explosion"] = Content.Load<Texture2D>("Sprites/Particles/Explosion");
            DB["Smoke"] = Content.Load<Texture2D>("Sprites/Particles/Smoke");
        }

        public static Texture2D Get(string key)
        {
            return DB[key];
        }
    }
}
