﻿/*
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
    public class MenuState : GameState
    {
        private const string menuID = "MENU";
        private List<GameObject> gameObjects = new List<GameObject>();

        private static void menuToPlay()
        {
            Console.WriteLine("PLAY!");
            Game.Instance.GetStateMachine.ChangeState(new PlayState());
        }

        private static void exitFromMenu()
        {
            Console.WriteLine("Exit!");
            Game.Instance.IsRunning = false;
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
            if (!TextureManager.Instance.Load("Resources/Play.bmp", "playbutton", Game.Instance.GetRenderer))
            {
                return false;
            }
            if (!TextureManager.Instance.Load("Resources/Exit.bmp", "exitbutton", Game.Instance.GetRenderer))
            {
                return false;
            }
            GameObject button1 = new MenuButton(new LoaderParams(320, 100, 380, 203, "playbutton"), menuToPlay);
            GameObject button2 = new MenuButton(new LoaderParams(320, 400, 380, 203, "exitbutton"), exitFromMenu);
            gameObjects.Add(button1);
            gameObjects.Add(button2);

            Console.WriteLine("entering MenuState");

            return true;
        }

        public override bool OnExit()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Clean();
            }
            gameObjects.Clear();
            TextureManager.Instance.ClearFromTextureMap("playbutton");
            TextureManager.Instance.ClearFromTextureMap("exitbutton");

            Console.WriteLine("Exiting Menustate");
            return true;
        }

        public override string GetStateID()
        {
            return menuID;
        }
    }
}
