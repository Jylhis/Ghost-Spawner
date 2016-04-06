/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 26.02.2016
 */
using System;
using System.Collections.Generic;

namespace src
{
    public class GameOverState : GameState
    {
        private List<SDLGameObject> gameObjects = new List<SDLGameObject>();
        private const string menuID = "GAMEOVER";

        private static void gameOverToMain()
        {
            Console.WriteLine("Pressed main menu");
            Game.Instance.GetStateMachine.Change(new MenuState());
        }

        private static void restartPlay()
        {
            Console.WriteLine("Pressed restart");
            Game.Instance.GetStateMachine.Change(new PlayState());
        }

        public GameOverState()
        {
        }

        public override void Update()
        {
            foreach(SDLGameObject obj in gameObjects)
            {
                obj.Update();
            }
        }

        public override void Render()
        {
            foreach (SDLGameObject obj in gameObjects)
            {
                obj.Draw();
            }
        }

        public override bool OnEnter()
        {
            if (!TextureManager.Instance.Load("Resources/Main.bmp",
                    "mainbutton", Game.Instance.GetRenderer))
            {
                return false;
            }

            if (!TextureManager.Instance.Load("Resources/Restart.bmp",
                    "restartbutton", Game.Instance.GetRenderer))
            {
                return false;
            }

            SDLGameObject button1 = new MenuButton(new LoaderParams(320, 400, 380, 203, "mainbutton"), gameOverToMain);
            SDLGameObject button2 = new MenuButton(new LoaderParams(320, 100, 380, 203, "restartbutton"), restartPlay);

            gameObjects.Add(button1);
            gameObjects.Add(button2);
#if DEBUG
            Console.WriteLine("Entering GameOverState");
#endif
            return true;
        }

        public override bool OnExit()
        {
            gameObjects.Clear();
            TextureManager.Instance.ClearFromTextureMap("mainbutton");
            TextureManager.Instance.ClearFromTextureMap("restartbutton");

            InputHandler.Instance.Reset();
#if DEBUG
            Console.WriteLine("Exiting GameOverState");
#endif
            return true;
        }

        public override string GetStateID()
        {
            return menuID;
        }
    }
}

