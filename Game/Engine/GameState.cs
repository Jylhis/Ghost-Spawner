// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using SDL2;

namespace src
{
    public abstract class GameState
    {
        /// <summary>
        /// Update this instance.
        /// </summary>
        public abstract void update();

        /// <summary>
        /// Render this instance.
        /// </summary>
        public abstract void render();

        /// <summary>
        /// Ons the enter.
        /// </summary>
        /// <returns><c>true</c>, if enter was oned, <c>false</c> otherwise.</returns>
        public abstract bool onEnter();

        /// <summary>
        /// Ons the exit.
        /// </summary>
        /// <returns><c>true</c>, if exit was oned, <c>false</c> otherwise.</returns>
        public abstract bool onExit();

        /// <summary>
        /// Gets the state I.
        /// </summary>
        /// <returns>The state I.</returns>
        public abstract string getStateID();
    }
}
