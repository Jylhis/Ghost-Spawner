/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 25.02.2016
 */
using System;
using System.Collections.Generic;

namespace src
{
    public class PlayState : GameState
    {
        private const string menuID = "PLAY";

        public static List<GameObject> gameObjects = new List<GameObject>();

        public override void Update()
        {
            if (InputHandler.Instance.IsKeyDown(SDL2.SDL.SDL_Scancode.SDL_SCANCODE_ESCAPE))
            {
                Game.Instance.GetStateMachine.PushState(new PauseState());
            }

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }

            if (checkCollision(gameObjects[0], gameObjects[1]))
            {
                Game.Instance.GetStateMachine.ChangeState(new GameOverState());
            }

        }

        public override void Render()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw();
            }
        }
        

        public override bool OnEnter()
        {
            // Add Player
            if (!TextureManager.Instance.Load("Resources/spr_player_strip8.png", "player", Game.Instance.GetRenderer))
            {
                return false;
            }
            GameObject player = new Player(new LoaderParams(100, 100, 40, 40, "player"));

            // Add Enemy
            if (!TextureManager.Instance.Load("Resources/spr_suicidebomber_strip4.png", "enemy", Game.Instance.GetRenderer))
            {
                return false;
            }
            GameObject enemy = new Enemy(new LoaderParams(300, 300, 40, 40, "enemy"));

            // Load bullet
            if (!TextureManager.Instance.Load("Resources/spr_playerbullet.png", "bullet", Game.Instance.GetRenderer))
            {
                return false;
            }


            gameObjects.Add(player);
            gameObjects.Add(enemy);

            Console.WriteLine("Entering Playstate");
            return true;
        }

        public override bool OnExit()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Clean();
            }
            gameObjects.Clear();
            TextureManager.Instance.ClearFromTextureMap("Room");

            Console.WriteLine("Exiting Playstate");
            return true;
        }

        private bool checkCollision(params GameObject[] list)
        {
            SDLGameObject p1 = (SDLGameObject)list[0];
            SDLGameObject p2 = (SDLGameObject)list[1];
            int leftA, leftB;
            int rightA, rightB;
            int topA, topB;
            int bottomA, bottomB;

            leftA = (int)p1.position.X;
            rightA = (int)(p1.position.X + p1.W);
            topA = (int)p1.position.Y;
            bottomA = (int)(p1.position.Y + p1.H);

            leftB = (int)p2.position.X;
            rightB = (int)(p2.position.X + p2.W);
            topB = (int)p2.position.Y;
            bottomB = (int)(p2.position.Y + p2.H);

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

        public override string GetStateID()
        {
            return menuID;
        }
    }
}
