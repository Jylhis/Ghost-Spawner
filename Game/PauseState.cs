/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 25.02.2016
 */
using System;
using System.Collections.Generic;

namespace src
{
    public class PauseState : GameState
    {
        private const string menuID = "PAUSE";
        private List<SDLGameObject> gameObjects = new List<SDLGameObject>();

        private static void pauseToMain()
        {
            Console.WriteLine("PLAY!");
            Game.Instance.GetStateMachine.Pop();
            Game.Instance.GetStateMachine.Change(new MenuState());

        }

        private static void resumePlay()
        {
            Console.WriteLine("Exit!");
            Game.Instance.GetStateMachine.Pop();
        }

        public override void Update()
        {


            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }
        }

        public override void Render()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw();
            }
        }

        public override bool OnEnter()
        {
            if (!TextureManager.Instance.Load("Resources/Resume.bmp",
                    "resumebutton", Game.Instance.GetRenderer))
            {
                return false;
            }
            if (!TextureManager.Instance.Load("Resources/Main.bmp",
                    "mainmenubutton", Game.Instance.GetRenderer))
            {
                return false;
            }
            SDLGameObject button1 = new MenuButton(new LoaderParams(320, 100, 380, 203, "resumebutton"), resumePlay);
            SDLGameObject button2 = new MenuButton(new LoaderParams(320, 400, 380, 203, "mainmenubutton"), pauseToMain);
            gameObjects.Add(button1);
            gameObjects.Add(button2);

            Console.WriteLine("entering PauseState");
            return true;
        }

        public override bool OnExit()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Clean();
            }
            gameObjects.Clear();
            TextureManager.Instance.ClearFromTextureMap("resumebutton");
            TextureManager.Instance.ClearFromTextureMap("mainmenubutton");

            InputHandler.Instance.Reset();

            Console.WriteLine("Exiting Menustate");
            return true;
        }

        public override string GetStateID()
        {
            return menuID;
        }
    }
}
