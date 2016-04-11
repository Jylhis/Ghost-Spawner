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
        public EnemySpawner(LoaderParams pParams)
            : base(ref pParams)
        {
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
            currentFrame = (int)(((SDL.SDL_GetTicks()) / 100) % 8);
            base.Update();
        }
    }
}

