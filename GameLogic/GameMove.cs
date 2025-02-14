using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public class GameMove
    {
        private MoveLocations m_ChooseLocations;
        private MoveLocations m_CurrentLocations;
        private ComputerLocations m_ComputerLocations;

        public MoveLocations ChooseLocations
        {
            get
            {
                return m_ChooseLocations;
            }

            set
            {
                m_ChooseLocations = value;
            }
        }

        public MoveLocations CurrentLocations
        {
            get
            {
                return m_CurrentLocations;
            }

            set
            {
                m_CurrentLocations = value;
            }

        }

        public ComputerLocations ComputerLocations
        {
            get
            {
                return m_ComputerLocations;
            }

            set
            {
                m_ComputerLocations = value;
            }
        }

        private void AddValidMoves(MoveLocations i_CurrentLocation, int rowDirection, ref Board io_Board, ref List<MoveLocations> io_PossibleMoves)
        {
            int boardSize = (int)io_Board.GetBoardSize;
            char[,] boardMatrix = io_Board.GetBoardMatrix;
            int newRow = i_CurrentLocation.m_Row + rowDirection;

            if (newRow >= 0 && newRow < boardSize)
            {

                if (i_CurrentLocation.m_Col > 0 && boardMatrix[newRow, i_CurrentLocation.m_Col - 1] == ' ')

                {
                    io_PossibleMoves.Add(new MoveLocations(newRow, i_CurrentLocation.m_Col - 1));
                }

                if (i_CurrentLocation.m_Col < boardSize - 1 && boardMatrix[newRow, i_CurrentLocation.m_Col + 1] == ' ')
                {
                    io_PossibleMoves.Add(new MoveLocations(newRow, i_CurrentLocation.m_Col + 1));
                }
            }
        }

        public List<MoveLocations> ValidChoiceMoves(MoveLocations i_CurrentLocation, ref Board io_Board)
        {
            List<MoveLocations> io_PossibleMoves = new List<MoveLocations>();
            char playerMark = io_Board.GetCurrentLocationPlayerMark(i_CurrentLocation.m_Row, i_CurrentLocation.m_Col);

            if (playerMark == (char)ePlayerMark.Player1Mark)

            {
                AddValidMoves(i_CurrentLocation, 1, ref io_Board, ref io_PossibleMoves);
            }

            else if (playerMark == (char)ePlayerMark.Player2Mark)

            {
                AddValidMoves(i_CurrentLocation, -1, ref io_Board, ref io_PossibleMoves);
            }

            return io_PossibleMoves;
        }

        private bool IsValidMove(int i_Row, int i_Col, Board io_Board)
        {
            int boardSize = (int)io_Board.GetBoardSize;
            return i_Row >= 0 && i_Row < boardSize && i_Col >= 0 && i_Col < boardSize;
        }

        private void AddValidSkippingMoves(MoveLocations i_CurrentLocation, int i_RowMoveDirection, ePlayerMark i_OppositeMark, ePlayerMark i_OppositeKingMark, ref Board io_Board, ref List<MoveLocations> o_PossibleSkippingMoves)
        {
            int boardSize = (int)io_Board.GetBoardSize;
            char[,] boardMatrix = io_Board.GetBoardMatrix;
            int row = i_CurrentLocation.m_Row + (2 * i_RowMoveDirection);

            if (row < 0 || row >= boardSize)

            {
                return;
            }

            int[] colMoveDirections = { -1, 1 };

            foreach (int colMoveDirection in colMoveDirections)
            
            {
                int skippedRow = i_CurrentLocation.m_Row + i_RowMoveDirection;
                int skippedCol = i_CurrentLocation.m_Col + colMoveDirection;
                int col = i_CurrentLocation.m_Col + (2 * colMoveDirection);

                if (IsValidMove(row, col, io_Board) && (boardMatrix[skippedRow, skippedCol] == (char)i_OppositeMark || boardMatrix[skippedRow, skippedCol] == (char)i_OppositeKingMark) && boardMatrix[row, col] == ' ')
                
                {
                    o_PossibleSkippingMoves.Add(new MoveLocations(row, col));
                }
            }
        }

        public List<MoveLocations> ValidSkippingMoves(MoveLocations i_CurrentLocation, ref Board io_Board)
        {
            List<MoveLocations> o_PossibleSkippingMoves = new List<MoveLocations>();
            char playerMark = io_Board.GetCurrentLocationPlayerMark(i_CurrentLocation.m_Row, i_CurrentLocation.m_Col);
            int boardSize = (int)io_Board.GetBoardSize;


            if (playerMark == (char)ePlayerMark.Player1Mark && i_CurrentLocation.m_Row < boardSize - 2)

            {
                AddValidSkippingMoves(i_CurrentLocation, 1, ePlayerMark.Player2Mark, ePlayerMark.Player2KingMark, ref io_Board, ref o_PossibleSkippingMoves);
            }

            else if (playerMark == (char)ePlayerMark.Player2Mark && i_CurrentLocation.m_Row > 1)

            {
                AddValidSkippingMoves(i_CurrentLocation, -1, ePlayerMark.Player1Mark, ePlayerMark.Player1KingMark, ref io_Board, ref o_PossibleSkippingMoves);
            }

            return o_PossibleSkippingMoves;

        }

        public bool IsSkippingMove(ref Board io_Board, ePlayerNumber i_PlayerNumber)
        {
            int playerNumber = (int)i_PlayerNumber;
            bool o_BoolFlag = false;
            int boardSize = (int)io_Board.GetBoardSize;
            char[,] boardMatrix = io_Board.GetBoardMatrix;

            for (int i = 0; i < boardSize; i++)

            {
                for (int j = 0; j < boardSize; j++)

                {
                    char currentMark = boardMatrix[i, j];
                    MoveLocations currentLocation = new MoveLocations(i, j);

                    if ((playerNumber == 1 && currentMark == (char)ePlayerMark.Player1Mark) || (playerNumber == 2 && currentMark == (char)ePlayerMark.Player2Mark))

                    {
                        List<MoveLocations> possibleSkippingMoves = ValidSkippingMoves(currentLocation, ref io_Board);

                        if (possibleSkippingMoves.Count > 0)

                        {
                            o_BoolFlag = true;
                            break;
                        }
                    }

                    else if ((playerNumber == 1 && currentMark == (char)ePlayerMark.Player1KingMark) || (playerNumber == 2 && currentMark == (char)ePlayerMark.Player2KingMark))

                    {
                        List<MoveLocations> possibleKingSkippingMoves = ValidKingSkippingMoves(currentLocation, ref io_Board);

                        if (possibleKingSkippingMoves.Count > 0)

                        {
                            o_BoolFlag = true;
                            break;
                        }
                    }
                }

                if (o_BoolFlag)

                {
                    break;
                }
            }

            return o_BoolFlag;
        }

        public List<MoveLocations> ValidKingMoves(MoveLocations i_CurrentLocation, ref Board io_Board)
        {
            List<MoveLocations> o_PossibleKingMoves = new List<MoveLocations>();
            char[,] boardMatrix = io_Board.GetBoardMatrix;
            int boardSize = (int)io_Board.GetBoardSize;

            int[,] directions =
            {
                { -1, -1 }, { -1, 1 },
                { 1, -1 }, { 1, 1 }
            };

            int row = i_CurrentLocation.m_Row;
            int col = i_CurrentLocation.m_Col;

            for (int i = 0; i < directions.GetLength(0); i++)

            {
                int newRow = row + directions[i, 0];
                int newCol = col + directions[i, 1];

                if (newRow >= 0 && newRow < boardSize && newCol >= 0 && newCol < boardSize)

                {
                    if (boardMatrix[newRow, newCol] == ' ')

                    {
                        o_PossibleKingMoves.Add(new MoveLocations(newRow, newCol));
                    }
                }
            }

            return o_PossibleKingMoves;
        }

        public bool IsPossibleMoves(ref Board io_Board, ePlayerNumber i_PlayerNumber)
        {
            bool o_BoolFlag = false;
            char[,] boardMatrix = io_Board.GetBoardMatrix;
            int boardSize = (int)io_Board.GetBoardSize;
            MoveLocations currentIndex;

            for (int i = 0; i < boardSize; i++)

            {
                for (int j = 0; j < boardSize; j++)

                {
                    if ((int)i_PlayerNumber == 1)

                    {
                        if (boardMatrix[i, j] == (char)ePlayerMark.Player1Mark)

                        {
                            currentIndex.m_Row = i;
                            currentIndex.m_Col = j;
                            List<MoveLocations> PossibleMoves = ValidChoiceMoves(currentIndex, ref io_Board);

                            if (PossibleMoves.Count > 0)

                            {
                                o_BoolFlag = true;
                                break;
                            }

                        }

                        else if (boardMatrix[i, j] == (char)ePlayerMark.Player1KingMark)

                        {
                            currentIndex.m_Row = i;
                            currentIndex.m_Col = j;
                            List<MoveLocations> PossibleSkippingMoves = ValidKingMoves(currentIndex, ref io_Board);

                            if (PossibleSkippingMoves.Count > 0)
                            {
                                o_BoolFlag = true;
                                break;
                            }

                        }
                    }

                    else

                    {
                        if (boardMatrix[i, j] == (char)ePlayerMark.Player2Mark)

                        {
                            currentIndex.m_Row = i;
                            currentIndex.m_Col = j;
                            List<MoveLocations> PossibleSkippingMoves = ValidChoiceMoves(currentIndex, ref io_Board);

                            if (PossibleSkippingMoves.Count > 0)

                            {
                                o_BoolFlag = true;
                                break;
                            }
                        }

                        else if (boardMatrix[i, j] == (char)ePlayerMark.Player2KingMark)

                        {
                            currentIndex.m_Row = i;
                            currentIndex.m_Col = j;
                            List<MoveLocations> PossibleSkippingMoves = ValidKingMoves(currentIndex, ref io_Board);

                            if (PossibleSkippingMoves.Count > 0)
                            {
                                o_BoolFlag = true;
                                break;
                            }
                        }

                    }

                }

                if (o_BoolFlag)

                {
                    break;
                }

            }

            return o_BoolFlag;
        }

        public List<MoveLocations> ValidKingSkippingMoves(MoveLocations i_CurrentLocation, ref Board io_Board)
        {
            List<MoveLocations> o_PossibleKingMoves = new List<MoveLocations>();
            char[,] boardMatrix = io_Board.GetBoardMatrix;
            char playerMark = io_Board.GetCurrentLocationPlayerMark(i_CurrentLocation.m_Row, i_CurrentLocation.m_Col);

            int[,] moveDirections =
            {
                { 1, 1 }, { 1, -1 },
                { -1, 1 }, { -1, -1 }
            };

            ePlayerMark oppositePlayerMark;
            ePlayerMark oppositePlayerKingMark;

            if (playerMark == (char)ePlayerMark.Player1KingMark)

            {
                oppositePlayerMark = ePlayerMark.Player2Mark;
                oppositePlayerKingMark = ePlayerMark.Player2KingMark;
            }

            else if (playerMark == (char)ePlayerMark.Player2KingMark)

            {
                oppositePlayerMark = ePlayerMark.Player1Mark;
                oppositePlayerKingMark = ePlayerMark.Player1KingMark;
            }

            else

            {
                return o_PossibleKingMoves;
            }

            for (int i = 0; i < moveDirections.GetLength(0); i++)

            {
                int skippedRow = i_CurrentLocation.m_Row + moveDirections[i, 0];
                int skippedCol = i_CurrentLocation.m_Col + moveDirections[i, 1];
                int row = i_CurrentLocation.m_Row + (2 * moveDirections[i, 0]);
                int col = i_CurrentLocation.m_Col + (2 * moveDirections[i, 1]);

                if (IsValidMove(row, col, io_Board) && (boardMatrix[skippedRow, skippedCol] == (char)oppositePlayerMark || boardMatrix[skippedRow, skippedCol] == (char)oppositePlayerKingMark) && boardMatrix[row, col] == ' ')
                {
                    o_PossibleKingMoves.Add(new MoveLocations(row, col));
                }
            }

            return o_PossibleKingMoves;
        }

        public bool IsPlayerSkipMoveValid(ref Board io_Board, MoveLocations i_CurrentLocation, MoveLocations i_ChosenLocation)
        {
            bool o_BoolFlag = false;
            List<MoveLocations> possibleMoves = new List<MoveLocations>();
            char[,] boardMatrix = io_Board.GetBoardMatrix;

            if ((boardMatrix[i_CurrentLocation.m_Row, i_CurrentLocation.m_Col] == (char)ePlayerMark.Player1KingMark) || boardMatrix[i_CurrentLocation.m_Row, i_CurrentLocation.m_Col] == (char)ePlayerMark.Player2KingMark)

            {
                possibleMoves = ValidKingSkippingMoves(i_CurrentLocation, ref io_Board);
            }

            else

            {
                possibleMoves = ValidSkippingMoves(i_CurrentLocation, ref io_Board);
            }

            if (possibleMoves.Count > 0)

            {
                foreach (MoveLocations MoveLocation in possibleMoves)

                {
                    if (MoveLocation.m_Row == i_ChosenLocation.m_Row && MoveLocation.m_Col == i_ChosenLocation.m_Col)

                    {
                        o_BoolFlag = true;
                        break;
                    }

                }

            }

            return o_BoolFlag;
        }

        public bool IsPlayerMoveValid(ref Board io_Board, MoveLocations i_CurrentLocation, MoveLocations i_ChosenLocation)
        {
            bool o_BoolFlag = false;
            List<MoveLocations> possiblePlayerMoves = new List<MoveLocations>();
            char[,] boardMatrix = io_Board.GetBoardMatrix;

            if ((boardMatrix[i_CurrentLocation.m_Row, i_CurrentLocation.m_Col] == (char)ePlayerMark.Player1KingMark) || boardMatrix[i_CurrentLocation.m_Row, i_CurrentLocation.m_Col] == (char)ePlayerMark.Player2KingMark)

            {
                possiblePlayerMoves = ValidKingMoves(i_CurrentLocation, ref io_Board);
            }

            else

            {
                possiblePlayerMoves = ValidChoiceMoves(i_CurrentLocation, ref io_Board);
            }

            if (possiblePlayerMoves.Count > 0)

            {
                foreach (MoveLocations moveLocation in possiblePlayerMoves)

                {
                    if (moveLocation.m_Row == i_ChosenLocation.m_Row && moveLocation.m_Col == i_ChosenLocation.m_Col)

                    {
                        o_BoolFlag = true;
                        break;
                    }

                }

            }

            return o_BoolFlag;
        }

        public List<ComputerLocations> ValidComputerMoves(ref Board io_Board)
        {
            List<ComputerLocations> o_PossibleMoves = new List<ComputerLocations>();
            List<MoveLocations> validMoves = new List<MoveLocations>();
            char[,] boardMatrix = io_Board.GetBoardMatrix;
            int boardSize = (int)io_Board.GetBoardSize;
            MoveLocations currentLocation;

            for (int i = 0; i < boardSize; i++)

            {
                for (int j = 0; j < boardSize; j++)

                {
                    if (boardMatrix[i, j] == (char)ePlayerMark.Player2Mark)

                    {
                        currentLocation.m_Row = i;
                        currentLocation.m_Col = j;
                        validMoves = ValidChoiceMoves(currentLocation, ref io_Board);

                        foreach (MoveLocations moveLocation in validMoves)

                        {
                            o_PossibleMoves.Add(new ComputerLocations(i, j, moveLocation.m_Row, moveLocation.m_Col));
                        }

                    }

                    else if (boardMatrix[i, j] == (char)ePlayerMark.Player2KingMark)

                    {
                        currentLocation.m_Row = i;
                        currentLocation.m_Col = j;
                        validMoves = ValidKingMoves(currentLocation, ref io_Board);

                        foreach (MoveLocations moveLocation in validMoves)

                        {
                            o_PossibleMoves.Add(new ComputerLocations(i, j, moveLocation.m_Row, moveLocation.m_Col));
                        }

                    }

                }

            }

            return o_PossibleMoves;
        }

        public List<ComputerLocations> ValidComputerSkippingMoves(ref Board io_Board)
        {
            List<ComputerLocations> o_PossibleSkippingMoves = new List<ComputerLocations>();
            List<MoveLocations> validMoves = new List<MoveLocations>();
            char[,] boardMatrix = io_Board.GetBoardMatrix;
            int boardSize = (int)io_Board.GetBoardSize;
            MoveLocations currentLocation;

            for (int i = 0; i < boardSize; i++)

            {
                for (int j = 0; j < boardSize; j++)

                {
                    if (boardMatrix[i, j] == (char)ePlayerMark.Player2Mark)

                    {
                        currentLocation.m_Row = i;
                        currentLocation.m_Col = j;

                        validMoves = ValidSkippingMoves(currentLocation, ref io_Board);

                        foreach (MoveLocations moveLocation in validMoves)

                        {
                            o_PossibleSkippingMoves.Add(new ComputerLocations(i, j, moveLocation.m_Row, moveLocation.m_Col));
                        }

                    }

                    else if (boardMatrix[i, j] == (char)ePlayerMark.Player2KingMark)

                    {
                        currentLocation.m_Row = i;
                        currentLocation.m_Col = j;

                        validMoves = ValidKingSkippingMoves(currentLocation, ref io_Board);

                        foreach (MoveLocations moveLocation in validMoves)

                        {
                            o_PossibleSkippingMoves.Add(new ComputerLocations(i, j, moveLocation.m_Row, moveLocation.m_Col));
                        }

                    }

                }

            }

            return o_PossibleSkippingMoves;
        }

        public GameMove RandomSkippingComputerMove(List<ComputerLocations> i_ComputerSkippingMoves)
        {
            int randomLocation = 0;
            GameMove o_GameRandomMove = new GameMove();
            MoveLocations startRandomLocationMove = new MoveLocations();
            MoveLocations endRandomLocationMove = new MoveLocations();

            Random random = new Random();

            if (i_ComputerSkippingMoves.Count > 0)

            {
                randomLocation = random.Next(i_ComputerSkippingMoves.Count);
                startRandomLocationMove.m_Row = i_ComputerSkippingMoves[randomLocation].m_StartRow;
                startRandomLocationMove.m_Col = i_ComputerSkippingMoves[randomLocation].m_StartCol;
                endRandomLocationMove.m_Row = i_ComputerSkippingMoves[randomLocation].m_FinishRow;
                endRandomLocationMove.m_Col = i_ComputerSkippingMoves[randomLocation].m_FinishCol;

            }

            o_GameRandomMove.CurrentLocations = startRandomLocationMove;
            o_GameRandomMove.ChooseLocations = endRandomLocationMove;

            return o_GameRandomMove;
        }

        public GameMove RandomComputerMove(ref Board io_Board)
        {
            int randomLocation = 0;
            GameMove o_GameRandomMove = new GameMove();
            MoveLocations startRandomLocationMove = new MoveLocations();
            MoveLocations endRandomLocationMove = new MoveLocations();
            List<ComputerLocations> computerMoves = ValidComputerMoves(ref io_Board);
            Random random = new Random();

            if (computerMoves.Count > 0)

            {
                randomLocation = random.Next(computerMoves.Count);
                startRandomLocationMove.m_Row = computerMoves[randomLocation].m_StartRow;
                startRandomLocationMove.m_Col = computerMoves[randomLocation].m_StartCol;
                endRandomLocationMove.m_Row = computerMoves[randomLocation].m_FinishRow;
                endRandomLocationMove.m_Col = computerMoves[randomLocation].m_FinishCol;
            }

            o_GameRandomMove.CurrentLocations = startRandomLocationMove;
            o_GameRandomMove.ChooseLocations = endRandomLocationMove;

            return o_GameRandomMove;
        }

        public bool IsPlayerStuck(ref Board io_Board, ePlayerNumber i_PlayerNumber)
        {
            return ((!IsPossibleMoves(ref io_Board, i_PlayerNumber) && !IsSkippingMove(ref io_Board, i_PlayerNumber)));
        }

        public GameMove InputToPlayerMove(string i_PlayerInput)
        {
            GameMove o_PlayerMove = new GameMove();

            MoveLocations currentLocations = new MoveLocations();
            MoveLocations chosenLocations = new MoveLocations();

            currentLocations.m_Row = i_PlayerInput[0] - 'A';
            currentLocations.m_Col = i_PlayerInput[1] - 'a';
            chosenLocations.m_Row = i_PlayerInput[3] - 'A';
            chosenLocations.m_Col = i_PlayerInput[4] - 'a';

            o_PlayerMove.CurrentLocations = currentLocations;
            o_PlayerMove.ChooseLocations = chosenLocations;

            return o_PlayerMove;
        }

        public StringBuilder ComputerMoveToString(ref GameMove i_GameMoves)

        {
            StringBuilder o_MoveString = new StringBuilder();

            o_MoveString.Append((char)(i_GameMoves.CurrentLocations.m_Row + 'A'));
            o_MoveString.Append((char)(i_GameMoves.CurrentLocations.m_Col + 'a'));
            o_MoveString.Append('>');
            o_MoveString.Append((char)(i_GameMoves.ChooseLocations.m_Row + 'A'));
            o_MoveString.Append((char)(i_GameMoves.ChooseLocations.m_Col + 'a'));

            return o_MoveString;
        }

    }
}
