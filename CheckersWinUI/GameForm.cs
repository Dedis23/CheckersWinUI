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
        private readonly List<CheckersButton> r_CheckersBoard;

        public GameForm(int i_BoardSize, bool i_Player2Enable, string i_Player1Name, string i_Player2Name)
        {
            StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Text = "Damka";
            this.ShowIcon = false;
            r_LogicUnit = new LogicUnit(i_BoardSize);
            createBoard();
            r_LogicUnit.CreatePlayerOne(i_Player1Name);
            r_CheckersBoard = new List<CheckersButton>(i_BoardSize);
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
            for(int i = 0; i < i_BoardSize; i++)
            {
                for(int j = 0; j < i_BoardSize; j++)
                {
                    r_CheckersBoard[i] = new CheckersButton();
                    r_CheckersBoard[i]
                }

            }
        }
    }
}
