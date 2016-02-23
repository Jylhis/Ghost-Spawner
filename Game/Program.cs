// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen

namespace src
{
    class Program
    {
		static int Main(string[] args)
		{
			// Init
			Game game = new Game();
            
			Player player = new Player(10, 10, 55, 55, "player", ref game);  // Create player

			//  Main game loop
			while (game.IsRunning) {
                game.HandleEvents();
                game.Update();

                player.Update();

                game.Render(ref player);

                
			}
			game.Clean();
			return 0;
		}
    }
}
