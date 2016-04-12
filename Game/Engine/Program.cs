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
    class Program
    {
        const int FPS = 144;
        const int DELAY_TIME = (int)(1000.0 / FPS);

        static int Main(string[] args)
        {
#if DEBUG
            Console.WriteLine("DEBUG MODE");
#else
            Console.WriteLine("RELEASE MODE"); 
#endif
            UInt32 frameStart, frameTime;
            Game.Instance.Init("Peli", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, 1024, 720, false);

            while (Game.Instance.IsRunning)
            {
                frameStart = SDL.SDL_GetTicks();

                Game.Instance.HandleEvents();
                Game.Instance.Update();
                Game.Instance.Render();

                frameTime = SDL.SDL_GetTicks() - frameStart;

                if (frameTime < DELAY_TIME)
                {
                    SDL.SDL_Delay(DELAY_TIME - frameTime);
                }
            }
            Game.Instance.Close();
            return 0;
        }
    }
}
