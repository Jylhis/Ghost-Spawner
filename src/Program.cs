// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace src
{
    class Program
    {
		static int Main(string[] args)
		{
			// Init stuff
			bool quit = false; // Quit game
			SDL.SDL_Event e; // Catch events
			Player player = new Player();  // Create player
			Area level = new Area(); // Create Level

			// Start SDL
			if (SDL.SDL_Init (SDL.SDL_INIT_VIDEO) != 0) {
				Console.WriteLine("Could not start SDL: " + SDL.SDL_GetError());
			}

			// Create window
			IntPtr window = SDL.SDL_CreateWindow("Peli",SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED,800, 600, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
			if (window == IntPtr.Zero) {
				Console.WriteLine("Could not create window: " + SDL.SDL_GetError());
			}

			// Create Renderer
			IntPtr renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
			if (renderer == IntPtr.Zero) {
				Console.WriteLine("Could not create renderer: " + SDL.SDL_GetError());
			}

			//  Main game loop
			// https://stackoverflow.com/questions/21007329/what-is-a-sdl-renderer
			while (!quit) {

				// Render to window
				SDL.SDL_RenderClear(renderer);
				// FIXME: SDL.SDL_RenderCopy(renderer,texture, null, null);
				SDL.SDL_RenderPresent(renderer);

				// Handle events
				while (SDL.SDL_PollEvent (out e) != 0) {
					//User requests quit
					if (e.type == SDL.SDL_EventType.SDL_QUIT) {
						quit = true;
					} else if (e.type == SDL.SDL_EventType.SDL_CONTROLLERAXISMOTION &&
						e.type == SDL.SDL_EventType.SDL_KEYDOWN &&
						e.type == SDL.SDL_EventType.SDL_KEYUP) 
					{
						player.OnEvent(e);
					}

				}
			}

			// Free stuff from memory
			// FIXME: SDL.SDL_DestroyTexture(texture);
			SDL.SDL_DestroyRenderer(renderer);
			SDL.SDL_DestroyWindow (window);
			SDL.SDL_Quit ();  // Quit everything SDL

			return 0;
		}


    }
}
