﻿/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 22.02.2016
 */
using SDL2;
using System;

namespace src
{
    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
        DOLE,
        DORI,
        UPLE,
        UPRI
    }
    public class Player : SDLGameObject
    {
        private const int health = 100;
        private Vector2D lastVelocity = new Vector2D(0, 0);
        private UInt32 startTime;

        public Player(LoaderParams pParams)
            : base(ref pParams)
        {
        }

        public override void Draw()
        {
            double ang;

            if (velocity.X > 0)
            {

                if (velocity.Y > 0)
                {
                    ang = 45;
                    lastVelocity.X = 1;
                    lastVelocity.Y = 1;
                }
                else if (velocity.Y == 0)
                {
                    ang = 0;
                    lastVelocity.X = 1;
                    lastVelocity.Y = 0;
                }
                else
                {
                    ang = -45;
                    lastVelocity.X = 1;
                    lastVelocity.Y = -1;
                }
            }

            else if (velocity.X < 0)
            {
                if (velocity.Y > 0)
                {
                    ang = 135;
                    lastVelocity.X = -1;
                    lastVelocity.Y = 1;
                }
                else if (velocity.Y == 0)
                {
                    ang = 180;
                    lastVelocity.X = -1;
                    lastVelocity.Y = 0;
                }
                else
                {
                    ang = -135;
                    lastVelocity.X = -1;
                    lastVelocity.Y = -1;
                }
            }

            else
            {
                if (velocity.Y > 0)
                {
                    ang = 90;
                    lastVelocity.X = 0;
                    lastVelocity.Y = 1;
                }
                else if (velocity.Y == 0)
                {
                    if (lastVelocity.X > 0)
                    {
                        ang = 0;
                        if (lastVelocity.Y > 0)
                        {
                            ang = 45;
                        }
                        else if (lastVelocity.Y == 0)
                        {
                            ang = 0;
                        }
                        else
                        {
                            ang = -45;
                        }
                    }
                    else if (lastVelocity.X == 0)
                    {
                        ang = 0;
                        if (lastVelocity.Y > 0)
                        {
                            ang = 90;
                        }
                        else if (lastVelocity.Y < 0)
                        {
                            ang = -90;
                        }
                    }
                    else
                    {
                        ang = 180;
                        if (lastVelocity.Y > 0)
                        {
                            ang = 135;
                        }
                        else if (lastVelocity.Y < 0)
                        {
                            ang = -135;
                        }
                        else
                        {
                            ang = 180;
                        }
                    }
                }
                else
                {
                    ang = -90;
                    lastVelocity.X = 0;
                    lastVelocity.Y = -1;
                }
            }

            TextureManager.Instance.DrawFrame(id,
                    (int)position.X, (int)position.Y,
                    W, H, currentRow, currentFrame,
                    Game.Instance.GetRenderer, SDL.SDL_RendererFlip.SDL_FLIP_NONE, ang);
        }

        public override void Update()
        {
            velocity.X = 0;
            velocity.Y = 0;
            handleInput();

            currentFrame = (int)(((SDL.SDL_GetTicks()) / 100) % 8);
            base.Update();
        }

        private void handleInput()
        {
            // Keyboard
            if (InputHandler.Instance.IsKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_W))
            {
                velocity.Y = -2;
            }
            if (InputHandler.Instance.IsKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_A))
            {
                velocity.X = -2;
            }
            if (InputHandler.Instance.IsKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_S))
            {
                velocity.Y = 2;
            }
            if (InputHandler.Instance.IsKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_D))
            {
                velocity.X = 2;
            }

            // Shoot
            if(SDL.SDL_GetTicks() - 170 > startTime)
            {
                if (InputHandler.Instance.IsKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_UP))
                {
                    Shoot(Direction.UP);
                }
                if (InputHandler.Instance.IsKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_DOWN))
                {
                    Shoot(Direction.DOWN);
                }
                if (InputHandler.Instance.IsKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_LEFT))
                {
                    Shoot(Direction.LEFT);
                }
                if (InputHandler.Instance.IsKeyDown(SDL.SDL_Scancode.SDL_SCANCODE_RIGHT))
                {
                    Shoot(Direction.RIGHT);
                }
            }
            

            // Joystick / Controller
            if (InputHandler.Instance.JoysticksInitialised)
            {
                if (InputHandler.Instance.Xvalue(0, 1) > 0 ||
                    InputHandler.Instance.Xvalue(0, 1) < 0)
                {
                    velocity.X = 2 * InputHandler.Instance.Xvalue(0, 1);
                }
                if (InputHandler.Instance.Yvalue(0, 1) > 0 ||
                    InputHandler.Instance.Yvalue(0, 1) < 0)
                {
                    velocity.Y = 2 * InputHandler.Instance.Yvalue(0, 1);
                }
                if (InputHandler.Instance.Xvalue(0, 2) > 0 ||
                    InputHandler.Instance.Xvalue(0, 2) < 0)
                {
                    velocity.X = 2 * InputHandler.Instance.Xvalue(0, 1);
                }
                if (InputHandler.Instance.Yvalue(0, 2) > 0 ||
                    InputHandler.Instance.Yvalue(0, 2) < 0)
                {
                    velocity.Y = 2 * InputHandler.Instance.Yvalue(0, 1);
                }
            }
        }

        public void Shoot(Direction d)
        {
            startTime = SDL.SDL_GetTicks();
            SoundManager.Instance.PlaySound("shoot");
            SDLGameObject bullet = new Bullet(new LoaderParams((int)position.X + w / 2, (int)position.Y + w / 2, 4, 4, "bullet"), d);
            PlayState.gameObjects.Add(bullet);
        }
        ~Player()
        {
            Console.WriteLine("Player Deconstructor");
        }
    }
}
