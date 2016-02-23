// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace src
{
    public class GameObject
    {
        protected SDL.SDL_Rect sizePos;
        protected int currentFrame;
        protected int currentRow;
        protected string id;
        public struct vel
        {
            public static int x;
            public static int y;
        }
        public GameObject(int x, int y, int h, int w, string tid)
        {
            Load(x, y, h, w, tid);
        }
        public void Load(int x, int y, int h, int w, string tid) {
            sizePos.x = x;
            sizePos.y = y;
            sizePos.w = w;
            sizePos.h = h;
            id = tid;

            currentRow = 1;
            currentFrame = 1;
        }
        public void Draw( ref Game game) {
            game.DrawFrame(id, sizePos.x, sizePos.y, sizePos.w, sizePos.h, currentRow, currentFrame);
        }
        public void Update() {
           // sizePos.x += 1;

        }
        public void Clean() { }

    }
}
