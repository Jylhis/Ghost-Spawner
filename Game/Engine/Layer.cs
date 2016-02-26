// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;

namespace src
{
    public abstract class Layer
    {
        /// <summary>
        /// Render this instance.
        /// </summary>
        public abstract void render();

        /// <summary>
        /// Update this instance.
        /// </summary>
        public abstract void update();

    }
}

