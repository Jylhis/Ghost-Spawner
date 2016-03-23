/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 24.02.2016
 */
using System;
using SDL2;

namespace src
{
    public abstract class GameObject
    {
        protected GameObject(ref LoaderParams pParams)
        {
        }

        /// <summary>
        /// Draw this instance.
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Update this instance.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Clean this instance.
        /// </summary>
        public abstract void Clean();
    }
}
