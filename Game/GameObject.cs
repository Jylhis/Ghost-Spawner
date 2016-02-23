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

		/// <summary>
		/// Initializes a new instance of the <see cref="src.GameObject"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="h">The height.</param>
		/// <param name="w">The width.</param>
		/// <param name="tid">The id.</param>
		public GameObject (int x, int y, int h, int w, string tid)
		{
			Load (x, y, h, w, tid);
		}
			
		protected void Load (int x, int y, int h, int w, string tid)
		{
			Console.WriteLine ("Loaded: "+this);
			this.sizePos.x = x;
			this.sizePos.y = y;
			this.sizePos.w = w;
			this.sizePos.h = h;
			this.id = tid;

			currentRow = 1;
			currentFrame = 0;
		}

		/// <summary>
		/// Draw the instance.
		/// </summary>
		/// <param name="game">Game.</param>
		public void Draw ()
		{
			Console.WriteLine ("Draw: "+this);
			TextureManager.Instance.DrawFrame (this.id, this.sizePos.x, this.sizePos.y, this.sizePos.w, this.sizePos.h, this.currentRow, this.currentFrame, Program.game.Renderer);
		}

		/// <summary>
		/// Update this instance.
		/// </summary>
		public void Update ()
		{
			Console.WriteLine ("Updated: "+this);

		}

		/// <summary>
		/// Clean this instance.
		/// </summary>
		public void Clean ()
		{
			Console.WriteLine ("Cleaned: "+this);
		}

	}
}
