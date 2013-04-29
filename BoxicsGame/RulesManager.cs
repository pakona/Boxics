using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace BoxicsGame
{
    class RulesManager
    {
        GameLevel level;
        BoxCreationManager boxCreationManager;
        Box CompletionBox { get; set; }
        bool IsLevelCompleted { get; set; }
        ValueAnimation transitionTimer;

        public RulesManager(World world, GameLevel level)
        {
            this.level = level;
            boxCreationManager = new BoxCreationManager(world, level.BoxAreas);
            IsLevelCompleted = false;
        }

        public void Update(float dt)
        {
            if (IsLevelCompleted && transitionTimer.IsDone)
            {
                ScreenManager.Instance.NavigateToGameplayScreen(level.Id + 1);
            }

            if (IsLevelCompleted)
            {
                // Attract the box in the completion square
                CompletionBox.Body.LinearVelocity = (level.CompletionSquareCenter - CompletionBox.Center) / 0.2f;
                transitionTimer.Update(dt);
                return;
            }

            boxCreationManager.Update();
            foreach (BoxArea boxArea in level.BoxAreas)
            {
                if (boxArea.BoxAlive != null && Vector2.DistanceSquared(boxArea.BoxAlive.Center, level.CompletionSquareCenter) < level.Precision * level.Precision)
                {
                    OnLevelCompleted(boxArea.BoxAlive);
                    break;
                }
            }
        }

        void OnLevelCompleted(Box box)
        {
            transitionTimer = new ValueAnimation(0.0f, 2000f, 2000f);

            IsLevelCompleted = true;
            CompletionBox = box;
            CompletionBox.Body.IgnoreGravity = true;
            CompletionBox.Body.LinearVelocity = Vector2.Zero;
            CompletionBox.Body.AngularVelocity = 0.0f;
            CompletionBox.Body.Rotation = 0.0f;
        }
    }
}
