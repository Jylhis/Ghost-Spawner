﻿/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen & Veeti Karttunen
 *
 * This is part of Object-Oriented and GUI Programming course assignment
 * and is licensed under MIT license, see LICENSE file.
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
#if DEBUG
            Console.WriteLine("Pressed Play");
#endif
            Game.Instance.GetStateMachine.Change(new PlayState());
        }

        private static void exitFromMenu()
        {
#if DEBUG
            Console.WriteLine("Pressed Exit");
#endif
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
#if DEBUG
            Console.WriteLine("entering MenuState");
#endif
            return true;
        }

        public override bool OnExit()
        {
            gameObjects.Clear();
            TextureManager.Instance.ClearFromTextureMap("playbutton");
            TextureManager.Instance.ClearFromTextureMap("exitbutton");
#if DEBUG
            Console.WriteLine("Exiting Menustate");
#endif
            return true;
        }

        public override string GetStateID()
        {
            return menuID;
        }
    }
}
