using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using CheckersLogic;

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

        private const int k_HeightSize6x6 = 390;
        private const int k_WidthSize6x6 = 320;
        private const int k_HeightSize8x8 = 480;
        private const int k_WidthSize8x8 = 410;
        private const int k_HeightSize10x10 = 570;
        private const int k_WidthSize10x10 = 500;
        private const int k_InitialBoardTop = 50;
        private const int k_IntialBoardLeft = 20;
        private readonly LogicUnit r_LogicUnit;
        private readonly List<BoardSquare> r_CheckersBoard;
        private Label labelPlayer1;
        private Label labelPlayer2;
        private Button buttonForfeit;
        private Label labelCurrentPlayer;
        private BoardSquare boardSquareActiveSquare;

        public GameForm(int i_BoardSize, bool i_Player2Enable, string i_Player1Name, string i_Player2Name)
        {
            StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Text = "Damka";
            this.ShowIcon = false;
            this.boardSquareActiveSquare = null;
            r_LogicUnit = new LogicUnit(i_BoardSize);
            initializeLogicUnit(i_Player2Enable, i_Player1Name, i_Player2Name);
            r_CheckersBoard = new List<BoardSquare>(i_BoardSize * i_BoardSize);
            createGameFrame(i_BoardSize);
            this.BackColor = Color.NavajoWhite;
        }

        private void initializeLogicUnit(bool i_Player2Enable, string i_Player1Name, string i_Player2Name)
        {
            r_LogicUnit.CreateBoard();
            r_LogicUnit.CreatePlayerOne(i_Player1Name);
            string playerTwoName = string.Empty;
            if (i_Player2Enable == false)
            {
                // player vs computer
                r_LogicUnit.InitializeAI();
                r_LogicUnit.Mode = LogicUnit.eGameMode.PlayerVsComputer;
                playerTwoName = i_Player2Name.Trim('[', ']');
            }
            else
            {
                // player vs player
                r_LogicUnit.Mode = LogicUnit.eGameMode.PlayerVsPlayer;
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
            buildStatusBars();
        }

        private void buildStatusBars()
        {
            // Player1 Label
            StringBuilder player1TextLabel = new StringBuilder(r_LogicUnit.PlayerOne.Name);
            player1TextLabel.Append(": ");
            player1TextLabel.Append(r_LogicUnit.PlayerOne.Score.ToString());
            labelPlayer1 = new Label();
            labelPlayer1.Text = player1TextLabel.ToString();
            labelPlayer1.Font = new Font("Consolas", 9, FontStyle.Bold);
            labelPlayer1.AutoSize = true;
            labelPlayer1.ForeColor = Color.Black;
            labelPlayer1.BackColor = Color.Transparent;
            labelPlayer1.TextAlign = ContentAlignment.MiddleLeft;
            labelPlayer1.Top = r_CheckersBoard[0].Bounds.Top - labelPlayer1.Height - 10;
            labelPlayer1.Left = r_CheckersBoard[0].Bounds.Left;

            // Forfeit button
            buttonForfeit = new Button();
            buttonForfeit.Text = "Forfeit";
            buttonForfeit.AutoSize = true;
            buttonForfeit.Click += forfeitButton_Click;
            buttonForfeit.TextAlign = ContentAlignment.MiddleCenter;
            buttonForfeit.BackColor = Color.Transparent;
            buttonForfeit.FlatStyle = FlatStyle.Flat;
            buttonForfeit.Top = r_CheckersBoard[r_LogicUnit.Board.Size / 2].Bounds.Top - buttonForfeit.Height - 15;
            buttonForfeit.Left = r_CheckersBoard[r_LogicUnit.Board.Size / 2].Bounds.Left - (buttonForfeit.Width / 2);

            // Player2 Label
            StringBuilder player2TextLabel = new StringBuilder(r_LogicUnit.PlayerTwo.Name);
            player2TextLabel.Append(": ");
            player2TextLabel.Append(r_LogicUnit.PlayerTwo.Score.ToString());
            labelPlayer2 = new Label();
            labelPlayer2.Text = player2TextLabel.ToString();
            labelPlayer2.Font = new Font("Consolas", 9, FontStyle.Bold);
            labelPlayer2.ForeColor = Color.Black;
            labelPlayer2.AutoSize = true;
            labelPlayer2.BackColor = Color.Transparent;
            labelPlayer2.TextAlign = ContentAlignment.MiddleLeft;
            labelPlayer2.Top = r_CheckersBoard[r_LogicUnit.Board.Size - 2].Bounds.Top - labelPlayer2.Height - 10;
            labelPlayer2.Left = r_CheckersBoard[r_LogicUnit.Board.Size - 2].Bounds.Left;

            // Current Player Label
            StringBuilder labelCurrentPlayerText = new StringBuilder("Current Player: ");
            labelCurrentPlayerText.Append(r_LogicUnit.PlayerOne.Name);
            labelCurrentPlayer = new Label();
            labelCurrentPlayer.Font = new Font("Consolas", 9, FontStyle.Bold);
            labelCurrentPlayer.ForeColor = Color.Black;
            labelCurrentPlayer.AutoSize = true;
            labelCurrentPlayer.BackColor = Color.Transparent;
            labelCurrentPlayer.TextAlign = ContentAlignment.MiddleLeft;
            labelCurrentPlayer.Text = labelCurrentPlayerText.ToString();
            labelCurrentPlayer.Top = r_CheckersBoard[r_LogicUnit.Board.Size * r_LogicUnit.Board.Size - r_LogicUnit.Board.Size - 1].Bounds.Bottom + labelCurrentPlayer.Height + 20;
            labelCurrentPlayer.Left = r_CheckersBoard[r_LogicUnit.Board.Size * r_LogicUnit.Board.Size - r_LogicUnit.Board.Size].Bounds.Left;

            this.Controls.Add(labelPlayer1);
            this.Controls.Add(buttonForfeit);
            this.Controls.Add(labelPlayer2);
            this.Controls.Add(labelCurrentPlayer);
        }

        private void forfeitButton_Click(object sender, EventArgs e)
        {
            bool isCurrentPlayerForfeit = r_LogicUnit.CheckIfCurrentPlayerForfeit();
            if (isCurrentPlayerForfeit == true)
            {
                if (currentPlayerForfeitMessageBox() == true)
                {
                    initializeRematch();
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Current player cannot forfeit!", this.Text);
            }
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
                    if ((row % 2 == 0 && col % 2 == 0) || (row % 2 == 1 && col % 2 == 1))
                    {
                        r_CheckersBoard.Add(new BoardSquare(BoardSquare.eBoardSquareType.None, false, row, col));
                        this.Controls.Add(r_CheckersBoard[(row * i_BoardSize) + col]);
                    }
                    else if ((row < (i_BoardSize / 2) - 1) && ((row % 2 == 0 && col % 2 == 1) || (row % 2 == 1 && col % 2 == 0)))
                    {
                        r_CheckersBoard.Add(new BoardSquare(BoardSquare.eBoardSquareType.BlackPawn, true, row, col));
                        this.Controls.Add(r_CheckersBoard[(row * i_BoardSize) + col]);
                    }
                    else if ((row >= (i_BoardSize / 2) + 1) && ((row % 2 == 0 && col % 2 == 1) || (row % 2 == 1 && col % 2 == 0)))
                    {
                        r_CheckersBoard.Add(new BoardSquare(BoardSquare.eBoardSquareType.WhitePawn, true, row, col));
                        this.Controls.Add(r_CheckersBoard[(row * i_BoardSize) + col]);
                    }
                    else
                    {
                        r_CheckersBoard.Add(new BoardSquare(BoardSquare.eBoardSquareType.None, true, row, col));
                        this.Controls.Add(r_CheckersBoard[(row * i_BoardSize) + col]);
                    }

                    if (r_CheckersBoard[(row * i_BoardSize) + col] != null)
                    {
                        r_CheckersBoard[(row * i_BoardSize) + col].Click += new EventHandler(boardSquare_Clicked);
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
                    r_CheckersBoard[(row * i_BoardSize) + col].Top = currentTopCheckersBoard;
                    r_CheckersBoard[(row * i_BoardSize) + col].Left = currentLeftCheckersBoard + (col * widthOfCheckersSoldier);
                }

                currentTopCheckersBoard = currentTopCheckersBoard + heightOfCheckersSoldier;
            }
        }

        private void boardSquare_Clicked(object sender, EventArgs e)
        {
            BoardSquare clickedSquare = sender as BoardSquare;
            if (clickedSquare.Enabled == true)
            {
                if (boardSquareActiveSquare == null && clickedSquare.BoardSquareType != BoardSquare.eBoardSquareType.None)
                {   // board has no active square
                    clickedSquare.SetActive();
                    boardSquareActiveSquare = clickedSquare;
                }
                else if (boardSquareActiveSquare != null)
                {
                    // board has an active square, check if its a move or we click on the same square again to inactive
                    if (clickedSquare == boardSquareActiveSquare)
                    {
                        clickedSquare.SetInActive();
                        boardSquareActiveSquare = null;
                    }
                    else
                    {
                        // if we got to here, that means the user wanted to make a move
                        // Human turn
                        handleHumanTurn(clickedSquare);

                        // Computer turn
                        if (r_LogicUnit.Mode == LogicUnit.eGameMode.PlayerVsComputer && r_LogicUnit.CurrentTurn == LogicUnit.eCurrentPlayerTurn.Black)
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

                        // update the boards graphics
                        updateBoardGraphics();
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
                    initializeRematch();
                }
                else
                {
                    r_LogicUnit.Status = LogicUnit.eGameStatus.Quit;
                    this.Close();
                }
            }
            else
            {
                // switch turns
                switchTurns();
            }
        }

        private void initializeRematch()
        {
            r_LogicUnit.InitializeRematch();
            updateBoardGraphics();
            updateScoreLabels();
        }

        private void updateScoreLabels()
        {
            StringBuilder player1TextLabel = new StringBuilder(r_LogicUnit.PlayerOne.Name);
            player1TextLabel.Append(": ");
            player1TextLabel.Append(r_LogicUnit.PlayerOne.Score.ToString());
            labelPlayer1.Text = player1TextLabel.ToString();
            StringBuilder player2TextLabel = new StringBuilder(r_LogicUnit.PlayerTwo.Name);
            player2TextLabel.Append(": ");
            player2TextLabel.Append(r_LogicUnit.PlayerTwo.Score.ToString());
            labelPlayer2.Text = player2TextLabel.ToString();
        }

        private void updateCurrentTurnLabel(string i_CurrentPlayerName)
        {
            StringBuilder labelCurrentPlayerText = new StringBuilder("Current Player: ");
            labelCurrentPlayerText.Append(i_CurrentPlayerName);
            labelCurrentPlayer.Text = labelCurrentPlayerText.ToString();
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
                case LogicUnit.eCurrentPlayerTurn.Black:
                    currentPlayerName = r_LogicUnit.PlayerTwo.Name;
                    break;
                case LogicUnit.eCurrentPlayerTurn.White:
                    currentPlayerName = r_LogicUnit.PlayerOne.Name;
                    break;
                default:
                    break;
            }

            StringBuilder messageBoxMsg = new StringBuilder(currentPlayerName);
            messageBoxMsg.Append(" has won!");
            messageBoxMsg.AppendLine();
            messageBoxMsg.Append("Another round?");
            if (MessageBox.Show(messageBoxMsg.ToString(), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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

            // check if either player has won, lost or its a tie
            manageTasksBeforeNextTurn();
        }

        private bool checkIfValidTurnAndPreformIt(int i_ColDestination, int i_RowDestination)
        {
            bool isValidTurn = false;
            CheckersLogic.Point startingPoint = new CheckersLogic.Point(boardSquareActiveSquare.Row, boardSquareActiveSquare.Col);
            CheckersLogic.Point destinationPoint = new CheckersLogic.Point(i_RowDestination, i_ColDestination);
            if (r_LogicUnit.PreformMove(startingPoint, destinationPoint) == true)
            {
                isValidTurn = true;
            }
            else
            {
                MessageBox.Show(
@"Invalid move!
Please try again.",
this.Text);
                boardSquareActiveSquare.SetInActive();
                boardSquareActiveSquare = null;
            }

            return isValidTurn;
        }

        private void switchTurns()
        {
            if (r_LogicUnit.ExtraHumanTurn == false)
            {
                r_LogicUnit.SwitchTurns();
                string currentPlayerName = string.Empty;
                switch (r_LogicUnit.CurrentTurn)
                {
                    case LogicUnit.eCurrentPlayerTurn.Black:
                        currentPlayerName = r_LogicUnit.PlayerTwo.Name;
                        break;
                    case LogicUnit.eCurrentPlayerTurn.White:
                        currentPlayerName = r_LogicUnit.PlayerOne.Name;
                        break;
                    default:
                        break;
                }
                updateCurrentTurnLabel(currentPlayerName);
            }
        }

        private bool currentPlayerForfeitMessageBox()
        {
            bool isAnotherRound = false;
            string playerThatWonName = string.Empty;
            switch (r_LogicUnit.CurrentTurn)
            {
                case LogicUnit.eCurrentPlayerTurn.Black:
                    playerThatWonName = r_LogicUnit.PlayerOne.Name;
                    break;
                case LogicUnit.eCurrentPlayerTurn.White:
                    playerThatWonName = r_LogicUnit.PlayerTwo.Name;
                    break;
                default:
                    break;
            }

            StringBuilder messageBoxMsg = new StringBuilder(playerThatWonName);
            messageBoxMsg.Append(" has won!");
            messageBoxMsg.AppendLine();
            messageBoxMsg.Append("Another round?");
            if (MessageBox.Show(messageBoxMsg.ToString(), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                isAnotherRound = true;
            }

            return isAnotherRound;
        }

        private void updateBoardGraphics()
        {
            for (int rows = 0; rows < r_LogicUnit.Board.Size; rows++)
            {
                for (int cols = 0; cols < r_LogicUnit.Board.Size; cols++)
                {
                    if (r_LogicUnit.Board.GameBoard[rows, cols] == r_LogicUnit.Board.PlayerOneSign)
                    {
                        r_CheckersBoard[(rows * r_LogicUnit.Board.Size) + cols].SetSquareImage(BoardSquare.eBoardSquareType.WhitePawn);
                    }
                    else if (r_LogicUnit.Board.GameBoard[rows, cols] == r_LogicUnit.Board.PlayerOneKingSign)
                    {
                        r_CheckersBoard[(rows * r_LogicUnit.Board.Size) + cols].SetSquareImage(BoardSquare.eBoardSquareType.WhiteKing);
                    }
                    else if (r_LogicUnit.Board.GameBoard[rows, cols] == r_LogicUnit.Board.PlayerTwoSign)
                    {
                        r_CheckersBoard[(rows * r_LogicUnit.Board.Size) + cols].SetSquareImage(BoardSquare.eBoardSquareType.BlackPawn);
                    }
                    else if (r_LogicUnit.Board.GameBoard[rows, cols] == r_LogicUnit.Board.PlayerTwoKingSign)
                    {
                        r_CheckersBoard[(rows * r_LogicUnit.Board.Size) + cols].SetSquareImage(BoardSquare.eBoardSquareType.BlackKing);
                    }
                    else
                    {
                        r_CheckersBoard[(rows * r_LogicUnit.Board.Size) + cols].SetSquareImage(BoardSquare.eBoardSquareType.None);
                    }
                }
            }

            if (boardSquareActiveSquare != null)
            {
                boardSquareActiveSquare.SetInActive();
                boardSquareActiveSquare = null;
            }
        }
    }
}