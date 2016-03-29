﻿/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 23.03.2016
 */
using SDL2;
using System;

namespace src
{
    class Bullet : SDLGameObject
    {
        private UInt32 starttime, ftime;
        private const int maxVel = 5;
        public int total = 0;
       public bool IsMoving
        {
            get
            {
                if (velocity.X == 0 && velocity.Y == 0)
                {
                    return false;
                }
                return true;
            }
        }
        public Bullet(LoaderParams pParams, Direction di)
            : base(ref pParams)
        {
            total++;
            starttime = SDL.SDL_GetTicks();
            switch(di)
            {
                case Direction.UP:
                    velocity.Y = -maxVel;
                    break;
                case Direction.DOWN:
                    velocity.Y = maxVel;
                    break;
                case Direction.LEFT:
                    velocity.X = -maxVel;
                    break;
                case Direction.RIGHT:
                    velocity.X = maxVel;
                    break;
                default:
                    break;
            }

        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            ftime = SDL.SDL_GetTicks();
           // Console.WriteLine(ftime - starttime);
            if(ftime - starttime >= 3000)
            {
                velocity.X = 0;
                velocity.Y = 0;
            }
            currentFrame = (int)(((SDL.SDL_GetTicks()) / 100) % 5);
            base.Update();
        }
        ~Bullet()
        {
            total--;
            Console.WriteLine("Bullet Deconstructor");
        }
        
    }
}
