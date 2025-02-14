using System;
using System.Drawing;
using System.Windows.Forms;
using GameLogic;

namespace GameFront
{
    public partial class FormGame : Form
    {
        private readonly GameLogicManager r_GameLogicManager;
        private int m_SelectedRow;
        private int m_SelectedCol;
        private Button m_SelectedTile;
        private Button[,] m_BoardTiles;

        public FormGame(string i_Player1Name, string i_Player2Name, bool i_IsPlayerComputer, int i_BoardSize)
        {
            r_GameLogicManager = new GameLogicManager();
            r_GameLogicManager.BoardImplement += GameLogicManager_BoardImplement;
            r_GameLogicManager.GameOver += GameLogicManager_GameOver;
            r_GameLogicManager.InitializeNewGame((eBoardSize)i_BoardSize, i_Player1Name, i_Player2Name, i_IsPlayerComputer);
            InitializeComponent();
            this.Player1NameLabel.Text = i_Player1Name + ":";
            this.Player2NameLabel.Text = i_Player2Name + ":";
            r_GameLogicManager.GetBoard.InitializeBoard((eBoardSize)i_BoardSize);
            createBoard(i_BoardSize);         
        }

        private void createBoard(int i_BoardSize)
        {
            int tileSize = this.GameBoardPanel.Size.Height / i_BoardSize;
            m_BoardTiles = new Button[i_BoardSize, i_BoardSize];

            this.GameBoardPanel.Width = (i_BoardSize + 1) * tileSize;
            this.GameBoardPanel.Height = (i_BoardSize + 1) * tileSize;

            for (int i = 0; i < i_BoardSize; i++)

            {
                for (int j = 0; j < i_BoardSize; j++)

                {
                    Button button = new Button
                    {
                        Location = new Point(j * tileSize, i * tileSize),
                        Size = new Size(tileSize, tileSize),
                        BackColor = ((i + j) % 2 == 0) ? Color.White : Color.FromArgb(255,162,209,253),
                        Tag = new MoveLocations(i, j)
                    };

                    char piece = r_GameLogicManager.GetBoard.GetCurrentLocationPlayerMark(i, j);

                    if (piece == 'O')

                    {
                        Bitmap resizedImage = new Bitmap(GameFront.Properties.Resources.WhitePiece, tileSize, tileSize);
                        button.Image = resizedImage;
                    }

                    else if (piece == 'X')

                    {
                        Bitmap resizedImage = new Bitmap(GameFront.Properties.Resources.BlackPiece, tileSize, tileSize);
                        button.Image = resizedImage;
                    }

                    else if ((i + j) % 2 == 0)

                    {
                        button.Enabled = false;
                    }

                    button.ImageAlign = ContentAlignment.MiddleCenter;

                    m_BoardTiles[i, j] = button;

                    button.Click += tile_Click;

                    this.GameBoardPanel.Controls.Add(button);
                }
            }
        }

        private void updateBoard()
        {
            int boardSize = (int)this.r_GameLogicManager.GetBoard.GetBoardSize;

            for (int i = 0; i < boardSize; i++)

            {
                for (int j = 0; j < boardSize; j++)

                {
                    char piece = r_GameLogicManager.GetBoard.GetCurrentLocationPlayerMark(i, j);

                    Button tile = GameBoardPanel.Controls[i * boardSize + j] as Button;
                    Image pieceImage = null;

                    tile.BackColor = Color.FromArgb(255, 162, 209, 253);

                    if (piece == (char)ePlayerMark.Player2KingMark)

                    {
                        pieceImage = Properties.Resources.BlackKingPiece;
                    }

                    else if (piece == (char)ePlayerMark.Player2Mark)

                    {
                        pieceImage = Properties.Resources.BlackPiece;
                    }

                    else if (piece == (char)ePlayerMark.Player1KingMark)

                    {
                        pieceImage = Properties.Resources.WhiteKingPiece;
                    }

                    else if (piece == (char)ePlayerMark.Player1Mark)

                    {
                        pieceImage = Properties.Resources.WhitePiece;
                    }

                    else

                    {
                        pieceImage = (i + j) % 2 == 0 ? Properties.Resources.Board : Properties.Resources.PieceHolder;
                    }

                    if (pieceImage != null)

                    {
                        Bitmap resizedImage = new Bitmap(pieceImage, tile.Size);
                        tile.Image = resizedImage;
                    }
                }
            }
        }
        
