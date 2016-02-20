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
			IntPtr window = IntPtr.Zero;

			// Init stuff
			bool quit = false;
			SDL.SDL_Event e;
			Player player = new Player();  // Create player
			Area level = new Area(); // Create Level


			window = Init(window);

			IntPtr surface = SDL.SDL_GetWindowSurface( window );
			IntPtr image = SDL.SDL_LoadBMP ("Media/test.bmp");  // Load image
			SDL.SDL_BlitSurface(image, IntPtr.Zero, surface, IntPtr.Zero);
			SDL.SDL_UpdateWindowSurface (window);

			//  Main game loop
			while (!quit) {

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
			SDL.SDL_FreeSurface (surface);
			SDL.SDL_DestroyWindow (window);
			SDL.SDL_Quit ();  // Quit everything SDL

			return 0;
		}

		static IntPtr Init(IntPtr window) {
			SDL.SDL_Init(SDL.SDL_INIT_VIDEO);
			window = SDL.SDL_CreateWindow(
				"Peli",
				SDL.SDL_WINDOWPOS_UNDEFINED, 
				SDL.SDL_WINDOWPOS_UNDEFINED,
				800, 600, 
				SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
			if (window == IntPtr.Zero) {
				Console.WriteLine("Could not create window: " + SDL.SDL_GetError());

			}
			return window;
		}
    }
}
