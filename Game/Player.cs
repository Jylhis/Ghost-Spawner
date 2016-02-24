// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using SDL2;

namespace src
{
   
    public class Player : SDLGameObject
    {
        //int maxVel = 1;

        public Player(LoaderParams pParams)
            : base(ref pParams)
        {
            //Console.WriteLine ("Init: "+this);
            TextureManager.Instance.Load("Resources/Player.bmp", "player", Game.Instance.getRenderer);
        }

        public override void Draw()
        {
            //Console.WriteLine ("Calls gameObject Draw from: "+this);
            base.Draw();
        }

        public override void Update()
        {
            //Console.WriteLine ("Updated: " + this);
            velocity.X = 0;
            velocity.Y = 0;
            handleInput();

            currentFrame = (int)(((SDL.SDL_GetTicks()) / 100) % 2);
            //acceleration.X = 1;
            base.Update();
        }

        private void handleInput()
        {
            // switch case?

            // Keyboard
            if (InputHandler.Instance.isKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_W))
            {
                velocity.Y = 2;
            }
            if (InputHandler.Instance.isKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_A))
            {
                velocity.X = -2;
            }
            if (InputHandler.Instance.isKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_S))
            {
                velocity.Y = -2;
            }
            if (InputHandler.Instance.isKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_D))
            {
                velocity.X = 2;
            }


            // Joystick/Controller
            if (InputHandler.Instance.JoysticksInitialised)
            {
                if (InputHandler.Instance.xvalue(0, 1) > 0 ||
                    InputHandler.Instance.xvalue(0, 1) < 0)
                {
                    velocity.X = 1 * InputHandler.Instance.xvalue(0, 1);
                }
                if (InputHandler.Instance.yvalue(0, 1) > 0 ||
                    InputHandler.Instance.yvalue(0, 1) < 0)
                {
                    velocity.Y = 1 * InputHandler.Instance.yvalue(0, 1);
                }
                if (InputHandler.Instance.xvalue(0, 2) > 0 ||
                    InputHandler.Instance.xvalue(0, 2) < 0)
                {
                    velocity.X = 1 * InputHandler.Instance.xvalue(0, 1);
                }
                if (InputHandler.Instance.yvalue(0, 2) > 0 ||
                    InputHandler.Instance.yvalue(0, 2) < 0)
                {
                    velocity.Y = 1 * InputHandler.Instance.yvalue(0, 1);
                }
            }
        }

        public override void Clean()
        {
            Console.WriteLine("Calls gameObject Clean from: " + this);
        }
        
    }
}
