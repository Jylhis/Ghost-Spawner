// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using SDL2;

namespace src
{
    public class AnimatedGraphics : SDLGameObject
    {
        private int animSpeed;
        private int numFrames = 3;

        public AnimatedGraphics(LoaderParams pParams, int inAnimSpeed)
            : base(ref pParams)
        {
            animSpeed = inAnimSpeed;
        }

        public override void Update()
        {
            currentFrame = (int)((SDL.SDL_GetTicks() / (1000 / animSpeed)) % numFrames);
        }
    }
}

