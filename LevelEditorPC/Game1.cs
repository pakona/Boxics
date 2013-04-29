using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Timers;
using Microsoft.Xna.Framework.Input;

namespace LevelEditorPC
{
    class Game1 : GraphicsDeviceControl
    {
        BoxicsGame.BoxicsGame boxicsGame;
        Timer renderLoopTimer;
        float TargetElapsedTime = 1 / 30f;
        ContentManager Content { get; set; }

        protected override void Initialize()
        {
            Content = new ContentManager(Services, "Content");
            boxicsGame = new BoxicsGame.BoxicsGame(GraphicsDevice, Content);
            boxicsGame.LoadContent();
            Reload(new BoxicsDataTypes.LevelData());

            Mouse.WindowHandle = this.Handle;

            renderLoopTimer = new Timer();
            renderLoopTimer.Elapsed += new ElapsedEventHandler(RenderLoop);
            renderLoopTimer.Interval = 1000 * TargetElapsedTime;
            renderLoopTimer.Enabled = true;
        }

        void RenderLoop(object sender, ElapsedEventArgs e)
        {
            Update();
            Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            renderLoopTimer.Dispose();

            if (disposing)
            {
                Content.Unload();
            }

            base.Dispose(disposing);
        }

        protected new void Update()
        {
            boxicsGame.Update(TargetElapsedTime);
        }

        protected override void Draw()
        {
            boxicsGame.Draw();
        }

        internal void Reload(BoxicsDataTypes.LevelData data)
        {
            BoxicsGame.BoxicsGame.LevelsData = new BoxicsDataTypes.LevelData[1];
            BoxicsGame.BoxicsGame.LevelsData[0] = data;
            boxicsGame.Reset();
        }
    }
}
