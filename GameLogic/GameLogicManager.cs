using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class GameLogicManager
    {
        private Board m_Board;
        private Player m_CurrentPlayer = new Player();
        private Player m_OppositePlayer = new Player();
        private GameMove m_GameMove = new GameMove();
        private List<ComputerLocations> m_ComputerMovesList;
        public EventHandler<BoardImplementEventArgs> BoardImplement;
        public EventHandler NewGame;
        public EventHandler<GameOverEventArgs> GameOver;

        public GameMove GetGameMove
        {
            get 
            { 
                return m_GameMove; 
            }
        }

        public Board GetBoard
        {
            get
            {
                return m_Board;
            }
        }

        public Player CurrentPlayer
        {
            get 
            { 
                return m_CurrentPlayer;
            }
        }

        public Player OppositePlayer
        {
            get
            {
                return m_OppositePlayer;
            }
        }

        public List<ComputerLocations> ComputerMovesList
        {
            get 
            { 
                return m_ComputerMovesList; 
            }
        }

        public void SetPlayers(string i_Player1Name, string i_Player2Name, bool i_SecondPlayerIsComputer)
        {
            if (m_CurrentPlayer.PlayerNumber != ePlayerNumber.Player1)

            {
                SwitchPlayer(ref m_CurrentPlayer, ref m_OppositePlayer);
            }

            m_CurrentPlayer.PlayerName = i_Player1Name;
            m_CurrentPlayer.PlayerMark = ePlayerMark.Player1Mark;
            m_CurrentPlayer.PlayerKingMark = ePlayerMark.Player1KingMark;
            m_CurrentPlayer.PlayerType = ePlayerType.Human;

            m_OppositePlayer.PlayerMark = ePlayerMark.Player2Mark;
            m_OppositePlayer.PlayerKingMark = ePlayerMark.Player2KingMark;
            m_OppositePlayer.PlayerNumber = ePlayerNumber.Player2;

            if (i_SecondPlayerIsComputer)

            {
                m_OppositePlayer.PlayerType = ePlayerType.Computer;
                m_OppositePlayer.PlayerName = "Computer";
            }

            else

            {
                m_OppositePlayer.PlayerType = ePlayerType.Human;
                m_OppositePlayer.PlayerName = i_Player2Name;
            }
        }

        public bool IsGameOver(ref Board io_Board, ePlayerNumber i_PlayerNumber)
        {
            return m_GameMove.IsPlayerStuck(ref io_Board, i_PlayerNumber) ? true : false;
        }

        public eGameState DetermineGameOverState(ref Board io_Board, ref Player io_OppositePlayer)
        {
            eGameState o_GameState;

            if (m_GameMove.IsPlayerStuck(ref io_Board, io_OppositePlayer.PlayerNumber))

            {
                o_GameState = eGameState.Tie;
            }

            else

            {
                if (io_OppositePlayer.PlayerNumber == ePlayerNumber.Player1)

                {
                    o_GameState = eGameState.Player1Won;
                }

                else

                {
                    o_GameState = eGameState.Player2Won;
                }

                this.CalculateScore(ref io_OppositePlayer);
            }

            return o_GameState;
        }

        public void SwitchPlayer(ref Player io_CurrentPlayer, ref Player io_OppositePlayer)
        {
            Player tempPlayer = null;
            tempPlayer = io_CurrentPlayer;
            io_CurrentPlayer = io_OppositePlayer;
            io_OppositePlayer = tempPlayer;
        }

        public void OnBoardImplement(BoardImplementEventArgs i_BoardImplementEventArgs)
        {
            if (BoardImplement != null)
            {
                BoardImplement(this, i_BoardImplementEventArgs);
            }
        }

        public bool IsCurrentPlayerPiece(int i_Row, int i_Col)
        {
            char piece = GetBoard.GetCurrentLocationPlayerMark(i_Row, i_Col);

            return (piece == (char)m_CurrentPlayer.PlayerMark || piece == (char)m_CurrentPlayer.PlayerKingMark);
        }

        public void CalculateScore(ref Player io_WinnerPlayer)

        {
            char[,] boardMatrix = m_Board.GetBoardMatrix;

            int boardSize = (int)m_Board.GetBoardSize;
            int winnerPlayerScore = 0;
            int oppositePlayerScore = 0;

            ePlayerMark oppositePlayerMark = io_WinnerPlayer.PlayerMark == ePlayerMark.Player1Mark ? ePlayerMark.Player2Mark : ePlayerMark.Player1Mark;
            ePlayerMark oppositePlayerKingMark = io_WinnerPlayer.PlayerKingMark == ePlayerMark.Player1KingMark ? ePlayerMark.Player2KingMark : ePlayerMark.Player1KingMark;

            for (int i = 0; i < boardSize; i++)

            {
                for (int j = 0; j < boardSize; j++)

                {
                    if (boardMatrix[i, j] == (char)io_WinnerPlayer.PlayerMark)

                    {
                        winnerPlayerScore++;
                    }

                    else if (boardMatrix[i, j] == (char)io_WinnerPlayer.PlayerKingMark)

                    {
                        winnerPlayerScore += 4;
                    }

                    else if ((boardMatrix[i, j] == (char)oppositePlayerMark))

                    {
                        oppositePlayerScore++;
                    }

                    else if (boardMatrix[i, j] == (char)oppositePlayerKingMark)

                    {
                        oppositePlayerScore += 4;
                    }

                }

            }

            winnerPlayerScore = Math.Abs(winnerPlayerScore - oppositePlayerScore);
            io_WinnerPlayer.PlayerScore += winnerPlayerScore;
        }

        public bool ApplyMove(GameMove i_GameMove)
        {
            bool o_IsMoveApplied = false;

            BoardImplementEventArgs boardImplementsEventArgs;
            GameOverEventArgs gameOverEventArgs;
            GameMove computerMove = i_GameMove;

                if (i_GameMove.IsSkippingMove(ref m_Board, m_CurrentPlayer.PlayerNumber))

                {
                    if (i_GameMove.IsPlayerSkipMoveValid(ref m_Board, i_GameMove.CurrentLocations, i_GameMove.ChooseLocations))

                    {
                        m_Board.RemoveMark(i_GameMove.ChooseLocations, i_GameMove.CurrentLocations);
                        m_Board.UpdateBoard(i_GameMove.CurrentLocations, i_GameMove.ChooseLocations);
                        char currentPlayerMark = m_Board.GetCurrentLocationPlayerMark(i_GameMove.ChooseLocations.m_Row, i_GameMove.ChooseLocations.m_Col);
                        m_Board.UpdateKing(i_GameMove.ChooseLocations, currentPlayerMark);

                        if (!i_GameMove.IsSkippingMove(ref m_Board, m_CurrentPlayer.PlayerNumber))

                        {
                            SwitchPlayer(ref m_CurrentPlayer, ref m_OppositePlayer);
                        }

                        boardImplementsEventArgs = new BoardImplementEventArgs(i_GameMove);
                        OnBoardImplement(boardImplementsEventArgs);
                        o_IsMoveApplied = true;
                    }

                    else

                    {
                        o_IsMoveApplied = false;
                    }
                }

                else

                {
                    if (i_GameMove.IsPlayerMoveValid(ref m_Board, i_GameMove.CurrentLocations, i_GameMove.ChooseLocations))

                    {
                        m_Board.UpdateBoard(i_GameMove.CurrentLocations, i_GameMove.ChooseLocations);
                        char currentPlayerMark = m_Board.GetCurrentLocationPlayerMark(i_GameMove.ChooseLocations.m_Row, i_GameMove.ChooseLocations.m_Col);
                        m_Board.UpdateKing(i_GameMove.ChooseLocations, currentPlayerMark);
                        boardImplementsEventArgs = new BoardImplementEventArgs(i_GameMove);
                        OnBoardImplement(boardImplementsEventArgs);
                        SwitchPlayer(ref m_CurrentPlayer, ref m_OppositePlayer);
                        o_IsMoveApplied = true;
                    }

                    else

                    {
                        o_IsMoveApplied = false;
                    }
                }

            if (IsGameOver(ref m_Board, m_CurrentPlayer.PlayerNumber))

            {
                eGameState gameState = DetermineGameOverState(ref m_Board, ref m_OppositePlayer);
                gameOverEventArgs = new GameOverEventArgs(gameState);
                OnGameOver(gameOverEventArgs);
            }

            else if (m_CurrentPlayer.PlayerType == ePlayerType.Computer)

            {
                m_ComputerMovesList = i_GameMove.ValidComputerSkippingMoves(ref m_Board);

                if (m_ComputerMovesList.Count > 0)

                {
                    computerMove = i_GameMove.RandomSkippingComputerMove(m_ComputerMovesList);
                    m_Board.RemoveMark(computerMove.ChooseLocations, computerMove.CurrentLocations);
                    m_Board.UpdateBoard(computerMove.CurrentLocations, computerMove.ChooseLocations);
                    char currentPlayerMark = m_Board.GetCurrentLocationPlayerMark(computerMove.ChooseLocations.m_Row, computerMove.ChooseLocations.m_Col);
                    m_Board.UpdateKing(computerMove.ChooseLocations, currentPlayerMark);
                    boardImplementsEventArgs = new BoardImplementEventArgs(computerMove);
                    OnBoardImplement(boardImplementsEventArgs);
                    o_IsMoveApplied = true;

                    m_ComputerMovesList = i_GameMove.ValidComputerSkippingMoves(ref m_Board);

                    if (m_ComputerMovesList.Count < 1)

                    {
                        SwitchPlayer(ref m_CurrentPlayer, ref m_OppositePlayer);
                    }

                    else

                    {
                        ApplyMove(computerMove);
                    }

                }

                else

                {
                    computerMove = i_GameMove.RandomComputerMove(ref m_Board);
                    m_Board.UpdateBoard(computerMove.CurrentLocations, computerMove.ChooseLocations);
                    char currentPlayerMark = m_Board.GetCurrentLocationPlayerMark(computerMove.ChooseLocations.m_Row, computerMove.ChooseLocations.m_Col);
                    m_Board.UpdateKing(computerMove.ChooseLocations, currentPlayerMark);
                    boardImplementsEventArgs = new BoardImplementEventArgs(computerMove);
                    OnBoardImplement(boardImplementsEventArgs);
                    SwitchPlayer(ref m_CurrentPlayer, ref m_OppositePlayer);
                    o_IsMoveApplied = true;
                }
            }

            if (IsGameOver(ref m_Board, m_CurrentPlayer.PlayerNumber))

            {
                eGameState gameState = DetermineGameOverState(ref m_Board, ref m_OppositePlayer);
                gameOverEventArgs = new GameOverEventArgs(gameState);
                OnGameOver(gameOverEventArgs);
            }

            return o_IsMoveApplied;

        }

        public void OnNewGame()
        {
            EventArgs e = new EventArgs();

            if (NewGame != null)

            {
                NewGame(this, e);
            }
        }

        public void InitializeNewGame(eBoardSize i_BoardSize, string i_Player1Name, string i_Player2Name, bool i_IsPlayerAComputer)
        {
            m_Board = new Board(i_BoardSize);
            m_Board.InitializeBoard(i_BoardSize);
            SetPlayers(i_Player1Name, i_Player2Name, i_IsPlayerAComputer);
            OnNewGame();
        }

        protected virtual void OnGameOver(GameOverEventArgs e)
        {
            if (GameOver != null)

            {
                GameOver(this, e);
            }
        }

    }
}
