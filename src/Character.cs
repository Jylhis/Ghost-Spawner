// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace src
{
    class Character
    {
		// Coordinates
		public SDL.SDL_Rect size;
		private struct velocity {
			public static int x;
			public static int y;
		}
        private int level;
        private int health;
        public int Defense;
        public int Attack;
        private bool IsKill;
        private Weapon weapon;
		private string sprite = "player.bmp";


		public Character() {
			IntPtr bmp = SDL.SDL_LoadBMP (sprite);
			// FIXME: SDL.SDL_CreateTexture (src.Program.renderer, bmp, SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STATIC, size.w, size.h);
		}
        
		public int Health{
            get{return health;}
            set{
                if (value <= 0){
                    IsKill = true;
                    health = 0;
                }
            }
        }

		public void LevelUp(){
			level++;
		}

		public void Render() {
			// Render sprite
		}

		private void attack() {
		}

		public void OnEvent(SDL.SDL_Event e) {
			// map keyboard & controller


		}

	}
}
