using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework.Input;

namespace BoxicsGame
{
    class BoxCreationManager
    {
        List<BoxArea> boxAreas;
        World world;

        public BoxCreationManager(World world, List<BoxArea> boxAreas)
        {
            this.world = world;
            this.boxAreas = boxAreas;
        }

#if WINDOWS
        bool isMousePressed = false;
#endif

        public void Update()
        {
#if WINDOWS_PHONE
            TouchCollection touches = TouchPanel.GetState();
            if (touches.Count < 1 || touches[0].State != TouchLocationState.Pressed)
            {
                return;
            }
            Vector2 touchPos = touches[0].Position / BoxicsGame.ViewScale;
#elif WINDOWS
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                isMousePressed = true;
            }

            if (mouseState.LeftButton != ButtonState.Released || !isMousePressed)
            {
                return;
            }
            
            isMousePressed = false;
            Vector2 touchPos = new Vector2(mouseState.X, mouseState.Y) / BoxicsGame.ViewScale;
#endif

            foreach (BoxArea boxArea in boxAreas)
            {
                if (boxArea.Intersect(touchPos))
                {
                    boxArea.RecreateBoxAt(touchPos, Vector2.Zero);
                    break;
                }
            }
        }
    }
}

/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Boxics
{
    class BoxCreationManager
    {
        List<BoxArea> boxAreas;
        World world;
        Vector2 direction;
        BoxArea beginTouchBoxArea;
        Vector2 endTouchPosBoxArea;
        bool isFirstTouchInside;

        public BoxCreationManager(World world, List<BoxArea> boxAreas)
        {
            this.world = world;
            this.boxAreas = boxAreas;
        }

        public void Update()
        {
            TouchCollection touches = TouchPanel.GetState();
            if (touches.Count < 1)
            {
                return;
            }

            Vector2 touchPos = touches[0].Position / BoxicsGame.ViewScale;
            switch (touches[0].State)
            {
                case TouchLocationState.Pressed:
                    isFirstTouchInside = false;
                    foreach (BoxArea boxArea in boxAreas)
                    {
                        if (boxArea.Intersect(touchPos))
                        {
                            isFirstTouchInside = true;
                            beginTouchBoxArea = boxArea;
                            endTouchPosBoxArea = touchPos;
                            break;
                        }
                    }

                    break;

                case TouchLocationState.Moved:
                    if (isFirstTouchInside && beginTouchBoxArea.Intersect(touchPos))
                    {
                        direction = touchPos - endTouchPosBoxArea;
                        endTouchPosBoxArea = touchPos;
                    }
                    break;

                case TouchLocationState.Released:
                    if (isFirstTouchInside)
                    {
                        direction = direction.LengthSquared() > 0 ? Vector2.Normalize(direction) : direction;
                        beginTouchBoxArea.RecreateBoxAt(endTouchPosBoxArea, direction * 5);
                    }
                    break;
            }
        }
    }
}
*/
