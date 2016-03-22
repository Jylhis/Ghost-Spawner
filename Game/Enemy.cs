// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using SDL2;

namespace src
{
    public class Enemy : SDLGameObject
    {
        public Enemy(LoaderParams pParams)
            : base(ref pParams)
        {
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            currentFrame = (int)(((SDL.SDL_GetTicks()) / 100) % 4);
            base.Update();
        }

        public override void Clean()
        {
            base.Clean();
        }
    }
}

