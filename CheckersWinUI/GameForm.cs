using System;
using System.Windows.Forms;
using CheckersLogic;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CheckersWinUI
{
    public class GameForm : Form
    {
        public enum eBoardSize
        {
            Small = 6,
            Medium = 8,
            Large = 10,
        }

        private const int k_HeightSize6x6 = 380;
        private const int k_WidthSize6x6 = 320;
        private const int k_HeightSize8x8 = 470;
        private const int k_WidthSize8x8 = 410;
        private const int k_HeightSize10x10 = 560;
        private const int k_WidthSize10x10 = 500;
        private const int k_InitialBoardTop = 50;
        private const int k_IntialBoardLeft = 20;
        private readonly LogicUnit r_LogicUnit;
        private readonly List<BoardSquare> r_CheckersBoard;
        private Label m_Player1Label;
        private Label m_Player2Label;
        private BoardSquare m_ActiveSquare;

        public GameForm(int i_BoardSize, bool i_Player2Enable, string i_Player1Name, string i_Player2Name)
        {
            StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Text = "Damka";
            this.ShowIcon = false;
            this.m_ActiveSquare = null;
            r_LogicUnit = new LogicUnit(i_BoardSize);
            initializeLogicUnit(i_Player2Enable, i_Player1Name, i_Player2Name);
            r_CheckersBoard = new List<BoardSquare>(i_BoardSize * i_BoardSize);
            createGameFrame(i_BoardSize);
        }

        private void initializeLogicUnit(bool i_Player2Enable, string i_Player1Name, string i_Player2Name)
        {
            r_LogicUnit.CreateBoard();
            r_LogicUnit.CreatePlayerOne(i_Player1Name);
            string playerTwoName = string.Empty;
            if (i_Player2Enable == false)
            {
                // player vs player
                r_LogicUnit.Mode = LogicUnit.eGameMode.PlayerVsPlayer;
                playerTwoName = i_Player2Name.Trim('[', ']');
            }
            else
            {
                // player vs computer
                r_LogicUnit.InitializeAI();
                r_LogicUnit.Mode = LogicUnit.eGameMode.PlayerVsComputer;
                playerTwoName = i_Player2Name;
            }
            r_LogicUnit.CreatePlayerTwo(playerTwoName);
            r_LogicUnit.InitializeCoins();
            r_LogicUnit.Status = LogicUnit.eGameStatus.Play;
        }

        private void createGameFrame(int i_BoardSize)
        {
            buildFrame(i_BoardSize);
            buildCheckersBoard(i_BoardSize);
            buildPlayersScoreBar(i_BoardSize);
        }

        private void buildPlayersScoreBar(int i_BoardSize)
        {
            StringBuilder player1TextLabel = new StringBuilder(r_LogicUnit.PlayerOne.Name);
            player1TextLabel.Append(": ");
            player1TextLabel.Append(r_LogicUnit.PlayerOne.Score.ToString());

            // Player1 Label
            m_Player1Label = new Label();
            m_Player1Label.Text = player1TextLabel.ToString();
            m_Player1Label.Font = new Font("Arial", 9, FontStyle.Bold);
            m_Player1Label.ForeColor = Color.Black;
            m_Player1Label.Top = 15;
            m_Player1Label.Left = 60 + (i_BoardSize - 6) * 20;

            StringBuilder player2TextLabel = new StringBuilder(r_LogicUnit.PlayerTwo.Name);
            player2TextLabel.Append(": ");
            player2TextLabel.Append(r_LogicUnit.PlayerTwo.Score.ToString());

            // Player2 Label
            m_Player2Label = new Label();
            m_Player2Label.Text = player2TextLabel.ToString();
            m_Player2Label.Font = new Font("Arial", 9, FontStyle.Bold);
            m_Player2Label.ForeColor = Color.Black;
            m_Player2Label.Top = 15;
            m_Player2Label.Left = m_Player1Label.Left + 120;

            this.Controls.Add(m_Player2Label);
            this.Controls.Add(m_Player1Label);

        }

        private void buildFrame(int i_BoardSize)
        {
            switch ((eBoardSize)i_BoardSize)
            {
                case eBoardSize.Small:
                    this.Height = k_HeightSize6x6;
                    this.Width = k_WidthSize6x6;
                    break;
                case eBoardSize.Medium:
                    this.Height = k_HeightSize8x8;
                    this.Width = k_WidthSize8x8;
                    break;
                case eBoardSize.Large:
                    this.Height = k_HeightSize10x10;
                    this.Width = k_WidthSize10x10;
                    break;
                default:
                    break;
            }
        }

        private void buildCheckersBoard(int i_BoardSize)
        {
            for (int row = 0; row < i_BoardSize; row++)
            {
                for (int col = 0; col < i_BoardSize; col++)
                {
                    if (row % 2 == 0 && col % 2 == 0 || row % 2 == 1 && col % 2 == 1)
                    {
                        r_CheckersBoard.Add(new BoardSquare(BoardSquare.eBoardSquareType.None, false, row, col));
                        this.Controls.Add(r_CheckersBoard[row * i_BoardSize + col]);
                    }
                    else if (row < i_BoardSize / 2 - 1 && (row % 2 == 0 && col % 2 == 1 || row % 2 == 1 && col % 2 == 0))
                    {
                        r_CheckersBoard.Add(new BoardSquare(BoardSquare.eBoardSquareType.BlackPawn, true, row, col));
                        this.Controls.Add(r_CheckersBoard[row * i_BoardSize + col]);
                    }
                    else if (row >= i_BoardSize / 2 + 1 && (row % 2 == 0 && col % 2 == 1 || row % 2 == 1 && col % 2 == 0))
                    {
                        r_CheckersBoard.Add(new BoardSquare(BoardSquare.eBoardSquareType.WhitePawn, true, row, col));
                        this.Controls.Add(r_CheckersBoard[row * i_BoardSize + col]);
                    }
                    else
                    {
                        r_CheckersBoard.Add(new BoardSquare(BoardSquare.eBoardSquareType.None, true, row, col));
                        this.Controls.Add(r_CheckersBoard[row * i_BoardSize + col]);
                    }

                    if (r_CheckersBoard[row * i_BoardSize + col] != null)
                    {
                        r_CheckersBoard[row * i_BoardSize + col].Click += new EventHandler(boardSquare_Clicked);
                    }
                }
            }

            int heightOfCheckersSoldier = r_CheckersBoard[0].Height;
            int widthOfCheckersSoldier = r_CheckersBoard[0].Width;
            int currentTopCheckersBoard = k_InitialBoardTop;
            int currentLeftCheckersBoard = k_IntialBoardLeft;
            for (int row = 0; row < i_BoardSize; row++)
            {
                for (int col = 0; col < i_BoardSize; col++)
                {
                    r_CheckersBoard[row * i_BoardSize + col].Top = currentTopCheckersBoard;
                    r_CheckersBoard[row * i_BoardSize + col].Left = currentLeftCheckersBoard + col * widthOfCheckersSoldier;
                }
                currentTopCheckersBoard = currentTopCheckersBoard + heightOfCheckersSoldier;
            }
        }

        private void boardSquare_Clicked(object sender, EventArgs e)
        {
            BoardSquare clickedSquare = sender as BoardSquare;
            if (clickedSquare.Enabled == true)
            {
                if (m_ActiveSquare == null && clickedSquare.BoardSquareType != BoardSquare.eBoardSquareType.None)
                {   // board has no active square
                    clickedSquare.SetActive();
                    m_ActiveSquare = clickedSquare;
                }   
                else if (m_ActiveSquare != null)
                {
                    // board has an active square, check if its a move or we click on the same square again to inactive
                    if (clickedSquare == m_ActiveSquare)
                    {
                        clickedSquare.SetInActive();
                        m_ActiveSquare = null;
                    }
                    else
                    {
                        // if we got to here, that means the user wanted to make a move

                        // Human turn
                        handleHumanTurn(clickedSquare);

                        // Computer turn
                        if (r_LogicUnit.Mode == LogicUnit.eGameMode.PlayerVsComputer && r_LogicUnit.CurrentTurn == LogicUnit.eCurrentShapeTurn.Circle)
                        {   // if we are in player vs computer mode, and its the computer turn, make an AI move
                            bool continueComputerTurn = true;
                            while (continueComputerTurn == true)
                            {
                                handleComputerTurn();
                                if (r_LogicUnit.ExtraAITurn == false)
                                {
                                    continueComputerTurn = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void manageTasksBeforeNextTurn()
        {
            bool isAnotherRound = false;
            r_LogicUnit.CheckIfBothSidesHaveMoreAvailableMoves();
            bool isItATie = r_LogicUnit.CheckForATie();
            if (isItATie == true)
            {
                isAnotherRound = itsATieMessageBox();
            }
            else
            {
                // TO DO THIS (GUY RONEN DIDNT EXPLAIN IF FORFEIT STILL EXIST (Q) NEED TO ASK HIM)
                /* 
                bool isCurrentPlayerForfeit = r_LogicUnit.CheckIfCurrentPlayerForfeit();
                if (isCurrentPlayerForfeit == true)
                {   // check if current player lost because he wanted to forfeit or he has no more moves
                    displayCurrentPlayerForfeitScreen();
                    displayCurrentScoresScreen();
                }*/

                bool isCurrentPlayerWon = r_LogicUnit.CheckIfCurrentPlayerWon();
                if (isCurrentPlayerWon == true)
                {
                    // check if current player won, either by destroying all the opponents coins or the opponents has no more moves
                    isAnotherRound = currentPlayerWonMessageBox();
                }
            }
            if (r_LogicUnit.Status == LogicUnit.eGameStatus.EndOfRound)
            {   // if the round is over, check for rematch
                if (isAnotherRound == true)
                {
                    r_LogicUnit.InitializeRematch();
                    updateBoardGraphics();
                    updateScoreLabels();
                }
                else
                {
                    r_LogicUnit.Status = LogicUnit.eGameStatus.Quit;
                    this.Hide();
                }
            }
            else
            {
                // switch turns
                switchTurns();
            }
        }

        private void updateScoreLabels()
        {
            StringBuilder player1TextLabel = new StringBuilder(r_LogicUnit.PlayerOne.Name);
            player1TextLabel.Append(": ");
            player1TextLabel.Append(r_LogicUnit.PlayerOne.Score.ToString());
            m_Player1Label.Text = player1TextLabel.ToString();
            StringBuilder player2TextLabel = new StringBuilder(r_LogicUnit.PlayerTwo.Name);
            player2TextLabel.Append(": ");
            player2TextLabel.Append(r_LogicUnit.PlayerTwo.Score.ToString());
            m_Player2Label.Text = player2TextLabel.ToString();
        }

        private bool itsATieMessageBox()
        {
            bool isAnotherRound = false;
            if (MessageBox.Show(
@"It's a tie!
Another round?",
this.Text,
MessageBoxButtons.YesNo,
MessageBoxIcon.Information) == DialogResult.Yes)
            {
                isAnotherRound = true;
            }
            return isAnotherRound;
        }

        private bool currentPlayerWonMessageBox()
        {
            bool isAnotherRound = false;
            string currentPlayerName = string.Empty;
            switch (r_LogicUnit.CurrentTurn)
            {
                case LogicUnit.eCurrentShapeTurn.Circle:
                    currentPlayerName = r_LogicUnit.PlayerTwo.Name;
                    break;
                case LogicUnit.eCurrentShapeTurn.Ex:
                    currentPlayerName = r_LogicUnit.PlayerOne.Name;
                    break;
                default:
                    break;
            }

            StringBuilder messageBoxMsg = new StringBuilder(currentPlayerName);
            messageBoxMsg.Append(" has won!");
            messageBoxMsg.AppendLine();
            messageBoxMsg.Append("Another round?");
            if (MessageBox.Show(messageBoxMsg.ToString(),this.Text,MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                isAnotherRound = true;
            }
            return isAnotherRound;
        }

        private void handleHumanTurn(BoardSquare i_ClickedSquare)
        {
            // check if the move is legal, if yes preform it
            if (checkIfValidTurnAndPreformIt(i_ClickedSquare.Col, i_ClickedSquare.Row) == true)
            {
                // update the boards graphics
                updateBoardGraphics();

                // check if either player has won, lost or its a tie
                manageTasksBeforeNextTurn();
            }
        }

        private void handleComputerTurn()
        {
            CheckersLogic.Point startingComputerPoint = null;
            CheckersLogic.Point destinationComputerPoint = null;
            r_LogicUnit.GetAnAIMove(ref startingComputerPoint, ref destinationComputerPoint); // make an AI move
            r_LogicUnit.PreformMove(startingComputerPoint, destinationComputerPoint); // preform the move (no need to use the returned boolean value because the AI always choose valid moves)

            // after AI turn we update the graphics
            updateBoardGraphics();

            // check if either player has won, lost or its a tie
            manageTasksBeforeNextTurn();
        }

        private bool checkIfValidTurnAndPreformIt(int i_ColDestination, int i_RowDestination)
        {
            bool isValidTurn = false;
            CheckersLogic.Point startingPoint = new CheckersLogic.Point(m_ActiveSquare.Row, m_ActiveSquare.Col);
            CheckersLogic.Point destinationPoint = new CheckersLogic.Point(i_RowDestination, i_ColDestination);
            if (r_LogicUnit.PreformMove(startingPoint, destinationPoint) == true)
            {
                isValidTurn = true;
            }
            else
            {
                MessageBox.Show(@"Invalid move!
Please try again.", this.Text);
                m_ActiveSquare.SetInActive();
                m_ActiveSquare = null;
            }
            return isValidTurn;
        }

        private void switchTurns()
        {
            bool switchTurns = false;
            if (r_LogicUnit.ExtraHumanTurn == false)
            {
                switchTurns = true;
            }
            if (switchTurns == true)
            {
                r_LogicUnit.SwitchTurns();
            }
        }

        private void updateBoardGraphics()
        {
            for (int rows = 0; rows < r_LogicUnit.Board.Size; rows++)
            {
                for (int cols = 0; cols < r_LogicUnit.Board.Size; cols++)
                {
                    if (r_LogicUnit.Board.GameBoard[rows, cols] == r_LogicUnit.Board.PlayerOneSign)
                    {
                        r_CheckersBoard[rows * r_LogicUnit.Board.Size + cols].SetSquareImage(BoardSquare.eBoardSquareType.WhitePawn);
                    }
                    else if (r_LogicUnit.Board.GameBoard[rows, cols] == r_LogicUnit.Board.PlayerOneKingSign)
                    {
                        r_CheckersBoard[rows * r_LogicUnit.Board.Size + cols].SetSquareImage(BoardSquare.eBoardSquareType.WhiteKing);
                    }
                    else if (r_LogicUnit.Board.GameBoard[rows, cols] == r_LogicUnit.Board.PlayerTwoSign)
                    {
                        r_CheckersBoard[rows * r_LogicUnit.Board.Size + cols].SetSquareImage(BoardSquare.eBoardSquareType.BlackPawn);
                    }
                    else if (r_LogicUnit.Board.GameBoard[rows, cols] == r_LogicUnit.Board.PlayerTwoKingSign)
                    {
                        r_CheckersBoard[rows * r_LogicUnit.Board.Size + cols].SetSquareImage(BoardSquare.eBoardSquareType.BlackKing);
                    }
                    else
                    {
                        r_CheckersBoard[rows * r_LogicUnit.Board.Size + cols].SetSquareImage(BoardSquare.eBoardSquareType.None);
                    }
                }
            }
            if (m_ActiveSquare != null)
            {
                m_ActiveSquare.SetInActive();
                m_ActiveSquare = null;
            }
        }
    }
}