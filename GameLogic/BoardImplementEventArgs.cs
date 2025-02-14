using System;

namespace GameLogic
{
    public class BoardImplementEventArgs : EventArgs
    {
        private GameMove m_GameMove;

        public BoardImplementEventArgs(GameMove i_GameMove)
        {
            m_GameMove = i_GameMove;
        }

        public GameMove GetGameMove
        {
            get 
            { 
                return m_GameMove; 
            }
        }

    }
}
