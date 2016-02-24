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

        private static Game instance;

        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game();
                }
                return instance;
            }
        }
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
		private IntPtr renderer;
        public IntPtr getRenderer
        {
            get { return renderer; }
        }
		/// <summary>
		/// Events.
		/// </summary>
		public SDL.SDL_Event Events;

		public int currentFrame;

        public List<GameObject> gameObjects;
       

        public void Draw()
        {
            foreach(GameObject gObject in gameObjects)
            {
                gObject.Draw();
            }
        }

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
		private Game ()
		{
			

		}
        public void Init(string title, int x, int y, int w, int h, bool fullscreen)
        {
            SDL.SDL_WindowFlags flags = 0;
            if (fullscreen)
            {
                flags = SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;
            }
            Console.WriteLine("Constructor");
            // Start SDL
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) != 0)
            {
                Console.WriteLine("Could not start SDL: " + SDL.SDL_GetError());
            }
            else {
                Console.WriteLine("SDL started");
                // Create window
                Window = SDL.SDL_CreateWindow(title, x, y, w, h, flags);
                if (Window == IntPtr.Zero)
                {
                    Console.WriteLine("Could not create window: " + SDL.SDL_GetError());
                }
                else {
                    Console.WriteLine("Window started");
                    // Create Renderer
                    renderer = SDL.SDL_CreateRenderer(Window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
                    if (renderer == IntPtr.Zero)
                    {
                        Console.WriteLine("Could not create renderer: " + SDL.SDL_GetError());
                    }
                    else {
                        Console.WriteLine("Renderer started");
                        IsRunning = true;
                        SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 0);
                    }
                }
            }
            gameObjects = new List<GameObject>();
            gameObjects.Add(new Player(new LoaderParams(100, 100, 55, 55, "player")));
            Console.WriteLine("Player Init in Game");
        }

		/// <summary>
		/// Update this instance.
		/// </summary>
		public void Update ()
		{
            //Console.WriteLine ("Update");
            // currentFrame = (int)((SDL.SDL_GetTicks () / 100) % 2);
            foreach (GameObject gObject in gameObjects)
            {
                gObject.Update();
            }
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
			SDL.SDL_RenderClear (renderer);

            foreach (GameObject gObject in gameObjects)
            {
                gObject.Draw();
            }
            // Render all the stuff
            //TextureManager.Instance.Draw ("player", 0, 0, 55, 55, Renderer);
            //TextureManager.Instance.DrawFrame ("player", 100, 0, 55, 55, 1, currentFrame, Renderer);
            //Program.player.Draw();
			SDL.SDL_RenderPresent (renderer);
		}

	
		/// <summary>
		/// Free everything from memory and closes SDL.
		/// </summary
		public void Close ()
		{
			Console.WriteLine ("Destructor");
			// Free stuff from memory
			SDL.SDL_DestroyWindow (Window);
			SDL.SDL_DestroyRenderer (renderer);
			SDL.SDL_Quit ();  // Quit everything SDL
		}
	}


		
}

