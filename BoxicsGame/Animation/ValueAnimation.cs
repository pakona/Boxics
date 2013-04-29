using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BoxicsGame
{
    class ValueAnimation
    {
        float from;
        float to;
        float duration;
        float elapsed;
        float dt;

        public ValueAnimation(float from, float to, float duration)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            elapsed = 0;
            dt = (from - to) / duration;
            IsDone = false;
        }

        public bool IsDone { get; private set; }

        public void Update(float dt)
        {
            elapsed += dt * 1000;
            IsDone = (elapsed > duration);
        }
    }
}
