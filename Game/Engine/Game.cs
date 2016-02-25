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
        private IntPtr Window;
        private IntPtr renderer;
        private GameStateMachine gameStateMachine;

        private Game()
        {
        }

        /// <summary>
        /// True if game is running.
        /// </summary>
        public bool IsRunning = false;

        /// <summary>
        /// The game objects.
        /// </summary>
        public List<GameObject> gameObjects;

        public GameStateMachine getStateMachine
        {
            get
            {
                return gameStateMachine;
            }
        }

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
        /// Gets the renderer.
        /// </summary>
        /// <value>The get renderer.</value>
        public IntPtr getRenderer
        {
            get { return renderer; }
        }

        /// <summary>
        /// Initialize SDL, window, renderer, controller and everything.
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

            gameStateMachine = new GameStateMachine();

            gameStateMachine.changeState(new MenuState());

            InputHandler.Instance.InitJoysticks();
            gameObjects = new List<GameObject>();
        }

        /// <summary>
        /// Handles the events.
        /// </summary>
        public void HandleEvents()
        {
            InputHandler.Instance.Update();

            if (InputHandler.Instance.isKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_RETURN))
            {
                gameStateMachine.changeState(new PlayState());
            }
        }

        /// <summary>
        /// Update.
        /// </summary>
        public void Update()
        {
            /*foreach (GameObject gObject in gameObjects)
            {
                gObject.Update();
            }*/
            gameStateMachine.update();
        }

        /// <summary>
        /// Render.
        /// </summary>
        public void Render()
        {
            // Render to window
            SDL.SDL_RenderClear(renderer);

            gameStateMachine.render();
            // Loads all objets into renderer
            /* foreach (GameObject gObject in gameObjects)
            {
                gObject.Draw();
            }*/

            // Render everything
            SDL.SDL_RenderPresent(renderer);
        }

        /// <summary>
        /// Close everything.
        /// </summary>
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

