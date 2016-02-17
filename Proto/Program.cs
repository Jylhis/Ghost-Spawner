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
            while (true) { }

            // Aloita game loop
        }
    }
}
