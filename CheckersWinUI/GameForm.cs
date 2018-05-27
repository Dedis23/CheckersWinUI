using System;
using System.Windows.Forms;
using CheckersLogic;
using System.Collections.Generic;


namespace CheckersWinUI
{
    public class GameForm : Form
    {
        private const int k_SmallBoardSize = 6;
        private const int k_MediumBoardSize = 8;
        private const int k_BigBoardSize = 10;
        private const int k_LengthIncrease = 15;
        private const int k_WidthIncrease = 10;
        private const int k_InitialLengthIncrease = 200;
        private const int k_InitialWidthIncrease = 100;

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
            r_LogicUnit.CreatePlayerOne(i_Player1Name);
            r_CheckersBoard = new List<CheckersSoldier>(i_BoardSize * i_BoardSize);
            createBoard();
        }

        private void createBoard()
        {
            r_LogicUnit.CreateBoard();   
            switch(r_CheckersBoard.Count)
            {
                case k_SmallBoardSize:
                    BuildCheckersBoard(r_CheckersBoard.Count);
                    break;
                case k_MediumBoardSize:
                    BuildCheckersBoard(r_CheckersBoard.Count);
                    break;
                case k_BigBoardSize:
                    BuildCheckersBoard(r_CheckersBoard.Count);
                    break;
                default:
                    break;
            }
        }

        private void BuildCheckersBoard(int i_BoardSize)
        {
            for(int row = 0; row < i_BoardSize; row++)
            {
                for(int col = 0; col < i_BoardSize; col++)
                {
                    if(row % 2 == 0 && col % 2 == 0 || row % 2 == 1 && col % 2 == 1)
                    {
                        r_CheckersBoard[row * i_BoardSize + col] = new CheckersSoldier(CheckersSoldier.eSoldierType.None, true);
                    }
                    else if(row < i_BoardSize / 2 - 1 && (row % 2 == 0 && col % 2 == 1 || row % 2 == 1 && col % 2 == 0))
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


        }

    }
}
