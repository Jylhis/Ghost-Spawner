/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 24.02.2016
 */
using System;
using SDL2;

namespace src
{
    public class Game
    {
        private static Game instance;
        private IntPtr window, renderer;
        private GameStateMachine gameStateMachine = new GameStateMachine();
        private bool running = false;

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
        public GameStateMachine GetStateMachine
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
        public IntPtr GetRenderer
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
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) != 0)
            {
                Console.WriteLine("Could not start SDL: " + SDL.SDL_GetError());
            }
            else
            {
#if DEBUG
                Console.WriteLine("SDL started");
#endif
                window = SDL.SDL_CreateWindow(title, x, y, w, h, flags);
                if (window == IntPtr.Zero)
                {
                    Console.WriteLine("Could not create window: " + SDL.SDL_GetError());
                }
                else
                {
#if DEBUG
                    Console.WriteLine("Window started");
#endif
                    renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
                    if (renderer == IntPtr.Zero)
                    {
                        Console.WriteLine("Could not create renderer: " + SDL.SDL_GetError());
                    }
                    else
                    {
#if DEBUG
                        Console.WriteLine("Renderer started");
#endif
                        Game.Instance.IsRunning = true;
                        SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 0);
                    }
                }
            }

            gameStateMachine.Change(new MenuState());
            InputHandler.Instance.InitJoysticks();

            TextureManager.Instance.Load("Resources/background_robotron.bmp", "background", renderer);
        }

        /// <summary>
        /// Handles the events.
        /// </summary>
        public void HandleEvents()
        {
            InputHandler.Instance.Update();

            if (InputHandler.Instance.IsKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_RETURN))
            {
                gameStateMachine.Change(new PlayState());
            }
        }

        /// <summary>
        /// Update.
        /// </summary>
        public void Update()
        {
            gameStateMachine.Update();
        }


        /// <summary>
        /// Render.
        /// </summary>
        public void Render()
        {
            SDL.SDL_RenderClear(renderer);

            TextureManager.Instance.Draw("background", 0, 0, 1024, 720, ref renderer);

            gameStateMachine.Render();

            SDL.SDL_RenderPresent(renderer);
        }

        /// <summary>
        /// Close everything.
        /// </summary>
        public void Close()
        {
#if DEBUG
            Console.WriteLine("Closing game");
#endif
            InputHandler.Instance.Clean();
            SDL.SDL_DestroyWindow(window);
            SDL.SDL_DestroyRenderer(renderer);
            SDL.SDL_Quit();
        }
    }
}

