using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using BoxicsDataTypes;
using BoxicsGame.Textures;

namespace BoxicsGame
{
    class GameLevel
    {
        public int Id { get; private set; }
        public List<BoxArea> BoxAreas { get; private set; }
        List<Platform> Platforms { get; set; }
        List<Instruction> Instructions { get; set; }
        List<Speedwalk> Speedwalks { get; set; }
        List<PropulsivePlatform> ElasticSprings { get; set; }
        List<SwingPlatform> Swings { get; set; }
        List<Fan> Fans { get; set; }
        public Vector2 CompletionSquarePosition { get; private set; }
        public Vector2 CompletionSquareCenter { get; private set; }
        float CompletionSquareSize;
        public readonly float Precision;

        public GameLevel(int id, List<BoxArea> boxAreas, List<Platform> platforms, List<Instruction> instructions, 
            List<Speedwalk> speedwalks, List<PropulsivePlatform> elasticSprings,
            List<SwingPlatform> swings, List<Fan> fans,
            Vector2 completionSquarePosition, float completionSquareSize)
        {
            Id = id;
            BoxAreas = boxAreas;
            Platforms = platforms;
            Instructions = instructions;
            Speedwalks = speedwalks;
            ElasticSprings = elasticSprings;
            Swings = swings;
            Fans = fans;
            CompletionSquarePosition = completionSquarePosition;
            CompletionSquareCenter = new Vector2(completionSquarePosition.X + completionSquareSize / 2, 
                completionSquarePosition.Y + completionSquareSize/2);
            CompletionSquareSize = completionSquareSize;
            Precision = completionSquareSize / 3;
        }

        public void Update()
        {
            foreach (Platform platform in Platforms)
            {
                platform.Update();
            }

            foreach (Speedwalk sw in Speedwalks)
            {
                sw.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (BoxArea boxArea in BoxAreas)
            {
                boxArea.Draw(spriteBatch);
            }

            foreach (Instruction instruction in Instructions)
            {
                instruction.Draw(spriteBatch);
            }

            spriteBatch.Draw(BasicTextures.DotSquare, CompletionSquarePosition, null, Color.White, 0.0f, Vector2.Zero, 
                CompletionSquareSize / BasicTextures.DotSquare.Width, 
                SpriteEffects.None, 0.0f);

            foreach (Platform platform in Platforms)
            {
                platform.Draw(spriteBatch);
            }

            foreach (Speedwalk sw in Speedwalks)
            {
                sw.Draw(spriteBatch);
            }

            foreach (PropulsivePlatform e in ElasticSprings)
            {
                e.Draw(spriteBatch);
            }

            foreach(SwingPlatform sw in Swings)
            {
                sw.Draw(spriteBatch);
            }

            foreach (Fan fan in Fans)
            {
                fan.Draw(spriteBatch);
            }

            foreach (BoxArea boxArea in BoxAreas)
            {
                if (boxArea.BoxAlive != null)
                {
                    boxArea.BoxAlive.Draw(spriteBatch);
                }
            }
        }
    }
}
