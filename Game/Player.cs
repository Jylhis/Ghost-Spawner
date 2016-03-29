/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 22.02.2016
 */
using SDL2;

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
        private const int health = 100;

        public Player(LoaderParams pParams)
            : base(ref pParams)
        {
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

        public void Shoot(Direction d)
        {
            SDLGameObject bullet = new Bullet(new LoaderParams((int)position.X + w / 2, (int)position.Y + w / 2, 4, 4, "bullet"), d);
            PlayState.gameObjects.Add(bullet);
        }

        public override void Clean()
        {
        }
    }
}