        private void tile_Click(object sender, EventArgs e)
        {
            Button clickedTile = sender as Button;
            MoveLocations location = (MoveLocations)clickedTile.Tag;
            int row = location.m_Row;
            int col = location.m_Col;

            if (m_SelectedTile == null)

            {
                if (!r_GameLogicManager.IsCurrentPlayerPiece(row, col))

                {
                    MessageBox.Show("Invalid move! please choose your game piece");
                    return;
                }

                m_SelectedTile = clickedTile;
                m_SelectedRow = row;
                m_SelectedCol = col;
                clickedTile.BackColor = Color.FromArgb(255,75,107,148);
            }

            else

            {
                GameMove move = new GameMove
                {
                    CurrentLocations = new MoveLocations(m_SelectedRow, m_SelectedCol),
                    ChooseLocations = new MoveLocations(row, col)
                };

                if (r_GameLogicManager.ApplyMove(move))

                {
                    updateBoard();
                }

                else

                {
                    MessageBox.Show("Invalid move! please choose another move");
                    m_SelectedTile = null;
                    return;
                }

                m_SelectedTile.BackColor = ((m_SelectedRow + m_SelectedCol) % 2 == 0) ? Color.White : Color.Gray;
                m_SelectedTile = null;
            }
        }
        
        private void GameLogicManager_BoardImplement(object sender, EventArgs e)
        {
            BoardImplementEventArgs boardEventArgs = e as BoardImplementEventArgs;

            if (boardEventArgs != null)

            {
                updateBoard();
            }
        }

        private void GameLogicManager_GameOver(object sender, GameOverEventArgs e)
        {
            bool isPlayerAComputer = false;

            DialogResult result = MessageBox.Show(
                e.Message + Environment.NewLine + "Another round?",
                "Damka",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)

            {
                this.updateLabels();

                if (r_GameLogicManager.CurrentPlayer.PlayerNumber == ePlayerNumber.Player2)

                {
                    if (r_GameLogicManager.CurrentPlayer.PlayerType == ePlayerType.Computer)

                    {

                        isPlayerAComputer = true;
                    }

                    r_GameLogicManager.InitializeNewGame(
                        r_GameLogicManager.GetBoard.GetBoardSize,
                        r_GameLogicManager.OppositePlayer.PlayerName,
                        r_GameLogicManager.CurrentPlayer.PlayerName,
                        isPlayerAComputer
                    );
                }

                else

                {
                    if (r_GameLogicManager.OppositePlayer.PlayerType == ePlayerType.Computer)

                    {
                        isPlayerAComputer = true;
                    }

                    r_GameLogicManager.InitializeNewGame(
                        r_GameLogicManager.GetBoard.GetBoardSize,
                        r_GameLogicManager.CurrentPlayer.PlayerName,
                        r_GameLogicManager.OppositePlayer.PlayerName,
                        isPlayerAComputer
                    );

                }

            }

            else

            {
                Close();
            }
        }

        private void updateLabels()
        {
            if (r_GameLogicManager.CurrentPlayer.PlayerScore != 0 && (r_GameLogicManager.CurrentPlayer.PlayerNumber == ePlayerNumber.Player1))

            {
                this.Player1ScoreLabel.Text = r_GameLogicManager.CurrentPlayer.PlayerScore.ToString();
            }

            else if (r_GameLogicManager.CurrentPlayer.PlayerScore != 0 && (r_GameLogicManager.CurrentPlayer.PlayerNumber == ePlayerNumber.Player2))

            {
                this.Player2ScoreLabel.Text = r_GameLogicManager.CurrentPlayer.PlayerScore.ToString();
            }

            if (r_GameLogicManager.OppositePlayer.PlayerScore != 0 && (r_GameLogicManager.OppositePlayer.PlayerNumber == ePlayerNumber.Player1))

            {
                this.Player1ScoreLabel.Text = r_GameLogicManager.OppositePlayer.PlayerScore.ToString();
            }

            else if (r_GameLogicManager.OppositePlayer.PlayerScore != 0 && (r_GameLogicManager.OppositePlayer.PlayerNumber == ePlayerNumber.Player2))

            {
                this.Player2ScoreLabel.Text = r_GameLogicManager.OppositePlayer.PlayerScore.ToString();
            }
        }

    }
}
