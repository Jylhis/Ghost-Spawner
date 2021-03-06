﻿/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen & Veeti Karttunen
 *
 * This is part of Object-Oriented and GUI Programming course assignment
 * and is licensed under MIT license, see LICENSE file.
 *
 * Created: 26.02.2016
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace src
{
    public class GameOverState : GameState
    {
        private List<GameObject> gameObjects = new List<GameObject>();
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
            /* Stream scoreFile = new FileStream("scores.db", FileMode.Open, FileAccess.Read, FileShare.None);
             int lastHighScore = scoreFile.ReadByte();
             scoreFile.Close();

             Console.WriteLine(lastHighScore);*/

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

            GameObject button1 = new MenuButton(new LoaderParams(320, 400, 380, 203, "mainbutton"), gameOverToMain);
            GameObject button2 = new MenuButton(new LoaderParams(320, 100, 380, 203, "restartbutton"), restartPlay);

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

