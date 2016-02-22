// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
using Engine;

namespace src
{
    class Player : Entity
    {
		new string sprite = "Resources/Player.bmp";
		new int maxVel = 10;
	
		public Player(ref IntPtr renderer) {
			
			size.w = 55;
			size.h = 55;
			
			IntPtr bmp = SDL.SDL_LoadBMP (sprite);
			if (bmp == IntPtr.Zero) {
				Console.WriteLine (" - SDL_LoadBMP Error: " + SDL.SDL_GetError());
			}

			Texture = SDL.SDL_CreateTextureFromSurface (renderer, bmp);
			SDL.SDL_FreeSurface (bmp);
			if (Texture == IntPtr.Zero) {
				Console.WriteLine (" - SDL_CreateTextureFromSurface Error: " + SDL.SDL_GetError());
			}
		}

		public void OnEvent(SDL.SDL_Event e) {
			//If a key was pressed
			if( e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.repeat == 0 )
			{
				//Adjust the velocity
				switch( e.key.keysym.sym )
				{
				case SDL.SDL_Keycode.SDLK_UP: vel.y -= maxVel; break;
				case SDL.SDL_Keycode.SDLK_DOWN: vel.y += maxVel; break;
				case SDL.SDL_Keycode.SDLK_LEFT: vel.x -= maxVel; break;
				case SDL.SDL_Keycode.SDLK_RIGHT: vel.x += maxVel; break;
				}
			}
			//If a key was released
			else if( e.type == SDL.SDL_EventType.SDL_KEYUP && e.key.repeat == 0 )
			{

				//Adjust the velocity
				switch( e.key.keysym.sym )
				{ 
				case SDL.SDL_Keycode.SDLK_UP: vel.y += maxVel; break;
				case SDL.SDL_Keycode.SDLK_DOWN: vel.y -= maxVel; break;
				case SDL.SDL_Keycode.SDLK_LEFT: vel.x += maxVel; break;
				case SDL.SDL_Keycode.SDLK_RIGHT: vel.x -= maxVel; break;
				}
			}
		}
        
    }
}
