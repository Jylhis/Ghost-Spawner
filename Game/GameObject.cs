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
			sizePos.x = x;
			sizePos.y = y;
			sizePos.w = w;
			sizePos.h = h;
			id = tid;

			currentRow = 1;
			currentFrame = 1;
		}

		/// <summary>
		/// Draw the instance.
		/// </summary>
		/// <param name="game">Game.</param>
		public void Draw (ref Game game)
		{
			Console.WriteLine ("Draw: "+this);
			game.DrawFrame (this.id, this.sizePos.x, this.sizePos.y, this.sizePos.w, this.sizePos.h, this.currentRow, this.currentFrame);
		}

		/// <summary>
		/// Update this instance.
		/// </summary>
		public void Update ()
		{
			Console.WriteLine ("Updated: "+this);
			// sizePos.x += 1;

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
