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
    public class Enemy : SDLGameObject
    {
        private const int health = 100;

        public Enemy(LoaderParams pParams)
            : base(ref pParams)
        {
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            currentFrame = (int)(((SDL.SDL_GetTicks()) / 100) % 4);
            base.Update();
        }

        public override void Clean()
        {
            base.Clean();
        }
    }
}

