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
		public int x;
		public int y;
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
		// Sprite sprite;
        
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

		}

		private void attack() {
		}

		public void OnEvent(SDL.SDL_Event e) {
			// map keyboard & controller

		}

	}
}
