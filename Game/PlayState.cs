/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen & Veeti Karttunen
 *
 * This is part of Object-Oriented and GUI Programming course assignment
 * and is licensed under MIT license, see LICENSE file.
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
                    Bullet tmpBul = (Bullet)gameObjects[i];
                    if (!tmpBul.IsMoving)
                    {
                        gameObjects.Remove(gameObjects[i]);
                    }
                }
                else if (gameObjects[i] is Enemy)
                {
                    Enemy tmpEne = (Enemy)gameObjects[i];
                    if(tmpEne.IsKill)
                    {
                        SoundManager.Instance.PlaySound("die");
                        gameObjects.Remove(gameObjects[i]);
                    }
                    else
                    {
                        if (gameObjects.Find(x => x.id == "player") == null)
                        {
                            break;
                        }
                        if (checkCollision(gameObjects[i], gameObjects.Find(x => x.id == "player")))
                        {

                        }
                        if (gameObjects.Find(x => x.id == "bullet") != null)
                        {
                            foreach (var bullet in gameObjects.FindAll(x => x.id == "bullet"))
                            {
                                if (checkCollision(bullet, gameObjects[i]))
                                {
#if DEBUG
                                    Console.WriteLine("Bullet - Enemy Collision");
#endif
                                    gameObjects.Remove(bullet);

                                }
                            }
                        }
                    }
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
            // if (!TextureManager.Instance.Load("Resources/spr_suicidebomber_strip4.png", "enemy", Game.Instance.GetRenderer))
            if (!TextureManager.Instance.Load("Resources/tmpEne.png", "enemy", Game.Instance.GetRenderer))
            {
                return false;
            }

            // Load bullet texture
            if (!TextureManager.Instance.Load("Resources/spr_playerbullet.png", "bullet", Game.Instance.GetRenderer))
            {
                return false;
            }

            // Load enemy spawner textures
            if (!TextureManager.Instance.Load("Resources/spr_antspawner_strip44.png", "spawner", Game.Instance.GetRenderer))
            {
                return false;
            }
            SDLGameObject spawner = new EnemySpawner(new LoaderParams(400, 400, 38, 36, "spawner"));

            // Sounds
            if (!SoundManager.Instance.Load("Resources/sound/laser2.wav", "shoot", sound_type.SOUND_SFX))
            {
                return false;
            }
            if (!SoundManager.Instance.Load("Resources/sound/explode1.wav", "getHit", sound_type.SOUND_SFX))
            {
                return false;
            }
            if (!SoundManager.Instance.Load("Resources/sound/singlespawn1.wav", "die", sound_type.SOUND_SFX))
            {
                return false;
            }
            if (!SoundManager.Instance.Load("Resources/sound/idler.wav", "spawn", sound_type.SOUND_SFX))
            {
                return false;
            }

            gameObjects.Add(player);
            gameObjects.Add(spawner);
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
