// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
using Engine;

namespace src
{
    class Program
    {
		static int Main(string[] args)
		{
			// Init
			Engine.Game game = new Engine.Game();

			Area level = new Area(); // Create Level
			Player player = new Player(ref game.Renderer);  // Create player

			//  Main game loop
			while (game.IsRunning) {
				// Handle events
				while (game.PollEvents) {
					//User requests quit
					if (game.Events.type == SDL.SDL_EventType.SDL_QUIT) {
						game.IsRunning = false;
					} else if (game.Events.type == SDL.SDL_EventType.SDL_CONTROLLERAXISMOTION ||
						game.Events.type == SDL.SDL_EventType.SDL_KEYDOWN ||
						game.Events.type == SDL.SDL_EventType.SDL_KEYUP) 
					{
						player.OnEvent(game.Events);
					}
				}

				// Render to window
				SDL.SDL_RenderClear(game.Renderer);
				SDL.SDL_RenderCopy(game.Renderer, player.Texture, IntPtr.Zero, IntPtr.Zero);
				SDL.SDL_RenderPresent(game.Renderer);


			}
			game.Quit();
			return 0;
		}
    }
}
