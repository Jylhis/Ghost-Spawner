// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;

namespace src
{
    public class MenuState : GameState
    {
        private const string menuID = "MENU";
        private List<GameObject> gameobjects;

        private static void menuToPlay()
        {
            Console.WriteLine("PLAY!");
            Game.Instance.getStateMachine.changeState(new PlayState());
        }

        private static void exitFromMenu()
        {
            Console.WriteLine("Exit!");
            Game.Instance.IsRunning = false;
        }

        public override void update()
        {
            for (int i = 0; i < gameobjects.Count; i++)
            {
                gameobjects[i].Update();
            }
        }

        public override void render()
        {
            for (int i = 0; i < gameobjects.Count; i++)
            {
                gameobjects[i].Draw();
            } 
        }

        public override bool onEnter()
        {
            gameobjects = new List<GameObject>();
            if (!TextureManager.Instance.Load("Resources/Play.bmp",
                   "playbutton", Game.Instance.getRenderer))
            {
                return false;
            }
            if (!TextureManager.Instance.Load("Resources/Exit.bmp",
                   "exitbutton", Game.Instance.getRenderer))
            {
                return false;
            }
            GameObject button1 = new MenuButton(new LoaderParams(100, 100, 400, 100, "playbutton"), menuToPlay);
            GameObject button2 = new MenuButton(new LoaderParams(100, 300, 400, 100, "exitbutton"), exitFromMenu);
            gameobjects.Add(button1);
            gameobjects.Add(button2);
            Console.WriteLine("entering MenuState");
            return true;
        }

        public override bool onExit()
        {
            for (int i = 0; i < gameobjects.Count; i++)
            {
                gameobjects[i].Clean();
            }
            gameobjects.Clear();
            TextureManager.Instance.clearFromTextureMap("playbutton");
            TextureManager.Instance.clearFromTextureMap("exitbutton");

            Console.WriteLine("Exiting Menustate");
            return true;
        }

        public override string getStateID()
        {
            return menuID;
        }
    }
}
