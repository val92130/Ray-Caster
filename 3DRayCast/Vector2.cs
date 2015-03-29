using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DRayCast
{
    [Serializable]
    public class Vector2
    {
        double x, y;
        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public double Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        public float Length
        {
            get
            {
                return (float)(Math.Sqrt((this.X * this.X) + (this.Y * this.Y)));
            }
        }

        public void Normalize()
        {
            this.x = this.x / this.Length;
            this.y = this.y / this.Length;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2 Normalize(Vector2 point)
        {
            float length = (float)(Math.Sqrt((point.X * point.X) + (point.Y * point.Y)));
            return new Vector2(point.X / length, point.Y / length);
        }

        public static Vector2 MultiplyAngle(Vector2 vector, float angle)
        {
            Vector2 newp = new Vector2((float)(vector.X * Math.Cos(angle) + vector.Y * -Math.Sin(angle)),
               (float)(vector.X * Math.Sin(angle) + vector.Y * Math.Cos(angle)));
            return newp;
        }

        public static float Lerp(float flGoal, float flCurrent, float dt)
        {
            float flDifference = flGoal - flCurrent;

            if (flDifference > dt)
            {
                return flCurrent + dt;
            }
            if (flDifference < -dt)
            {
                return flCurrent - dt;
            }
            return flGoal;
        }
    }
}
