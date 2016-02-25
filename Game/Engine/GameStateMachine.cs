// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using System.Linq;

namespace src
{
    public class GameStateMachine
    {
        private List<GameState> gameStates;

        public GameStateMachine()
        {
            gameStates = new List<GameState>();
        }

        public void update()
        {
            if (gameStates.Count != 0)
            {
                gameStates.Last().update();
            }
        }

        public void render()
        {
            if (gameStates.Count != 0)
            {
                gameStates.Last().render();
            }
        }

        public void pushState(GameState state)
        {
            gameStates.Add(state);
            gameStates.Last().onEnter();
        }

        public void changeState(GameState state)
        {
            if (gameStates.Count != 0)
            {
                if (gameStates.Last().getStateID() == state.getStateID())
                {
                    return;
                }

                if (gameStates.Last().onExit())
                {
                    gameStates.Remove(gameStates.Last());
                }
            }
            gameStates.Add(state);
            gameStates.Last().onEnter();
        }

        public void popState()
        {
            if (gameStates.Count != 0)
            {
                if (gameStates.Last().onExit())
                {
                    gameStates.Remove(gameStates.Last());
                }
            }
        }

    }
}

