/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 24.02.2016
 */
using SDL2;
using System;

namespace src
{
    public class SDLGameObject
    {
        protected int currentRow, currentFrame;
        protected Vector2D velocity, acceleration;
        protected SDL.SDL_Rect rect;

        public SDL.SDL_Rect getRect
        {
            get
            {
                return rect;
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int W
        {
            get
            {
                return rect.w;
            }
            set
            {
                rect.w = value;
            }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int H
        {
            get
            {
                return rect.h;
            }
            set
            {
                rect.h = value;
            }
        }

        /// <summary>
        /// The position.
        /// </summary>
        public Vector2D Position
        {
            get
            {
                Vector2D tmp = new Vector2D(rect.x, rect.y);
                return tmp;
            }
            set
            {
                rect.x = (int)value.X;
                rect.y = (int)value.Y;
            }
        }

        /// <summary>
        /// The identifier.
        /// </summary>
        public string id;

        /// <summary>
        /// Initializes a new instance of the <see cref="src.SDLGameObject"/> class.
        /// </summary>
        /// <param name="pParams">Parameters.</param>
        public SDLGameObject(ref LoaderParams pParams)
        {
            Position = new Vector2D(pParams.X, pParams.Y);
            velocity = new Vector2D(0, 0);
            acceleration = new Vector2D(0, 0);
            W = pParams.W;
            H = pParams.H;
            id = pParams.Id;

            currentFrame = 1;
            currentRow = 1;
        }

        /// <summary>
        /// Draw this instance.
        /// </summary>
        public virtual void Draw()
        {
            double ang;
            if (velocity.Y < 0 && velocity.X == 0)
            {
                ang = -90.0;
            }
            else if (velocity.Y > 0 && velocity.X == 0)
            {
                ang = 90.0;
            }
            else
            {
                ang = 0;
            }


            if (velocity.X < 0)
            {
                if (velocity.Y < 0)
                    ang = 45;
                if (velocity.Y > 0)
                    ang = -45;

                TextureManager.Instance.DrawFrame(id,
                    (int)Position.X, (int)Position.Y,
                    W, H, currentRow, currentFrame,
                    Game.Instance.GetRenderer, SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL, ang);

            }
            else
            {
                if (velocity.Y < 0 && velocity.X != 0)
                    ang = 315;
                if (velocity.Y > 0 && velocity.X != 0)
                    ang = -315;

                TextureManager.Instance.DrawFrame(id,
                    (int)Position.X, (int)Position.Y,
                    W, H, currentRow, currentFrame,
                    Game.Instance.GetRenderer, SDL.SDL_RendererFlip.SDL_FLIP_NONE, ang);
            }
        }

        /// <summary>
        /// Update this instance.
        /// </summary>
        public virtual void Update()
        {
            velocity += acceleration;

            switch ((int)Position.X)
            {
                case 10:
                    if (velocity.X < 0)
                    {
                        velocity.X = 0;
                    }
                    break;
                case 970:
                    if (velocity.X > 0)
                    {
                        velocity.X = 0;
                    }
                    break;
                default:
                    break;
            }
            switch ((int)Position.Y)
            {
                case 10:
                    if (velocity.Y < 0)
                    {
                        velocity.Y = 0;
                    }
                    break;
                case 670:
                    if (velocity.Y > 0)
                    {
                        velocity.Y = 0;
                    }
                    break;
                default:
                    break;
            }

            Position += velocity;

        }

        public virtual void OnCollision()
        {
            Console.WriteLine(this + ": OnCollision");
        }


    }
}
