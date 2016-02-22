﻿// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
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
			Game game = new Game();

			// Init stuff
			Area level = new Area(); // Create Level
			Player player = new Player(ref game.Renderer);  // Create player

			//  Main game loop
			while (game.IsRunning) {
				
				// Render to window
				SDL.SDL_RenderClear(game.Renderer);
				SDL.SDL_RenderCopy(game.Renderer, player.Texture, IntPtr.Zero, IntPtr.Zero);
				SDL.SDL_RenderPresent(game.Renderer);

				// Handle events
				while (game.PollEvent()) {
					//User requests quit
					if (game.Events.type == SDL.SDL_EventType.SDL_QUIT) {
						game.IsRunning = false;
					} else if (game.Events.type == SDL.SDL_EventType.SDL_CONTROLLERAXISMOTION &&
						game.Events.type == SDL.SDL_EventType.SDL_KEYDOWN &&
						game.Events.type == SDL.SDL_EventType.SDL_KEYUP) 
					{
						player.OnEvent(game.Events);
					}
				}
			}

			game.Close();
			return 0;
		}


    }
}
