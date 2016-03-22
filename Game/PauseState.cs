// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using SDL2;

namespace src
{
    public class PauseState : GameState
    {
        private const string menuID = "PAUSE";
        private List<GameObject> gameobjects = new List<GameObject>();

        private static void pauseToMain()
        {
            Console.WriteLine("PLAY!");
            Game.Instance.getStateMachine.changeState(new MenuState());
        }

        private static void resumePlay()
        {
            Console.WriteLine("Exit!");
            Game.Instance.getStateMachine.popState();
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
            if (!TextureManager.Instance.Load("Resources/Resume.bmp",
                    "resumebutton", Game.Instance.getRenderer))
            {
                return false;
            }
            if (!TextureManager.Instance.Load("Resources/Main.bmp",
                    "mainmenubutton", Game.Instance.getRenderer))
            {
                return false;
            }
            GameObject button1 = new MenuButton(new LoaderParams(320, 100, 380, 203, "resumebutton"), resumePlay);
            GameObject button2 = new MenuButton(new LoaderParams(320, 400, 380, 203, "mainmenubutton"), pauseToMain);
            gameobjects.Add(button1);
            gameobjects.Add(button2);
            Console.WriteLine("entering PauseState");
            return true;
        }

        public override bool onExit()
        {
            for (int i = 0; i < gameobjects.Count; i++)
            {
                gameobjects[i].Clean();
            }
            gameobjects.Clear();
            TextureManager.Instance.clearFromTextureMap("resumebutton");
            TextureManager.Instance.clearFromTextureMap("mainmenubutton");

            InputHandler.Instance.reset();

            Console.WriteLine("Exiting Menustate");
            return true;
        }

        public override string getStateID()
        {
            return menuID;
        }
    }
}
