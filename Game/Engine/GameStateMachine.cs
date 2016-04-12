/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen & Veeti Karttunen
 *
 * This is part of Object-Oriented and GUI Programming course assignment
 * and is licensed under MIT license, see LICENSE file.
 *
 * Created: 25.02.2016
 */
using System.Collections.Generic;
using System.Linq;

namespace src
{
    public class GameStateMachine
    {
        private List<GameState> gameStates = new List<GameState>();

        /// <summary>
        /// Update this instance.
        /// </summary>
        public void Update()
        {
            if (gameStates.Count != 0)
            {
                gameStates.Last().Update();
            }
        }

        /// <summary>
        /// Render this instance.
        /// </summary>
        public void Render()
        {
            if (gameStates.Count != 0)
            {
                gameStates.Last().Render();
            }
        }

        /// <summary>
        /// Push the state.
        /// </summary>
        /// <param name="state">State.</param>
        public void Push(GameState state)
        {
            gameStates.Add(state);
            gameStates.Last().OnEnter();
        }

        /// <summary>
        /// Changes the state.
        /// </summary>
        /// <param name="state">State.</param>
        public void Change(GameState state)
        {
            if (gameStates.Count != 0)
            {
                if (gameStates.Last().GetStateID() == state.GetStateID())
                {
                    return;
                }

                if (gameStates.Last().OnExit())
                {
                    gameStates.Remove(gameStates.Last());
                }
            }
            gameStates.Add(state);
            gameStates.Last().OnEnter();
        }

        /// <summary>
        /// Pops the state.
        /// </summary>
        public void Pop()
        {
            if (gameStates.Count != 0)
            {
                if (gameStates.Last().OnExit())
                {
                    gameStates.Remove(gameStates.Last());
                }
            }
        }
    }
}

