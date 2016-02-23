// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using SDL2;
using System;

namespace src
{
	class Program
	{
		public static Game game;
		static int Main (string[] args)
		{
			game = new Game ("Peli", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, 800, 600, false);
			Player player = new Player (0, 0, 55, 55, "player");


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
