/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen & Veeti Karttunen
 *
 * This is part of Object-Oriented and GUI Programming course assignment
 * and is licensed under MIT license, see LICENSE file.
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

            if (ftime - starttime >= 3000)
            {
                Spawn();
                starttime = SDL.SDL_GetTicks();
            }

            base.Update();
        }

        public void Spawn()
        {
            SDLGameObject enemy = new Enemy(new LoaderParams(rect.x, rect.y, 40, 40, "enemy"));
            PlayState.gameObjects.Add(enemy);
        }
    }
}

