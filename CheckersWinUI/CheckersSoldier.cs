using System.Windows.Forms;
using System.Drawing;

namespace CheckersWinUI
{
    public class CheckersSoldier: PictureBox
    {
        public enum eSoldierType
        {
            BlackPawn,
            BlackKing,
            WhitePawn,
            WhiteKing,
            None,
        }

        private readonly Timer r_Timer;

        public CheckersSoldier(eSoldierType i_SoldierType, bool i_IsActive)
        {
            this.Width = 32;
            this.Height = 32;
            if (i_IsActive == true)
            {
                setCorrectImage(i_SoldierType);
            }
            else
            {
                this.BackColor = Color.Black;
                this.Enabled = false;
            }
        }

        private void setCorrectImage(eSoldierType i_SoldierType)
        {
            switch (i_SoldierType)
            {
                case eSoldierType.BlackPawn:
                    this.Image = Properties.Resources.Black_Pawn;
                    break;
                case eSoldierType.BlackKing:
                    this.Image = Properties.Resources.Black_King;
                    break;
                case eSoldierType.WhitePawn:
                    this.Image = Properties.Resources.White_Pawn;
                    break;
                case eSoldierType.WhiteKing:
                    this.Image = Properties.Resources.White_King;
                    break;
                default:
                    break;
            }
        }
    }
}
