using System;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersWinUI
{
    public class CheckersButton: Button
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
        private PictureBox m_Picture;

        public CheckersButton(eSoldierType i_SoldierType, bool is_Active)
        {
            
        }
    }
}
