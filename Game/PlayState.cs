﻿/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 25.02.2016
 */
using SDL2;
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
                    if (!tmp.IsMoving)
                    {
                        gameObjects.Remove(gameObjects[i]);
                    }
                }
            }

            foreach (SDLGameObject enemy in gameObjects.FindAll(x => x.id == "enemy"))
            {
                if (gameObjects.Find(x => x.id == "player") == null)
                {
                    break;
                }
                if (checkCollision(enemy, gameObjects.Find(x => x.id == "player")))
                {
                    //Game.Instance.GetStateMachine.Change(new GameOverState());
                }
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

            // Load Enemy textures
            if (!TextureManager.Instance.Load("Resources/spr_suicidebomber_strip4.png", "enemy", Game.Instance.GetRenderer))
            {
                return false;
            }
            //SDLGameObject enemy = new Enemy(new LoaderParams(300, 300, 40, 40, "enemy"));
            //SDLGameObject enemy1 = new Enemy(new LoaderParams(400, 400, 40, 40, "enemy"));

            // Load bullet texture and sound
            if (!TextureManager.Instance.Load("Resources/spr_playerbullet.png", "bullet", Game.Instance.GetRenderer))
            {
                return false;
            }
            if (!SoundManager.Instance.Load("Resources/sound/laser1.wav", "shoot", sound_type.SOUND_SFX))
            {
                return false;
            }

            // Load enemy spawner textures
            if (!TextureManager.Instance.Load("Resources/spr_antspawner_strip44.png", "spawner", Game.Instance.GetRenderer))
            {
                return false;
            }
            SDLGameObject spawner = new EnemySpawner(new LoaderParams(400, 400, 38, 36, "spawner"));

            gameObjects.Add(player);
            gameObjects.Add(spawner);
            //gameObjects.Add(enemy);
            //gameObjects.Add(enemy1);
#if DEBUG
            Console.WriteLine("Entering Playstate");
#endif
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
#if DEBUG
            Console.WriteLine("Exiting Playstate");
#endif
            return true;
        }

        private bool checkCollision(SDLGameObject enemy, SDLGameObject other)
        {
            SDL.SDL_bool bo = SDL.SDL_bool.SDL_FALSE;
            SDL.SDL_Rect result;
            SDL.SDL_Rect enemyRect = enemy.getRect;
            SDL.SDL_Rect otherRect = other.getRect;

            bo = SDL.SDL_IntersectRect(ref enemyRect, ref otherRect, out result);
#if DEBUG
            // Console.WriteLine("X: "+result.x+", Y: "+result.y+"\nW: "+result.w+", H: "+result.h);
#endif

            if (bo == SDL.SDL_bool.SDL_TRUE)
            {
                other.OnCollision(enemy.damage);

                return true;
            }
            else
            {
                return false;
            }
        }

        public override string GetStateID()
        {
            return menuID;
        }
    }
}
