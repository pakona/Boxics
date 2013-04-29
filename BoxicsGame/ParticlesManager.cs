using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using BoxicsGame.ParticleSystem;

namespace BoxicsGame
{
    class ParticlesManager
    {
        ExplosionParticleSystem explosion;
        ExplosionSmokeParticleSystem smoke;
        SmokePlumeParticleSystem smokePlume;

        // a timer that will tell us when it's time to trigger another explosion.
        const float TimeBetweenExplosions = 2.0f;
        float timeTillExplosion = 0.0f;

        // keep a timer that will tell us when it's time to add more particles to the
        // smoke plume.
        const float TimeBetweenSmokePlumePuffs = .5f;
        float timeTillPuff = 0.0f;

        public ParticlesManager()
        {
            explosion = new ExplosionParticleSystem(1);
            smoke = new ExplosionSmokeParticleSystem(2);
            smokePlume = new SmokePlumeParticleSystem(9);
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            explosion.Update(gameTime);
            smoke.Update(gameTime);
            smokePlume.Update(gameTime);
            //UpdateExplosions(dt);
            UpdateSmokePlume(dt); 
        }

        // this function is called when we want to demo the smoke plume effect. it
        // updates the timeTillPuff timer, and adds more particles to the plume when
        // necessary.
        private void UpdateSmokePlume(float dt)
        {
            timeTillPuff -= dt;
            if (timeTillPuff < 0)
            {
                Vector2 where = Vector2.Zero;
                // add more particles at the bottom of the screen, halfway across.
                where.X = BoxicsGame.Viewport.Width / 2;
                where.Y = BoxicsGame.Viewport.Height;
                smokePlume.AddParticles(where);

                // and then reset the timer.
                timeTillPuff = TimeBetweenSmokePlumePuffs;
            }
        }

        // this function is called when we want to demo the explosion effect. it
        // updates the timeTillExplosion timer, and starts another explosion effect
        // when the timer reaches zero.
        private void UpdateExplosions(float dt)
        {
            timeTillExplosion -= dt;
            if (timeTillExplosion < 0)
            {
                Vector2 where = Vector2.Zero;
                // create the explosion at some random point on the screen.
                where.X = Helper.RandomBetween(0, BoxicsGame.Viewport.Width);
                where.Y = Helper.RandomBetween(0, BoxicsGame.Viewport.Height);

                // the overall explosion effect is actually comprised of two particle
                // systems: the fiery bit, and the smoke behind it. add particles to
                // both of those systems.
                explosion.AddParticles(where);
                smoke.AddParticles(where);

                // reset the timer.
                timeTillExplosion = TimeBetweenExplosions;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            explosion.Draw(spriteBatch);
            smoke.Draw(spriteBatch);
            smokePlume.Draw(spriteBatch);
        }
    }
}
