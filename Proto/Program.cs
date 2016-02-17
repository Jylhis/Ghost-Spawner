// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace Proto
{
    class Program
    {
        static void Main(string[] args)
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            var gWindow = SDL.SDL_CreateWindow("Peli", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED,800, 600, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            if(gWindow == null)
            {
                Console.WriteLine( "Window could not be created! SDL_Error: {0}\n", SDL.SDL_GetError() );
            }
            else
            {
                //Get window surface
                var gScreenSurface = SDL.SDL_GetWindowSurface( gWindow );
            }
            var gHelloWorld = SDL.SDL_LoadBMP("test.bmp");
            if (gHelloWorld == null)
            {
                Console.WriteLine("Unable to load image %s! SDL Error: %s\n", "02_getting_an_image_on_the_screen/hello_world.bmp", SDL.SDL_GetError());
            }
            //while (true) { }

            // Aloita game loop
        }
    }
}
