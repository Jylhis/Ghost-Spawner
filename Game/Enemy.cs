/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen & Veeti Karttunen
 *
 * This is part of Object-Oriented and GUI Programming course assignment
 * and is licensed under MIT license, see LICENSE file.
 *
 * Created: 22.02.2016
 */
using SDL2;
using System;

namespace src
{
    public class Enemy : SDLGameObject
    {
        private new int health = 100;
        public bool IsKill { get; set; }
        Random rnd;
        UInt32 tTime;

        public Enemy(LoaderParams pParams)
            : base(ref pParams)
        {
            IsKill = false;
            rnd = new Random((int)(DateTime.Now.TimeOfDay.Ticks + SDL.SDL_GetTicks() - (pParams.W * pParams.X) * pParams.Y));
            health = 100;
            damage = 10;
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            if (500 <= (int)SDL.SDL_GetTicks() - tTime)
            {
                tTime = SDL.SDL_GetTicks();

                switch ((Direction)rnd.Next(0, 9))
                {
                    case Direction.LEFT:
                        velocity.X = -2;
                        break;
                    case Direction.RIGHT:
                        velocity.X = 2;
                        break;
                    case Direction.UP:
                        velocity.Y = 2;
                        break;
                    case Direction.DOWN:
                        velocity.Y = -2;
                        break;
                    case Direction.UPLE:
                        velocity.Y = 2;
                        velocity.X = -2;
                        break;
                    case Direction.UPRI:
                        velocity.Y = 2;
                        velocity.X = 2;
                        break;
                    case Direction.DOLE:
                        velocity.Y = -2;
                        velocity.X = -2;
                        break;
                    case Direction.DORI:
                        velocity.Y = -2;
                        velocity.X = 2;
                        break;
                    default:
                        break;
                }
            }

            currentFrame = (int)(((SDL.SDL_GetTicks()) / 100) % 4);
            base.Update();
        }
        public override void OnCollision(int damage = 0)
        {
            health -= damage;
            if (health <= 0)
            {
                IsKill = true;
            }
        }
#if DEBUG
        ~Enemy()
        {
            Console.WriteLine("Enemy Deconstructor");
        }
#endif
    }
}

