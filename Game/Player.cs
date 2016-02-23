// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace src
{
    class Player : GameObject
    {
        IntPtr Texture;
		int maxVel = 10;
	
		public Player(ref IntPtr renderer, ref Game game) {
			
			sizePos.w = 55;
			sizePos.h = 55;

            game.LoadTexture("Resources/Player.bmp", "player", game.Renderer);
		}

        public void Draw()
        {
            base.Draw();
            
        }
        void Update(SDL.SDL_Event e)
        {
            base.Update();

            //If a key was pressed
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
            }
        }
        void Clean()
        {
            base.Clean();
        }
        
    }
}
