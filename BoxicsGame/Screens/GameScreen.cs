using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BoxicsGame
{
    abstract class GameScreen
    {
        public virtual void Reload(object data) { }
        public abstract void Update(float dt);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
