// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using SDL2;
using System.Collections.Generic;

namespace src
{
	public class TextureManager
	{
		// Texture Manager singleton
		private static TextureManager instance;
		private TextureManager (){}

		public static TextureManager Instance {
			get {
				if (instance == null) {
					instance = new TextureManager();
				}
				return instance;
			}
		}

		private Dictionary<string, IntPtr> textureDict = new Dictionary<string, IntPtr> ();

		/// <summary>
		/// Loads the texture.
		/// </summary>
		/// <returns><c>true</c>, if texture was loaded, <c>false</c> otherwise.</returns>
		/// <param name="path">Path.</param>
		/// <param name="id">Identifier.</param>
		/// <param name="Renderer">Renderer.</param>
		public bool Load (string path, string id, IntPtr Renderer)
		{
			Console.WriteLine ("LoadTexture: path: " + path + ", id: " + id);
			IntPtr tempSurface = SDL.SDL_LoadBMP (path);
			if (tempSurface == IntPtr.Zero) {
				Console.WriteLine (" - SDL_LoadBMP Error: " + SDL.SDL_GetError ());
				return false;
			} else {
				Console.WriteLine ("BMP loaded");
			}

			IntPtr Texture = SDL.SDL_CreateTextureFromSurface (Renderer, tempSurface);
			SDL.SDL_FreeSurface (tempSurface);
			if (Texture != IntPtr.Zero) {
				textureDict [id] = Texture;

				if (textureDict [id] != IntPtr.Zero) {
					Console.WriteLine ("Texture putted in Dict");
				} else {
					Console.WriteLine ("Error putting texture int dict");
				}
				return true;
			}
			Console.WriteLine ("Something Wrong in loadTexture");
			return false;
		}

		/// <summary>
		/// Draws the texture.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="w">The width.</param>
		/// <param name="h">The height.</param>
		/// <param name="Renderer">Renderer.</param>
		/// <param name="flip">Flip.</param>
		public void Draw (string id, int x, int y, int w, int h, IntPtr Renderer, SDL.SDL_RendererFlip flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE)
		{
			Console.WriteLine ("DrawTexture");
			SDL.SDL_Rect srcRect;
			SDL.SDL_Rect destRect;

			srcRect.x = 0;
			srcRect.y = 0;
			srcRect.w = destRect.w = w;
			srcRect.h = destRect.h = h;
			destRect.x = x;
			destRect.y = y;

			SDL.SDL_RenderCopyEx (Renderer, textureDict [id], ref srcRect, ref destRect, 0.0, IntPtr.Zero, flip);
		}

		/// <summary>
		/// Draws the frame.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="w">The width.</param>
		/// <param name="h">The height.</param>
		/// <param name="currentRow">Current row.</param>
		/// <param name="currentFrame">Current frame.</param>
		/// <param name="Renderer">Renderer.</param>
		/// <param name="flip">Flip.</param>
		public void DrawFrame (string id, int x, int y, int w, int h, int currentRow, int currentFrame, IntPtr Renderer, SDL.SDL_RendererFlip flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE)
		{
			Console.WriteLine ("DrawFrame string:{0} x:{1}, y:{2}",id,x,y);
			SDL.SDL_Rect srcRect;
			SDL.SDL_Rect destRect;

			srcRect.x = w * currentFrame;
			srcRect.y = h * (currentRow - 1);
			srcRect.w = destRect.w = w;
			srcRect.h = destRect.h = h;
			destRect.x = x;
			destRect.y = y; 

			SDL.SDL_RenderCopyEx (Renderer, textureDict [id], ref srcRect, ref destRect, 0.0, IntPtr.Zero, flip);
		}
	}
}

