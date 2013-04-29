using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BoxicsGame
{
    class ScreenManager
    {
        public GameScreen CurrentScreen { get; private set; }

        private ScreenManager()
        {
            NavigateToGameplayScreen(0);
        }

        public void NavigateTo(GameScreen screen)
        {
            CurrentScreen = screen;
        }

        public void NavigateToGameplayScreen(int levelId)
        {
            if (CurrentScreen is GameplayScreen)
            {
                CurrentScreen.Reload(levelId % BoxicsGame.LevelsData.Count());
            }
            else
            {
                CurrentScreen = new GameplayScreen(0 % BoxicsGame.LevelsData.Count());
            }
        }

        private static ScreenManager instance = null;
        public static ScreenManager Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenManager();
                }
                return instance;
            }
        }
    }
}
