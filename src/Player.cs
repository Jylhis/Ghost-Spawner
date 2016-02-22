// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace src
{
    class Player : Entity
    {
		
		new string sprite = "Resources/Player.bmp";
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
			// map keyboard & controller
		}
        
    }
}
