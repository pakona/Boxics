using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using BoxicsDataTypes;
using BoxicsGame.Textures;
using Microsoft.Xna.Framework;

namespace BoxicsGame
{
    public class BoxicsGame
    {
        SpriteBatch spriteBatch;
        public static GraphicsDevice GD;
        public static LevelData[] LevelsData;
        public static float ViewScale = 100f; // Farseer works with meters, we draw with pixels
        public static Viewport Viewport;

        private static Random random = new Random();
        public static Random Random
        {
            get { return random; }
        }

        public GraphicsDevice GraphicsDevice { get; set; }
        public ContentManager Content { get; set; }

        public BoxicsGame(GraphicsDevice graphicsDevice, ContentManager content)
        {
            GraphicsDevice = graphicsDevice;
            Content = content;
        }

        public void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            BoxicsGame.GD = GraphicsDevice;
            BoxicsGame.Viewport = GraphicsDevice.Viewport;
            BoxicsGame.LevelsData = Content.Load<LevelData[]>("Levels/Levels");
            BasicTextures.LoadTextures(Content);
            InstructionTextures.LoadTextures(Content);
            ParticleTextures.LoadTextures(Content);
            OtherTextures.LoadTextures(Content);
        }

        public void Reset()
        {
            ScreenManager.Instance.NavigateToGameplayScreen(0);
        }

        public void Update(float dt)
        {
            ScreenManager.Instance.CurrentScreen.Update(dt);
        }

        public void Draw()
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Matrix.CreateScale(ViewScale));
            ScreenManager.Instance.CurrentScreen.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
