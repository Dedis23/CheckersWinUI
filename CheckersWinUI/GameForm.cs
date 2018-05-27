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
        private readonly List<CheckersSoldier> r_CheckersBoard;
        private Label m_Player1Label;
        private Label m_Player2Label;

        public GameForm(int i_BoardSize, bool i_Player2Enable, string i_Player1Name, string i_Player2Name)
        {
            StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Text = "Damka";
            this.ShowIcon = false;
            r_LogicUnit = new LogicUnit(i_BoardSize);
            buildLogicUnit(i_Player2Enable, i_Player1Name, i_Player2Name);
            r_CheckersBoard = new List<CheckersSoldier>(i_BoardSize * i_BoardSize);
            createGameFrame(i_BoardSize);
        }

        private void buildLogicUnit(bool i_Player2Enable, string i_Player1Name, string i_Player2Name)
        {
            r_LogicUnit.CreateBoard();
            r_LogicUnit.CreatePlayerOne(i_Player1Name);
            string playerTwoName = string.Empty;
            if (i_Player2Enable == false)
            {
                playerTwoName = i_Player2Name.Trim('[', ']');
            }
            r_LogicUnit.CreatePlayerTwo(playerTwoName);
        }

        private void createGameFrame(int i_BoardSize)
        {
            buildFrame(i_BoardSize);
            buildCheckersBoard(i_BoardSize);
            buildInnerForms(i_BoardSize);
        }

        private void buildInnerForms(int i_BoardSize)
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
                for (int col = 0; col< i_BoardSize; col++)
                {
                    if (row % 2 == 0 && col % 2 == 0 || row % 2 == 1 && col % 2 == 1)
                    {
                        r_CheckersBoard.Add(new CheckersSoldier(CheckersSoldier.eSoldierType.None, false));
                        this.Controls.Add(r_CheckersBoard[row * i_BoardSize + col]);
                    }
                    else if (row < i_BoardSize / 2 - 1 && (row % 2 == 0 && col % 2 == 1 || row % 2 == 1 && col % 2 == 0))
                    {
                        r_CheckersBoard.Add(new CheckersSoldier(CheckersSoldier.eSoldierType.BlackPawn, true));
                        this.Controls.Add(r_CheckersBoard[row * i_BoardSize + col]);
                    }
                    else if (row >= i_BoardSize / 2 + 1 && (row % 2 == 0 && col % 2 == 1 || row % 2 == 1 && col % 2 == 0))
                    {
                        r_CheckersBoard.Add(new CheckersSoldier(CheckersSoldier.eSoldierType.WhitePawn, true));
                        this.Controls.Add(r_CheckersBoard[row * i_BoardSize + col]);
                    }
                    else
                    {
                        r_CheckersBoard.Add(new CheckersSoldier(CheckersSoldier.eSoldierType.None, true));
                        this.Controls.Add(r_CheckersBoard[row * i_BoardSize + col]);
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
    }
}
