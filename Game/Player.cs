/*
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
        DOWN
    }
    public class Player : SDLGameObject
    {
        private int health = 100;

        public Player(LoaderParams pParams)
            : base(ref pParams)
        {
        }

        public void TakeDamage(int damage)
        {
            Console.WriteLine(health);
            health -= damage;
            if(health <= 0)
            {
                Game.Instance.GetStateMachine.Change(new GameOverState());
            }
        }

        public override void Draw()
        {
            double ang;
            if (velocity.Y < 0 && velocity.X == 0)
            {
                ang = -90.0;
            }
            else if (velocity.Y > 0 && velocity.X == 0)
            {
                ang = 90.0;
            }
            else
            {
                ang = 0;
            }


            if (velocity.X < 0)
            {
                if (velocity.Y < 0)
                    ang = 45;
                if (velocity.Y > 0)
                    ang = -45;

                TextureManager.Instance.DrawFrame(id,
                    (int)Position.X, (int)Position.Y,
                    W, H, currentRow, currentFrame,
                    Game.Instance.GetRenderer, SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL, ang);

            }
            else
            {
                if (velocity.Y < 0 && velocity.X != 0)
                    ang = 315;
                if (velocity.Y > 0 && velocity.X != 0)
                    ang = -315;

                TextureManager.Instance.DrawFrame(id,
                    (int)Position.X, (int)Position.Y,
                    W, H, currentRow, currentFrame,
                    Game.Instance.GetRenderer, SDL.SDL_RendererFlip.SDL_FLIP_NONE, ang);
            }
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

        public override void OnCollision()
        {
            
             rect.x += (int)velocity.X * -30;
             rect.y += (int)velocity.Y * -30;
        }

        public void Shoot(Direction d)
        {

            SDLGameObject bullet = new Bullet(new LoaderParams((int)Position.X + rect.w / 2, (int)Position.Y + rect.w / 2, 4, 4, "bullet"), d);
            PlayState.gameObjects.Add(bullet);
        }
        ~Player()
        {
            Console.WriteLine("Player Deconstructor");
        }
    }
}
