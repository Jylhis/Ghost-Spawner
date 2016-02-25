﻿// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using SDL2;

namespace src
{

    public class Player : SDLGameObject
    {
        public Player(LoaderParams pParams) : base(ref pParams)
        {
            TextureManager.Instance.Load("Resources/Player.bmp", "player", Game.Instance.getRenderer);
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            velocity.X = 0;
            velocity.Y = 0;
            handleInput();

            currentFrame = (int)(((SDL.SDL_GetTicks()) / 100) % 2);
            base.Update();
        }

        private void handleInput()
        {
            // Keyboard
            if (InputHandler.Instance.isKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_W))
            {
                velocity.Y = -2;
            }
            if (InputHandler.Instance.isKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_A))
            {
                velocity.X = -2;
            }
            if (InputHandler.Instance.isKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_S))
            {
                velocity.Y = 2;
            }
            if (InputHandler.Instance.isKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_D))
            {
                velocity.X = 2;
            }

            // Joystick / Controller
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

        public override void Clean() { }
    }
}
