// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using SDL2;

namespace src
{
    public abstract class GameState
    {
        public abstract void update();

        public abstract void render();

        public abstract bool onEnter();

        public abstract bool onExit();

        public abstract string getStateID();
    }
}
