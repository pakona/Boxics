using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using BoxicsGame.Textures;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using BoxicsDataTypes;
using FarseerPhysics.Common;

namespace BoxicsGame
{
    class Speedwalk
    {
        SpeedwalkPath speedwalkPath;
        List<Body> bodies;
        Path path;
        float speed;
        bool leftToRight;
        float dShift;
        float shift = 0.0f;
        float radius;
        Vector2 from;
        Vector2 to;

        public Speedwalk(World world, SpeedwalkData speedwalkData)
        {
            bodies = new List<Body>(50);
            speed = Math.Abs(speedwalkData.Speed);
            leftToRight = speedwalkData.LeftToRight;
            radius = speedwalkData.Radius;
            from = speedwalkData.From;
            to = speedwalkData.To;
            speedwalkPath = new SpeedwalkPath(from, to, radius, bodies.Capacity);

            for (int i = 0; i < bodies.Capacity; i++)
            {
                Body body = BodyFactory.CreateCircle(world, speedwalkPath.delta / 2, 1f);
                body.Position = speedwalkPath.Pts[i];
                body.BodyType = BodyType.Kinematic;
                body.LinearVelocity = Vector2.Zero;

                bodies.Add(body);
            }

            path = new Path();
            foreach (Vector2 pt in speedwalkPath.Pts)
            {
                path.Add(pt);
            }
            path.Closed = true;

            dShift = leftToRight ? -speed / 30 : speed / 30;
        }

        public void Update()
        {
            shift += dShift;
            if (Math.Abs(shift) > 1f)
                shift = 0;

            for (int i = 0; i < bodies.Count; i++)
            {
                float time = shift + i * 1f / bodies.Count;
                if (time > 1f)
                {
                    time -= 1f;
                }
                if (time < 0.0f)
                {
                    time += 1f;
                }
                PathManager.MoveBodyOnPath(path, bodies[i], time, 1f, 1f / 30);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(BasicTextures.Circle.Width / 2f, BasicTextures.Circle.Height / 2f);
            foreach (Body body in bodies)
            {
                spriteBatch.Draw(BasicTextures.Circle, body.Position, null, Color.Red, body.Rotation, origin,
                    (speedwalkPath.delta / 2) / BasicTextures.Circle.Width, SpriteEffects.None,
                    0.0f);
            }
        }
    }

    class SpeedwalkPath
    {
        public float delta;
        public Vector2[] Pts;
        public SpeedwalkPath(Vector2 from, Vector2 to, float radius, int numberOfPoints)
        {
            this.Pts = new Vector2[numberOfPoints];
            HorizontalSpeedwalkPath horizontalSpeedwalkPath = new HorizontalSpeedwalkPath((from - to).Length(), radius, numberOfPoints);
            this.delta = horizontalSpeedwalkPath.delta;

            //Rotate and translate
            Vector2 center = (from + to) / 2;
            float rotationAngle = (float)Math.Atan2(to.Y - from.Y, to.X - from.X);
            float cosAngle = (float)Math.Cos(rotationAngle);
            float sinAngle = (float)Math.Sin(rotationAngle);
            Vector2 translation = center;
            float x, y;
            for (int i = 0; i < numberOfPoints; i++)
            {
                x = (horizontalSpeedwalkPath.Pts[i].X * cosAngle - horizontalSpeedwalkPath.Pts[i].Y * sinAngle) + translation.X;
                y = radius + (horizontalSpeedwalkPath.Pts[i].X * sinAngle + horizontalSpeedwalkPath.Pts[i].Y * cosAngle) + translation.Y;
                Pts[i] = new Vector2(x, y);
            }
        }
    }

    /// <summary>
    /// Creates a speedwalk centered at (0, 0)
    /// </summary>
    class HorizontalSpeedwalkPath
    {
        public float delta;
        float radius;
        float minX;
        float maxX;
        public Vector2[] Pts;
        public HorizontalSpeedwalkPath(float width, float radius, int numberOfPoints)
        {
            this.Pts = new Vector2[numberOfPoints];
            this.radius = radius;
            this.minX = -width / 2 - radius / 2;
            this.maxX = width / 2 + radius / 2;
            this.delta = 2 * (maxX - minX) / numberOfPoints;

            for (int i = 0; i < numberOfPoints; i++)
            {
                float x = minX + i * delta;
                float y;
                if (x > maxX)
                {
                    x = 2 * maxX - x;
                    y = -Equation(x);
                }
                else
                {
                    y = Equation(x);
                }
                Pts[i] = new Vector2(x, y);
            }
        }

        float Equation(float x)
        {
            if (minX + radius < x && x < maxX - radius)
            {
                return radius;
            }

            float a, b = 0.0f;
            if (x < minX + radius)
            {
                a = minX + radius;
            }
            else
            {
                a = maxX - radius;
            }
            return (float)Math.Sqrt(Math.Abs((x - a) * (x - a) - radius * radius)) + b;
        }
    }
}
