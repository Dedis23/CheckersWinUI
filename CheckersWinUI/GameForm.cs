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
            this.Text = "Damka";
            this.ShowIcon = false;
            r_LogicUnit = new LogicUnit(i_BoardSize);
            r_CheckersBoard = new List<CheckersSoldier>(i_BoardSize * i_BoardSize);
            //createBoard();
            //r_LogicUnit.CreatePlayerOne(i_Player1Name);
        }

        private void createBoard()
        {
            r_LogicUnit.CreateBoard();
            buildCheckersBoard(r_CheckersBoard.Count);
        }

        private void buildCheckersBoard(int i_BoardSize)
        {
            int topCheckersBoard = 30;
            int leftCheckersBoard = 10;

            for (int row = 0; row < i_BoardSize; row++)
            {
                for (int col = 0; col < i_BoardSize; col++)
                {
                    if (row % 2 == 0 && col % 2 == 0 || row % 2 == 1 && col % 2 == 1)
                    {
                        r_CheckersBoard[row * i_BoardSize + col] = new CheckersSoldier(CheckersSoldier.eSoldierType.None, true);
                    }
                    else if (row < i_BoardSize / 2 - 1 && (row % 2 == 0 && col % 2 == 1 || row % 2 == 1 && col % 2 == 0))
                    {
                        r_CheckersBoard[row * i_BoardSize + col] = new CheckersSoldier(CheckersSoldier.eSoldierType.WhitePawn, false);
                    }
                    else if (row >= i_BoardSize / 2 + 1 && (row % 2 == 0 && col % 2 == 1 || row % 2 == 1 && col % 2 == 0))
                    {
                        r_CheckersBoard[row * i_BoardSize + col] = new CheckersSoldier(CheckersSoldier.eSoldierType.BlackPawn, false);
                    }
                    else
                    {
                        r_CheckersBoard[row * i_BoardSize + col] = new CheckersSoldier(CheckersSoldier.eSoldierType.None, false);
                    }
                }
            }

            int heightOfCheckersSoldier = r_CheckersBoard[0].Height;
            int widthOfCheckersSoldier = r_CheckersBoard[0].Width;

            for (int row = 0; row < i_BoardSize; row++)
            {
                topCheckersBoard = topCheckersBoard + row * heightOfCheckersSoldier;
                for (int col = 0; col < i_BoardSize; col++)
                {
                    r_CheckersBoard[row * i_BoardSize + col].Top = topCheckersBoard;
                }
            }
        }
    }
}
