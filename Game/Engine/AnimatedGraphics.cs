/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 26.02.2016
 */
using System;
using SDL2;

namespace src
{
    class AnimatedGraphics : SDLGameObject
    {
        private int animSpeed;
        private int numFrames = 3;

        /// <summary>
        /// Initializes a new instance of the <see cref="src.AnimatedGraphics"/> class.
        /// </summary>
        /// <param name="pParams">Parameters.</param>
        /// <param name="inAnimSpeed">Animation speed.</param>
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

