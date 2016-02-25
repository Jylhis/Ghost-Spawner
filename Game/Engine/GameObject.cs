// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using SDL2;

namespace src
{
    public abstract class GameObject
    {
        protected GameObject(ref LoaderParams pParams)
        {
        }

        public abstract void Draw();

        public abstract void Update();

        public abstract void Clean();
    }
}
