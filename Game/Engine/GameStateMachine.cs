// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using System.Linq;

namespace src
{
    public class GameStateMachine
    {
        private List<GameState> gameStates;

        /// <summary>
        /// Initializes a new instance of the <see cref="src.GameStateMachine"/> class.
        /// </summary>
        public GameStateMachine()
        {
            gameStates = new List<GameState>();
        }

        /// <summary>
        /// Update this instance.
        /// </summary>
        public void update()
        {
            if (gameStates.Count != 0)
            {
                gameStates.Last().update();
            }
        }

        /// <summary>
        /// Render this instance.
        /// </summary>
        public void render()
        {
            if (gameStates.Count != 0)
            {
                gameStates.Last().render();
            }
        }

        /// <summary>
        /// Pushs the state.
        /// </summary>
        /// <param name="state">State.</param>
        public void pushState(GameState state)
        {
            gameStates.Add(state);
            gameStates.Last().onEnter();
        }

        /// <summary>
        /// Changes the state.
        /// </summary>
        /// <param name="state">State.</param>
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

        /// <summary>
        /// Pops the state.
        /// </summary>
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

