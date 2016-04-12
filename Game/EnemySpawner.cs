/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 11.04.2016
 */

using System;
using SDL2;

namespace src
{
    public class EnemySpawner : SDLGameObject
    {
        private UInt32 starttime, ftime;

        public EnemySpawner(LoaderParams pParams)
            : base(ref pParams)
        {
            starttime = SDL.SDL_GetTicks();
        }

        public override void Draw()
        {
            TextureManager.Instance.DrawFrame(id,
                (int)Position.X, (int)Position.Y,
                W, H, currentRow, currentFrame,
                Game.Instance.GetRenderer, SDL.SDL_RendererFlip.SDL_FLIP_NONE, 0);
        }

        public override void Update()
        {

            ftime = SDL.SDL_GetTicks();
            currentFrame = (int)(((SDL.SDL_GetTicks()) / 100) % 44);
#if DEBUG
            Console.WriteLine("ftime: " + ftime + "\nstarttime: " + starttime+ "\nTime: "+ (ftime - starttime));
#endif
            if (ftime - starttime >= 3000)
            {
                Spawn();
                starttime = SDL.SDL_GetTicks();
            }
            
            base.Update();
        }
       /* public void Spawn(int x, int y, int width, int height)
        {

            SDLGameObject enemy = new Enemy(new LoaderParams(x, y, width, height, "enemy"));
            PlayState.gameObjects.Add(enemy);
        }*/
        public void Spawn()
        {
            SDLGameObject enemy = new Enemy(new LoaderParams(rect.x, rect.y, 40, 40, "enemy"));
            PlayState.gameObjects.Add(enemy);
        }
    }
}

