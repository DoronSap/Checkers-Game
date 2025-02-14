using System;

namespace GameLogic
{
    public class GameOverEventArgs : EventArgs
    {
        private string m_Message;
        private eGameState m_GameState; 

        public eGameState GameState
        {
            get
            {
                return m_GameState;
            }

            set
            {
                m_GameState = value;
            }
        }
        public string Message
        {
            get
            {
                return m_Message;
            }
        }

        public GameOverEventArgs(eGameState gameState)
        {
            GameState = gameState;

            if (gameState == eGameState.Tie)

            {
                m_Message = "It's a tie!";
            }

            else if (gameState == eGameState.Player1Won)

            {
                m_Message = "Player 1 won!";
            }

            else if (gameState == eGameState.Player2Won)

            {
                m_Message = "Player 2 won!";
            }
        }

    }
}
