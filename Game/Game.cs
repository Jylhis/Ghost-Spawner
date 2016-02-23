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
		public bool IsRunning = true;
		/// <summary>
		/// The renderer.
		/// </summary>
		public IntPtr Renderer;
		/// <summary>
		/// The window.
		/// </summary>
		public IntPtr Window;
		/// <summary>
		/// Events.
		/// </summary>
		public SDL.SDL_Event Events;
        public int currentFrame;
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
		public Game()
		{
			// Start SDL
			if (SDL.SDL_Init (SDL.SDL_INIT_VIDEO) != 0) {
				Console.WriteLine ("Could not start SDL: " + SDL.SDL_GetError ());
			} else {

				// Create window
				Window = SDL.SDL_CreateWindow ("Peli", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, 800, 600, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN|SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS);
				if (Window == IntPtr.Zero) {
					Console.WriteLine ("Could not create window: " + SDL.SDL_GetError ());
				} else {

					// Create Renderer
					// https://stackoverflow.com/questions/21007329/what-is-a-sdl-renderer
					Renderer = SDL.SDL_CreateRenderer (this.Window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
					if (Renderer == IntPtr.Zero) {
						Console.WriteLine ("Could not create renderer: " + SDL.SDL_GetError ());
					} 
				}
			}
            textureDict = new Dictionary<string, IntPtr>();

        }

        public void Update()
        {
            currentFrame = (int)((SDL.SDL_GetTicks() / 100) % 6);
        }

        public void HandleEvents()
        {
            // Handle events
            if (PollEvents)
            {
                switch(Events.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT: IsRunning = false; break;
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                    case SDL.SDL_EventType.SDL_KEYUP: break;
                    default: break;
                }
            }
        }

        public void Render()
        {
            // Render to window
            SDL.SDL_RenderClear(Renderer);

            DrawTexture("player", 100, 0, 55, 55, Renderer);

            SDL.SDL_RenderPresent(Renderer);
        }

        public bool LoadTexture(string path, string id, IntPtr Renderer)
        {
            IntPtr bmp = SDL.SDL_LoadBMP(path);
            if (bmp == IntPtr.Zero)
            {
                Console.WriteLine(" - SDL_LoadBMP Error: " + SDL.SDL_GetError());
            }

            IntPtr Texture = SDL.SDL_CreateTextureFromSurface(Renderer, bmp);
            SDL.SDL_FreeSurface(bmp);
            if (Texture != IntPtr.Zero)
            {
                textureDict[id] = Texture;
                return true;
            }
            return false;
        }

        public void DrawTexture(string id, int x, int y, int w, int h, IntPtr Renderer, SDL.SDL_RendererFlip flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE)
        {
            SDL.SDL_Rect srcRect;
            SDL.SDL_Rect destRect;

            srcRect.x = 0;
            srcRect.y = 0;
            srcRect.w = destRect.w = w;
            srcRect.h = destRect.h = h;
            destRect.x = x;
            destRect.y = y;

            SDL.SDL_RenderCopyEx(Renderer, textureDict[id], ref srcRect, ref destRect, 0, IntPtr.Zero, flip);
        }

        public void DrawFrame(string id, int x, int y, int w, int h, int currentRow, int currentFrame, IntPtr Renderer, SDL.SDL_RendererFlip flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE)
        {
            SDL.SDL_Rect srcRect;
            SDL.SDL_Rect destRect;

            srcRect.x = w * currentFrame;
            srcRect.y = h * (currentRow - 1);
            srcRect.w = destRect.w = w;
            srcRect.h = destRect.h = h;
            destRect.x = x;
            destRect.y = y; 

            SDL.SDL_RenderCopyEx(Renderer, textureDict[id], ref srcRect, ref destRect, 0, IntPtr.Zero, flip);
        }


        /// <summary>
        /// Free everything from memory and closes SDL.
        /// </summary>
        public void Clean(){
			// Free stuff from memory
			SDL.SDL_DestroyRenderer(Renderer);
			SDL.SDL_DestroyWindow (Window);
			SDL.SDL_Quit ();  // Quit everything SDL
		}
	}
}

