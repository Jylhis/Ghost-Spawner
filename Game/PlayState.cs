// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using SDL2;

namespace src
{
    public class PlayState : GameState
    {
        private const string menuID = "PLAY";

        private List<GameObject> gameobjects;
        private Level pLevel;

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

            if (checkCollision(gameobjects[0], gameobjects[1]))
            {
                Game.Instance.getStateMachine.pushState(new GameOverState());
            }

        }

        public override void render()
        {
            // TODO: Fix level render
            //pLevel.render();
            for (int i = 0; i < gameobjects.Count; i++)
            {
                gameobjects[i].Draw();
            }
        }

        public override bool onEnter()
        {
            gameobjects = new List<GameObject>();

            LevelParser levelParser = new LevelParser();
            pLevel = levelParser.ParseLevel("Resources/map.tmx");

            // Add Player
            if (!TextureManager.Instance.Load("Resources/Player.bmp", "player", Game.Instance.getRenderer))
            {
                return false;
            }
            GameObject player = new Player(new LoaderParams(100, 100, 55, 55, "player"));

            // Add Enemy
            if (!TextureManager.Instance.Load("Resources/Enemy.bmp", "enemy", Game.Instance.getRenderer))
            {
                return false;
            }
            GameObject enemy = new Enemy(new LoaderParams(300, 300, 55, 55, "enemy"));


            gameobjects.Add(player);
            gameobjects.Add(enemy);

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

        bool checkCollision(params GameObject[] list)
        {
            SDLGameObject p1 = (SDLGameObject)list[0];
            SDLGameObject p2 = (SDLGameObject)list[1];
            int leftA, leftB;
            int rightA, rightB;
            int topA, topB;
            int bottomA, bottomB;

            leftA = (int)p1.position.X;
            rightA = (int)(p1.position.X + p1.w);
            topA = (int)p1.position.Y;
            bottomA = (int)(p1.position.Y + p1.h);

            leftB = (int)p2.position.X;
            rightB = (int)(p2.position.X + p2.w);
            topB = (int)p2.position.Y;
            bottomB = (int)(p2.position.Y + p2.h);

            if (bottomA <= topB)
            {
                return false;
            }
            if (topA >= bottomB)
            {
                return false;
            }
            if (rightA <= leftB)
            {
                return false;
            }
            if (leftA >= rightB)
            {
                return false;
            }
                            
            return true;
        }

        public override string getStateID()
        {
            return menuID;
        }
    }
}
  