/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 23.03.2016
 */
using SDL2;

namespace src
{
    class Bullet : SDLGameObject
    {
        public Bullet(LoaderParams pParams)
            : base(ref pParams)
        {
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            currentFrame = (int)(((SDL.SDL_GetTicks()) / 100) % 5);
            base.Update();
        }

        public override void Clean()
        {
        }
    }
}
