// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace src
{
    public class Player : GameObject
    {
		int maxVel = 10;
        Game game;
	
		public Player(int x, int y, int h, int w, string tid, ref Game peli) : base(x, y, h, w, tid) {
            // Load(x, y, h, w, tid);
            game = peli;
            game.LoadTexture("Resources/Player.bmp", "player");
           
        }

        public new void Draw()
        {
            base.Draw(ref game);

        }
        public new void Update()
        {
            

            /*If a key was pressed
            if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.repeat == 0)
            {
                //Adjust the velocity
                switch (e.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_UP: vel.y -= maxVel; break;
                    case SDL.SDL_Keycode.SDLK_DOWN: vel.y += maxVel; break;
                    case SDL.SDL_Keycode.SDLK_LEFT: vel.x -= maxVel; break;
                    case SDL.SDL_Keycode.SDLK_RIGHT: vel.x += maxVel; break;
                }
            }
            //If a key was released
            else if (e.type == SDL.SDL_EventType.SDL_KEYUP && e.key.repeat == 0)
            {

                //Adjust the velocity
                switch (e.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_UP: vel.y += maxVel; break;
                    case SDL.SDL_Keycode.SDLK_DOWN: vel.y -= maxVel; break;
                    case SDL.SDL_Keycode.SDLK_LEFT: vel.x += maxVel; break;
                    case SDL.SDL_Keycode.SDLK_RIGHT: vel.x -= maxVel; break;
                }
            }*/

        }
        public new void Clean()
        {
            base.Clean();
        }
        
    }
}
