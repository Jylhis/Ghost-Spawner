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

        public static List<SDLGameObject> gameObjects = new List<SDLGameObject>();

        public override void Update()
        {
            if (InputHandler.Instance.IsKeyDown(SDL2.SDL.SDL_Scancode.SDL_SCANCODE_ESCAPE))
            {
                Game.Instance.GetStateMachine.Push(new PauseState());
            }
            
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
                if (gameObjects[i] is Bullet)
                {
                    Bullet tmp = (Bullet)gameObjects[i];
                    if(!tmp.IsMoving)
                    {
                        gameObjects.Remove(gameObjects[i]);
                    }
                    
                }
            }

            if (checkCollision(gameObjects[0], gameObjects[1]))
            {
                Game.Instance.GetStateMachine.Change(new GameOverState());
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
            SDLGameObject player = new Player(new LoaderParams(100, 100, 40, 40, "player"));

            // Add Enemy
            if (!TextureManager.Instance.Load("Resources/spr_suicidebomber_strip4.png", "enemy", Game.Instance.GetRenderer))
            {
                return false;
            }
            SDLGameObject enemy = new Enemy(new LoaderParams(300, 300, 40, 40, "enemy"));

            // Load bullet
            if (!TextureManager.Instance.Load("Resources/spr_playerbullet.png", "bullet", Game.Instance.GetRenderer))
            {
                return false;
            }
            if (!SoundManager.Instance.Load("D:/K1565/proto/proto/Game/Resources/sound/laser1.wav","shoot",sound_type.SOUND_SFX))
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
                gameObjects[i] = null;
            }
            gameObjects.Clear();
            TextureManager.Instance.ClearFromTextureMap("Room");

            Console.WriteLine("Exiting Playstate");
            return true;
        }

        private bool checkCollision(params SDLGameObject[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                for (int j = i+1; j < list.Length; j++)
                {
                    if ((list[i].position.Y + list[i].H) <= list[j].position.Y)
                    {
                        return false;
                    }
                    if (list[i].position.Y >= (list[j].position.Y+list[j].H))
                    {
                        return false;
                    }
                    if ((list[i].position.X+list[i].W) <= list[j].position.X)
                    {
                        return false;
                    }
                    if (list[i].position.X >= (list[j].position.X+list[j].W))
                    {
                        return false;
                    }    
                }
            }
            return true;
        }

        public override string GetStateID()
        {
            return menuID;
        }
    }
}
