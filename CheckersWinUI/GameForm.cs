using System.Windows.Forms;
using CheckersLogic;

namespace CheckersWinUI
{
    public class GameForm : Form
    {
        private readonly LogicUnit r_LogicUnit;

        public GameForm(int i_BoardSize, bool i_Player2Enable, string i_Player1Name, string i_Player2Name)
        {
            r_LogicUnit = new LogicUnit(i_BoardSize);
        }
    }
}
