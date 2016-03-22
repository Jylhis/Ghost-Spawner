// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using SDL2;
using System.Collections.Generic;

namespace src
{
    public class Game
    {
        private static Game instance;
        private IntPtr Window, renderer;
        private GameStateMachine gameStateMachine;
        private bool running = false;

        private Game()
        {
        }

        /// <summary>
        /// Returns true if game is running.
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return running;
            }
            set
            {
                running = value;
            }
        }

        /// <summary>
        /// Gets state machine.
        /// </summary>
        /// <value>The get state machine.</value>
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
        /// <value>The renderer.</value>
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
                        Game.Instance.IsRunning = true;
                        SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 0);
                    }
                }
            }

            gameStateMachine = new GameStateMachine();
            gameStateMachine.changeState(new MenuState());
            InputHandler.Instance.InitJoysticks();

            //HACK
            TextureManager.Instance.Load("Resources/background_robotron.bmp", "background", renderer);
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
            gameStateMachine.update();
        }

        /// <summary>
        /// Render.
        /// </summary>
        public void Render()
        {
            // Render to window
            SDL.SDL_RenderClear(renderer);

            // background hack
            TextureManager.Instance.Draw("background", 0, 0, 1024, 720, renderer);

            // Loads all objets into renderer
            gameStateMachine.render();
                
            // Render everything
            SDL.SDL_RenderPresent(renderer);
        }

        /// <summary>
        /// Close everything.
        /// </summary>
        public void Close()
        {
            Console.WriteLine("Closing game");

            InputHandler.Instance.Clean();
            SDL.SDL_DestroyWindow(Window);
            SDL.SDL_DestroyRenderer(renderer);
            SDL.SDL_Quit();
        }
    }
}

