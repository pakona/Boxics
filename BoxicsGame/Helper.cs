//Microsoft stuff from Particle System

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxicsGame
{
    class Helper
    {
        //  a handy little function that gives a random float between two
        // values. This will be used in several places in the sample, in particilar in
        // ParticleSystem.InitializeParticle.
        public static float RandomBetween(float min, float max)
        {
            return min + (float)BoxicsGame.Random.NextDouble() * (max - min);
        }
    }
}
