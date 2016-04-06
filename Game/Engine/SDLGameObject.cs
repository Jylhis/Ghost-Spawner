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
        public int health, damage;

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
            TextureManager.Instance.DrawFrame(id,
                    (int)Position.X, (int)Position.Y,
                    W, H, currentRow, currentFrame,
                    Game.Instance.GetRenderer, SDL.SDL_RendererFlip.SDL_FLIP_NONE, 0.0);
        }

        /// <summary>
        /// Update this instance.
        /// </summary>
        public virtual void Update()
        {
            velocity += acceleration;

            if (Position.X <= 10)
            {
                if (velocity.X < 0)
                {
                    velocity.X = 0;
                }
                else
                {
                    rect.x = 11;
                }

            }
            else if (Position.X >= 970)
            {
                if (velocity.X > 0)
                {
                    velocity.X = 0;
                }
                else
                {
                    rect.x = 969;
                }
            }

            if (Position.Y <= 10)
            {
                if (velocity.Y < 0)
                {
                    velocity.Y = 0;
                }
                else
                {
                    rect.y = 11;
                }
            }
            else if (Position.Y >= 670)
            {
                if (velocity.Y > 0)
                {
                    velocity.Y = 0;
                }
                else
                {
                    rect.y = 669;
                }
            }
            Position += velocity;
        }

        public virtual void OnCollision(int damage = 0)
        {
#if DEBUG
            Console.WriteLine(this+" health: "+health);
#endif
        }
    }
}
