namespace GameLogic
{
    public class Player
    {
        private string m_PlayerName;
        private int m_PlayerScore;
        private ePlayerNumber m_PlayerNumber;
        private ePlayerType m_PlayerType;
        private ePlayerMark m_PlayerMark;
        private ePlayerMark m_PlayerKingMark;

        public Player()
        {
            m_PlayerName = "Player";
            m_PlayerScore = 0;
            m_PlayerNumber = ePlayerNumber.Player1;
        }
        public Player(string i_PlayerName)
        {
            m_PlayerName = i_PlayerName;
            m_PlayerScore = 0;
            m_PlayerNumber = ePlayerNumber.Player1;
        }

        public ePlayerNumber PlayerNumber
        {
            get 
            {
                return m_PlayerNumber; 
            }

            set 
            { 
                m_PlayerNumber = value; 
            }
        }

        public ePlayerType PlayerType
        {
            get 
            {
                return m_PlayerType; 
            }

            set 
            { 
                m_PlayerType = value; 
            }
        }

        public ePlayerMark PlayerMark
        {
            get 
            { 
                return m_PlayerMark; 
            }

            set 
            { 
                m_PlayerMark = value; 
            }
        }

        public ePlayerMark PlayerKingMark
        {
            get 
            {
                return m_PlayerKingMark; 
            }

            set 
            {
                m_PlayerKingMark = value; 
            }
        }

        public int PlayerScore
        {
            get 
            { 
                return m_PlayerScore; 
            }

            set 
            {
                m_PlayerScore = value; 
            }
        }

        public string PlayerName
        {
            get 
            { 
                return m_PlayerName; 
            }

            set 
            { 
                m_PlayerName = value; 
            }
        }

        public static bool IsPlayerNameValid(string i_PlayerName)
        {
            if (i_PlayerName.Length > 20 || i_PlayerName.Contains(" "))

            {
                return false;
            }

            return true;
        }

    }
}
