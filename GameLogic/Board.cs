using System;

namespace GameLogic
{
    public class Board
    {
        private readonly eBoardSize r_BoardSize;
        private char[,] m_BoardMatrix;

        public Board(eBoardSize i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            m_BoardMatrix = new char[(int)r_BoardSize, (int)r_BoardSize];
        }

        public eBoardSize GetBoardSize
        {
            get 
            { 
                return r_BoardSize; 
            }
        }

        public char[,] GetBoardMatrix
        {
            get 
            { 
                return m_BoardMatrix; 
            }
        }

        public void InitializeBoard(eBoardSize i_BoardSize)
        {
            int boardSize = (int)i_BoardSize;

            for (int i = 0; i < boardSize; i++)

            {
                for (int j = 0; j < boardSize; j++)

                {
                    if (i < (boardSize - 2) / 2)

                    {
                        if ((i % 2 != 0 && j % 2 == 0) || (i % 2 == 0 && j % 2 != 0))

                        {
                            m_BoardMatrix[i, j] = 'O'; 
                        }

                        else

                        {
                            m_BoardMatrix[i, j] = ' ';
                        }

                    }

                    else if (i >= ((boardSize - 2) / 2) + 2)

                    {
                        if ((i % 2 != 0 && j % 2 == 0) || (i % 2 == 0 && j % 2 != 0))

                        {
                            m_BoardMatrix[i, j] = 'X';
                        }

                        else

                        {
                            m_BoardMatrix[i, j] = ' ';
                        }

                    }

                    else

                    {
                        m_BoardMatrix[i, j] = ' ';
                    }

                }


            }
        }

        public char GetCurrentLocationPlayerMark(int i_PlayerRow, int i_PlayerCol)
        {
            return m_BoardMatrix[i_PlayerRow, i_PlayerCol];
        }

        public void UpdateBoard(MoveLocations i_CurrentLocation, MoveLocations i_ChosenLocation)
        {
            m_BoardMatrix[i_ChosenLocation.m_Row, i_ChosenLocation.m_Col] = m_BoardMatrix[i_CurrentLocation.m_Row, i_CurrentLocation.m_Col];
            m_BoardMatrix[i_CurrentLocation.m_Row, i_CurrentLocation.m_Col] = ' ';
        }

        public void RemoveMark(MoveLocations i_SkipLocation, MoveLocations i_CurrentLocation)
        {
            int boardSize = (int)this.GetBoardSize;

            int rowDiff = i_SkipLocation.m_Row - i_CurrentLocation.m_Row;
            int colDiff = i_SkipLocation.m_Col - i_CurrentLocation.m_Col;

            if (Math.Abs(rowDiff) == 2 && Math.Abs(colDiff) == 2)

            {
                int removedRow = (i_SkipLocation.m_Row + i_CurrentLocation.m_Row) / 2;
                int removedCol = (i_SkipLocation.m_Col + i_CurrentLocation.m_Col) / 2;

                if (removedRow >= 0 && removedRow < boardSize && removedCol >= 0 && removedCol < boardSize)

                {
                    m_BoardMatrix[removedRow, removedCol] = ' ';
                }
            }
        }

        public void UpdateKing(MoveLocations i_CurrentLocation, char i_Mark)
        {
            if (i_Mark == (char)ePlayerMark.Player1Mark && i_CurrentLocation.m_Row == (int)r_BoardSize - 1)

            {
                m_BoardMatrix[i_CurrentLocation.m_Row, i_CurrentLocation.m_Col] = (char)ePlayerMark.Player1KingMark;
            }

            else if (i_Mark == (char)ePlayerMark.Player2Mark && i_CurrentLocation.m_Row == 0)

            {
                m_BoardMatrix[i_CurrentLocation.m_Row, i_CurrentLocation.m_Col] = (char)ePlayerMark.Player2KingMark;
            }

        }

        public bool IsKing(MoveLocations i_CurrentLocations)
        {
            return (m_BoardMatrix[i_CurrentLocations.m_Row, i_CurrentLocations.m_Col] == (char)ePlayerMark.Player1KingMark || m_BoardMatrix[i_CurrentLocations.m_Row, i_CurrentLocations.m_Col] == (char)ePlayerMark.Player2KingMark);
        }

        
    }
}
