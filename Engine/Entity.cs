// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace Engine
{
    class Entity
    {
		public struct size {
			public static int w;
			public static int h;
		}
		public struct pos {
			public int x;
			public int y;
		}

		public struct vel {
			public static int x;
			public static int y;
		}
		public int maxVel;
		protected int level;
		protected int health;
        public int Defense;
        public int Attack;
		protected bool IsKill;
		protected string sprite = "character.bmp";
		public IntPtr Texture;
		public bool flip = false;


		public Entity(){
		}


        /// <summary>
        /// Gets or sets the health. If set 0 or under character dies.
        /// </summary>
        /// <value>The health.</value>
		public int Health{
            get{return health;}
            set{
                if (value <= 0){
                    IsKill = true;
                    health = 0;
                }
            }
        }

		/// <summary>
		/// Increase level by 1.
		/// </summary>
		public void LevelUp(){
			level++;
		}

		private void attack(Entity other) {
			other.health -= this.Attack-other.Defense;
		}

	}
}
