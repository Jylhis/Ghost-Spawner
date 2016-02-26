// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using SDL2;

namespace src
{
    public class AnimatedGraphics : SDLGameObject
    {
        private int animSpeed;
        private int numFrames = 3;

        /// <summary>
        /// Initializes a new instance of the <see cref="src.AnimatedGraphics"/> class.
        /// </summary>
        /// <param name="pParams">P parameters.</param>
        /// <param name="inAnimSpeed">In animation speed.</param>
        public AnimatedGraphics(LoaderParams pParams, int inAnimSpeed)
            : base(ref pParams)
        {
            animSpeed = inAnimSpeed;
        }

        /// <summary>
        /// Update this instance.
        /// </summary>
        public override void Update()
        {
            currentFrame = (int)((SDL.SDL_GetTicks() / (1000 / animSpeed)) % numFrames);
        }
    }
}

