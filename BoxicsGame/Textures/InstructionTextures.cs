using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BoxicsGame.Textures
{
    static class InstructionTextures
    {
        static Dictionary<string, Texture2D> DB;

        public static void LoadTextures(ContentManager Content)
        {
            DB = new Dictionary<string, Texture2D>(4);
            DB["InTheBox"] = Content.Load<Texture2D>("Sprites/Instructions/InTheBox");
            DB["PhysicsYourFriend"] = Content.Load<Texture2D>("Sprites/Instructions/PhysicsYourFriend");
            DB["GreatPowers"] = Content.Load<Texture2D>("Sprites/Instructions/GreatPowers");
            DB["CallingBud"] = Content.Load<Texture2D>("Sprites/Instructions/CallingBud");
            DB["InTheGreen"] = Content.Load<Texture2D>("Sprites/Instructions/InTheGreen");
        }

        public static Texture2D Get(string key)
        {
            return DB[key];
        }
    }
}
