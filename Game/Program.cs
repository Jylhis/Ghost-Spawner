// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using SDL2;
using System;

namespace src
{
	class Program
	{
		static int Main (string[] args)
		{
			Game game = new Game ("Peli", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, 800, 600, false);
            
			Player player = new Player (10, 10, 55, 55, "player", ref game);

			while (game.IsRunning) {
				game.HandleEvents ();
				game.Update ();
				game.Render ();
			}

			game = null;

			return 0;
		}
	}
}
