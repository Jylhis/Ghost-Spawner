// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen

namespace src
{
    class Program
    {
		static int Main(string[] args)
		{
			// Init
			Game game = new Game();

			Area level = new Area(); // Create Level
			Player player = new Player(ref game.Renderer, ref game);  // Create player

			//  Main game loop
			while (game.IsRunning) {
                game.HandleEvents();
                game.Update();
                game.Render();
			}
			game.Clean();
			return 0;
		}
    }
}
