// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;

namespace src
{
    public class PlayState : GameState
    {
        private const string menuID = "PLAY";

        private List<GameObject> gameobjects;

        public override void update()
        {
            if (InputHandler.Instance.isKeyDown(SDL2.SDL.SDL_Scancode.SDL_SCANCODE_ESCAPE))
            {
                Game.Instance.getStateMachine.pushState(new PauseState());
            }

            for (int i = 0; i < gameobjects.Count; i++)
            {
                gameobjects[i].Update();
            }
        }

        public override void render()
        {
            for (int i = 0; i < gameobjects.Count; i++)
            {
                gameobjects[i].Draw();
            }
        }

        public override bool onEnter()
        {
            gameobjects = new List<GameObject>();
            if (!TextureManager.Instance.Load("Resources/Player.bmp", "player", Game.Instance.getRenderer))
            {
                return false;
            }
            GameObject player = new Player(new LoaderParams(100, 100, 55, 55, "player"));

            gameobjects.Add(player);

            Console.WriteLine("Entering Playstate");
            return true;
        }

        public override bool onExit()
        {
            for (int i = 0; i < gameobjects.Count; i++)
            {
                gameobjects[i].Clean();
            }
            gameobjects.Clear();
            TextureManager.Instance.clearFromTextureMap("Room");

            Console.WriteLine("Exiting Playstate");
            return true;
        }

        public override string getStateID()
        {
            return menuID;
        }
    }
}
  