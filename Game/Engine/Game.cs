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

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
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
        private IntPtr Window;

        /// <summary>
        /// The renderer.
        /// </summary>
        private IntPtr renderer;

        /// <summary>
        /// Gets the get renderer.
        /// </summary>
        /// <value>The get renderer.</value>
        public IntPtr getRenderer
        {
            get { return renderer; }
        }

        /// <summary>
        /// Events.
        /// </summary>
        public SDL.SDL_Event Events;

        /// <summary>
        /// The game objects.
        /// </summary>
        public List<GameObject> gameObjects;


        /// <summary>
        /// Draw this instance.
        /// </summary>
        public void Draw()
        {
            foreach (GameObject gObject in gameObjects)
            {
                gObject.Draw();
            }
        }

        /// <summary>
        /// Polls the events.
        /// </summary>
        /// <returns><c>true</c>, if there is events left, <c>false</c> otherwise.</returns>
        public bool PollEvents
        {
            get
            {
                if (SDL.SDL_PollEvent(out Events) != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="src.Game"/> class.
        /// </summary>
        private Game() { }

        /// <summary>
        /// Start SDL, Window, Renderer and other stuff
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="w">The width.</param>
        /// <param name="h">The height.</param>
        /// <param name="fullscreen">If set to <c>true</c> fullscreen.</param>
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
            else
            {
                Console.WriteLine("SDL started");
                // Create window
                Window = SDL.SDL_CreateWindow(title, x, y, w, h, flags);
                if (Window == IntPtr.Zero)
                {
                    Console.WriteLine("Could not create window: " + SDL.SDL_GetError());
                }
                else
                {
                    Console.WriteLine("Window started");
                    // Create Renderer
                    renderer = SDL.SDL_CreateRenderer(Window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
                    if (renderer == IntPtr.Zero)
                    {
                        Console.WriteLine("Could not create renderer: " + SDL.SDL_GetError());
                    }
                    else
                    {
                        Console.WriteLine("Renderer started");
                        IsRunning = true;
                        SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 0);
                    }
                }
            }
            InputHandler.Instance.InitJoysticks();
            gameObjects = new List<GameObject>();
            gameObjects.Add(new Player(new LoaderParams(100, 100, 55, 55, "player")));
        }

        /// <summary>
        /// Update instances.
        /// </summary>
        public void Update()
        {
            foreach (GameObject gObject in gameObjects)
            {
                gObject.Update();
            }
        }

        /// <summary>
        /// Handles events.
        /// </summary>
        public void HandleEvents()
        {
            InputHandler.Instance.Update();
        }

        /// <summary>
        /// Render instances.
        /// </summary>
        public void Render()
        {
            // Render to window
            SDL.SDL_RenderClear(renderer);

            // Loads all objets into renderer
            foreach (GameObject gObject in gameObjects)
            {
                gObject.Draw();
            }

            // Render everything
            SDL.SDL_RenderPresent(renderer);
        }


        /// <summary>
        /// Free everything from memory and closes SDL.
        /// </summary
        public void Close()
        {
            Console.WriteLine("Closing game");
            // Free stuff from memory
            InputHandler.Instance.Clean();
            SDL.SDL_DestroyWindow(Window);
            SDL.SDL_DestroyRenderer(renderer);
            SDL.SDL_Quit();
        }
    }



}

