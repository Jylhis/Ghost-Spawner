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
        /// On enter.
        /// </summary>
        /// <returns><c>true</c>, if enter was success, <c>false</c> otherwise.</returns>
        public abstract bool onEnter();

        /// <summary>
        /// On exit.
        /// </summary>
        /// <returns><c>true</c>, if exit was success, <c>false</c> otherwise.</returns>
        public abstract bool onExit();

        /// <summary>
        /// Gets the state ID.
        /// </summary>
        /// <returns>The state ID.</returns>
        public abstract string getStateID();
    }
}
