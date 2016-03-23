/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
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
        /// Initializes a new instance of the <see cref="src.GameStateMachine"/> class.
        /// </summary>
        public GameStateMachine()
        {
        }

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
        public void PushState(GameState state)
        {
            gameStates.Add(state);
            gameStates.Last().OnEnter();
        }

        /// <summary>
        /// Changes the state.
        /// </summary>
        /// <param name="state">State.</param>
        public void ChangeState(GameState state)
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
        public void PopState()
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

