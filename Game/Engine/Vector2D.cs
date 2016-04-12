/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen & Veeti Karttunen
 *
 * This is part of Object-Oriented and GUI Programming course assignment
 * and is licensed under MIT license, see LICENSE file.
 *
 * Created: 24.02.2016
 */
using System;

namespace src
{
    public class Vector2D
    {
        private double x, y;

        /// <summary>
        /// Initializes a new instance of the <see cref="src.Vector2D"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Vector2D(double inx, double iny)
        {
            x = inx;
            y = iny;
        }

        /// <param name="v1">V1.</param>
        /// <param name="v2">V2.</param>
        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            v1.X += v2.X;
            v1.Y += v2.Y;
            return v1;
        }

        /// <param name="v1">V1.</param>
        /// <param name="scalar">Scalar.</param>
        public static Vector2D operator *(Vector2D v1, float scalar)
        {
            v1.X *= scalar;
            v1.Y *= scalar;
            return v1;
        }

        /// <param name="v1">V1.</param>
        /// <param name="v2">V2.</param>
        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            v1.X -= v2.X;
            v1.Y -= v2.Y;
            return v1;
        }

        /// <param name="v1">V1.</param>
        /// <param name="scalar">Scalar.</param>
        public static Vector2D operator /(Vector2D v1, float scalar)
        {
            v1.X /= scalar;
            v1.Y /= scalar;
            return v1;
        }

        /// <summary>
        /// Normalize this instance.
        /// </summary>
       /* public void Normalize()
        {
            double l = 1 / Length;
            if (l > 0)
            {
                this.X *= l;
                this.Y *= l;
            }
        }*/


        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>The x.</value>
        public double X { get { return x; } set { x = value; } }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>The y.</value>
        public double Y { get { return y; } set { y = value; } }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>The length.</value>
        // public double Length { get { return Math.Sqrt(x * x + y * y); } }
    }
}
