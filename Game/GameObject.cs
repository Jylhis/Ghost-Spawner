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
        public struct vel
        {
            public static int x;
            public static int y;
        }
        public void Draw() { }
        public void Update() { }
        public void Clean() { }

    }
}
