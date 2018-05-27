using System;
using System.Windows.Forms;
using CheckersLogic;
using System.Collections.Generic;


namespace CheckersWinUI
{
    public class GameForm : Form
    {
        private readonly LogicUnit r_LogicUnit;
        private readonly List<CheckersSoldier> r_CheckersBoard;

        public GameForm(int i_BoardSize, bool i_Player2Enable, string i_Player1Name, string i_Player2Name)
        {
            StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Height = 1000;
            this.Text = "Damka";
            this.ShowIcon = false;
            r_LogicUnit = new LogicUnit(i_BoardSize);
            r_CheckersBoard = new List<CheckersSoldier>(i_BoardSize * i_BoardSize);
            createBoard(i_BoardSize);
            r_LogicUnit.CreatePlayerOne(i_Player1Name);
        }

        private void createBoard(int i_BoardSize)
        {
            r_LogicUnit.CreateBoard();
            buildCheckersBoard(i_BoardSize);
        }

        private void buildCheckersBoard(int i_BoardSize)
        {
            int CurrentTopCheckersBoard = 50;
            int CurrentLeftCheckersBoard = 30;

            for (int row = 0; row < i_BoardSize; row++)
            {
                for (int col = 0; col < i_BoardSize; col++)
                {
                    if (row % 2 == 0 && col % 2 == 0 || row % 2 == 1 && col % 2 == 1)
                    {
                        r_CheckersBoard.Add(new CheckersSoldier(CheckersSoldier.eSoldierType.None, false));
                        this.Controls.Add(r_CheckersBoard[row * i_BoardSize + col]);
                    }
                    else if (row < i_BoardSize / 2 - 1 && (row % 2 == 0 && col % 2 == 1 || row % 2 == 1 && col % 2 == 0))
                    {
                        r_CheckersBoard.Add(new CheckersSoldier(CheckersSoldier.eSoldierType.WhitePawn, true));
                        this.Controls.Add(r_CheckersBoard[row * i_BoardSize + col]);
                    }
                    else if (row >= i_BoardSize / 2 + 1 && (row % 2 == 0 && col % 2 == 1 || row % 2 == 1 && col % 2 == 0))
                    {
                        r_CheckersBoard.Add(new CheckersSoldier(CheckersSoldier.eSoldierType.BlackPawn, true));
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

            for (int row = 0; row < i_BoardSize; row++)
            {
                for (int col = 0; col < i_BoardSize; col++)
                {
                    r_CheckersBoard[row * i_BoardSize + col].Top = CurrentTopCheckersBoard;
                    r_CheckersBoard[row * i_BoardSize + col].Left = CurrentLeftCheckersBoard + col * widthOfCheckersSoldier;
                }
                CurrentTopCheckersBoard = CurrentTopCheckersBoard + heightOfCheckersSoldier;
            }
        }
    }
}
