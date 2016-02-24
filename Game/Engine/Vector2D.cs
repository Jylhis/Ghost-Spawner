using System;

namespace src
{
    public class Vector2D
    {
        private double x, y;
        public Vector2D(double inx,double iny)
        {
            x = inx;
            y = iny;
        }
        public static Vector2D operator+ (Vector2D v1, Vector2D v2)
        {
            //return Vector2D( x+v2.X , y+v2.Y);

            v1.X += v2.X;
            v1.Y += v2.Y;
            return v1;
        }

        // FIXME
        public static Vector2D operator* (Vector2D v1,float scalar)
        {
            v1.X *= scalar;
            v1.Y *= scalar;
            return v1;
        }
        public static Vector2D operator- (Vector2D v1, Vector2D v2)
        {
            //return Vector2D( x+v2.X , y+v2.Y);

            v1.X -= v2.X;
            v1.Y -= v2.Y;
            return v1;
        }
        public static Vector2D operator/ (Vector2D v1, float scalar)
        {
            v1.X /= scalar;
            v1.Y /= scalar;
            return v1;
        }
        public void Normalize()
        {
            double l = 1/Length;
            if(l>0)//no dividing zero
            {
                this.X *= l;
                this.Y *= l;
            }
        }


        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }

        public double Length { get { return Math.Sqrt(x * x + y * y); } }

    }
}
