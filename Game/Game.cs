// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using SDL2;
using System.Collections.Generic;

namespace src
{
	/// <summary>
	/// Handles start and shutdown of SDL. Renders and events.
	/// </summary>
	public class Game
	{
		/// <summary>
		/// True if program is running.
		/// </summary>
		public bool IsRunning = false;
		/// <summary>
		/// The window.
		/// </summary>
		public IntPtr Window;
		/// <summary>
		/// The renderer.
		/// </summary>
		public IntPtr Renderer;
		/// <summary>
		/// Events.
		/// </summary>
		public SDL.SDL_Event Events;
		/// <summary>
		/// The current frame.
		/// </summary>
		public int currentFrame;
		/// <summary>
		/// The texture dict.
		/// </summary>
		public Dictionary<string, IntPtr> textureDict;


		/// <summary>
		/// Polls the events.
		/// </summary>
		/// <returns><c>true</c>, if there is events left, <c>false</c> otherwise.</returns>
		public bool PollEvents { 
			get {
				if (SDL.SDL_PollEvent (out Events) != 0) {
					return true;
				} else {
					return false;
				} 
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="src.Game"/> class.
		/// </summary>
		public Game (string title, int x, int y, int w, int h, bool fullscreen)
		{
			SDL.SDL_WindowFlags flags = 0;
			if (fullscreen) {
				flags = SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;
			}
			Console.WriteLine ("Constructor");
			// Start SDL
			if (SDL.SDL_Init (SDL.SDL_INIT_VIDEO) != 0) {
				Console.WriteLine ("Could not start SDL: " + SDL.SDL_GetError ());
			} else {
				Console.WriteLine ("SDL started");
				// Create window
				Window = SDL.SDL_CreateWindow (title, x, y, w, h,flags);
				if (Window == IntPtr.Zero) {
					Console.WriteLine ("Could not create window: " + SDL.SDL_GetError ());
				} else {
					Console.WriteLine ("Window started");
					// Create Renderer
					Renderer = SDL.SDL_CreateRenderer (Window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
					if (Renderer == IntPtr.Zero) {
						Console.WriteLine ("Could not create renderer: " + SDL.SDL_GetError ());
					} else {
						Console.WriteLine ("Renderer started");
						IsRunning = true;
						SDL.SDL_SetRenderDrawColor (Renderer, 0, 0, 0, 0);
					}
				}
			}
			textureDict = new Dictionary<string, IntPtr> ();
		}

		/// <summary>
		/// Update this instance.
		/// </summary>
		public void Update ()
		{
			//Console.WriteLine ("Update");
			currentFrame = (int)((SDL.SDL_GetTicks () / 100) % 2);
			//currentFrame = 1;
		}

		/// <summary>
		/// Handles the events.
		/// </summary>
		public void HandleEvents ()
		{
			//Console.WriteLine ("HandleEvents");
			// Handle events
			if (PollEvents) {
				switch (Events.type) {
				case SDL.SDL_EventType.SDL_QUIT:
					Console.WriteLine ("Event QUIT");
					IsRunning = false;
					break;
				case SDL.SDL_EventType.SDL_KEYDOWN:
					Console.WriteLine ("Event Key_DOWN");
					break;
				case SDL.SDL_EventType.SDL_KEYUP:
					Console.WriteLine ("Event Key_UP");
					break;
				default:
					break;
				}
			}
		}

		/// <summary>
		/// Render this instance.
		/// </summary>
		public void Render ()
		{
			//Console.WriteLine ("Render");

			// Render to window
			SDL.SDL_RenderClear (Renderer);

			DrawTexture ("player", 0, 0, 55, 55);
			DrawFrame ("player", 100, 0, 55, 55,1,currentFrame );
			// TODO: Put everything in Renderer

			SDL.SDL_RenderPresent (Renderer);
		}

		/// <summary>
		/// Loads the texture.
		/// </summary>
		/// <returns><c>true</c>, if texture was loaded, <c>false</c> otherwise.</returns>
		/// <param name="path">Path.</param>
		/// <param name="id">Identifier.</param>
		public bool LoadTexture (string path, string id)
		{
			Console.WriteLine ("LoadTexture: path: "+path+", id: "+id);
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
		/// <param name="flip">Flip.</param>
		public void DrawTexture (string id, int x, int y, int w, int h, SDL.SDL_RendererFlip flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE)
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

			SDL.SDL_RenderCopyEx (Renderer, textureDict[id], ref srcRect, ref destRect, 0.0, IntPtr.Zero, flip);
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
		/// <param name="flip">Flip.</param>
		public void DrawFrame (string id, int x, int y, int w, int h, int currentRow, int currentFrame, SDL.SDL_RendererFlip flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE)
		{
			Console.WriteLine ("DrawFrame");
			SDL.SDL_Rect srcRect;
			SDL.SDL_Rect destRect;

			srcRect.x = w * currentFrame;
			srcRect.y = h * (currentRow - 1);
			srcRect.w = destRect.w = w;
			srcRect.h = destRect.h = h;
			destRect.x = x;
			destRect.y = y; 

			SDL.SDL_RenderCopyEx (Renderer, textureDict[id], ref srcRect, ref destRect, 0.0, IntPtr.Zero, flip);
		}

		/// <summary>
		/// Free everything from memory and closes SDL.
		/// </summary
		~Game() {
			Console.WriteLine ("Destructor");
			// Free stuff from memory
			SDL.SDL_DestroyWindow (Window);
			SDL.SDL_DestroyRenderer (Renderer);
			SDL.SDL_Quit ();  // Quit everything SDL
		}
	}
}

