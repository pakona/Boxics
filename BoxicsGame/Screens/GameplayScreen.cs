using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using BoxicsGame.ParticleSystem;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace BoxicsGame
{
    class GameplayScreen : GameScreen
    {
        World world;
        GameLevel level;
        RulesManager rulesManager;

        public GameplayScreen(int levelId)
        {
            CreateWorld();
            Reload(levelId);
        }

        private void CreateWorld()
        {
            // Create the Farseer physics world
            Vector2 defaultGravity = new Vector2(0, 9.8f);
            world = new World(defaultGravity);
        }

        public override void Reload(object data) 
        {
            world.Clear();

            // World limits (ground)
            float simulatedHeight = BoxicsGame.Viewport.Height / BoxicsGame.ViewScale;
            float simulatedWidth = BoxicsGame.Viewport.Width / BoxicsGame.ViewScale;

            BodyFactory.CreateEdge(world, new Vector2(0.0f, 0.0f), new Vector2(simulatedWidth, 0.0f));
            BodyFactory.CreateEdge(world, new Vector2(simulatedWidth, 0.0f), new Vector2(simulatedWidth, simulatedHeight));
            BodyFactory.CreateEdge(world, new Vector2(0.0f, simulatedHeight), new Vector2(simulatedWidth, simulatedHeight));
            BodyFactory.CreateEdge(world, new Vector2(0.0f, 0.0f), new Vector2(0.0f, simulatedHeight));

            // World objects/bodies and game rules
            level = GameLevelsFactory.CreateLevel(world, (int)data);
            rulesManager = new RulesManager(world, level);
        }

        public override void Update(float dt)
        {
            world.Step(dt);
            level.Update();
            rulesManager.Update(dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            level.Draw(spriteBatch);
        }
    }
}
