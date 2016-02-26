// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;

namespace src
{
    public class GameOverState : GameState
    {
        private List<GameObject> gameObjects;
        private const string menuID = "GAMEOVER";

        private static void gameOverToMain()
        {
            Game.Instance.getStateMachine.changeState(new MenuState());
        }

        private static void restartPlay()
        {
            Game.Instance.getStateMachine.changeState(new PlayState());
        }

        public GameOverState()
        {
        }

        public override void update()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }
        }

        public override void render()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw();
            }
            ;
        }

        public override bool onEnter()
        {
            if (!TextureManager.Instance.Load("assets/gameover.png",
                    "gameovertext", Game.Instance.getRenderer))
            {
                return false;
            }
            if (!TextureManager.Instance.Load("assets/main.png",
                    "mainbutton", Game.Instance.getRenderer))
            {
                return false;
            }
            if (!TextureManager.Instance.Load("assets/restart.png",
                    "restartbutton", Game.Instance.getRenderer))
            {
                return false;
            }
            GameObject gameOverText = new AnimatedGraphics(new LoaderParams(200, 100, 190, 30, "gameovertext"), 2);

            GameObject button1 = new MenuButton(new LoaderParams(200, 200,
                                         200, 80, "mainbutton"), gameOverToMain);
            GameObject button2 = new MenuButton(new LoaderParams(200, 300,
                                         200, 80, "restartbutton"), restartPlay);

            gameObjects.Add(gameOverText);

            gameObjects.Add(button1);
            gameObjects.Add(button2);
            Console.WriteLine("Entering GameOverState");
            return true;
        }

        public override bool onExit()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Clean();
            }
            gameObjects.Clear();
            TextureManager.Instance.clearFromTextureMap("mainbutton");
            TextureManager.Instance.clearFromTextureMap("restartbutton");

            InputHandler.Instance.reset();

            Console.WriteLine("Exiting GameOverState");
            return true;
        }

        public override string getStateID()
        {
            return menuID;
        }
    }
}

