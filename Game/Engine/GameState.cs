/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 25.02.2016
 */
using System;
using SDL2;

namespace src
{
    public abstract class GameState
    {
        /// <summary>
        /// Update this instance.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Render this instance.
        /// </summary>
        public abstract void Render();

        /// <summary>
        /// On enter.
        /// </summary>
        /// <returns><c>true</c>, if enter was success, <c>false</c> otherwise.</returns>
        public abstract bool OnEnter();

        /// <summary>
        /// On exit.
        /// </summary>
        /// <returns><c>true</c>, if exit was success, <c>false</c> otherwise.</returns>
        public abstract bool OnExit();

        /// <summary>
        /// Gets the state ID.
        /// </summary>
        /// <returns>The state ID.</returns>
        public abstract string GetStateID();
    }
}
