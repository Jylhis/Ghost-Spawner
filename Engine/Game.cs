// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using SDL2;

namespace Engine
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
				Window = SDL.SDL_CreateWindow ("Peli", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, 800, 600, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
				if (Window == IntPtr.Zero) {
					Console.WriteLine ("Could not create window: " + SDL.SDL_GetError ());
				} else {

					// Create Renderer
					// https://stackoverflow.com/questions/21007329/what-is-a-sdl-renderer
					Renderer = SDL.SDL_CreateRenderer (this.Window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
					if (Renderer == IntPtr.Zero) {
						Console.WriteLine ("Could not create renderer: " + SDL.SDL_GetError ());
					} else {
						SDL.SDL_SetRenderDrawColor (Renderer, 0Xff, 0xff, 0xff, 0xff);
					}
				}
			}
		}


		/// <summary>
		/// Free everything from memory and closes SDL.
		/// </summary>
		public void Quit(){
			// Free stuff from memory
			// TODO: SDL.SDL_DestroyTexture(texture);
			SDL.SDL_DestroyRenderer(Renderer);
			SDL.SDL_DestroyWindow (Window);
			SDL.SDL_Quit ();  // Quit everything SDL
		}
	}
}

