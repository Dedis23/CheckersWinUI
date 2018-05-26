using System;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersWinUI
{
    public class GameForm : Form
    {
        private readonly LogicUnit r_LogicUnit;

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
        }

        private void createBoard()
        {
            r_LogicUnit.CreateBoard();
        }
    }
}
