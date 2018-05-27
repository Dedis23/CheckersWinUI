using System.Windows.Forms;
using System.Drawing;

namespace CheckersWinUI
{
    public class CheckersSoldier : PictureBox
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
        private eSoldierType m_CheckerSoilderType;
        private int m_RowOfSoilder;
        private int m_ColOfSoilder;


        public CheckersSoldier(eSoldierType i_SoldierType, bool i_IsActive, int i_RowOfSoilder, int i_ColOfSoilder)
        {
            this.Width = 45;
            this.Height = 45;
            this.m_RowOfSoilder = i_RowOfSoilder;
            this.m_ColOfSoilder = i_ColOfSoilder;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.m_CheckerSoilderType = i_SoldierType;
            if (i_IsActive == true)
            {
                setCorrectImage(i_SoldierType);
                this.BackColor = Color.Peru;
                this.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            else
            {
                this.BackColor = Color.Wheat;
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

        public eSoldierType CheckerSoilderType
        {
            get
            {
                return m_CheckerSoilderType;
            }
            set
            {
                m_CheckerSoilderType = value;
            }
        }

        public int RowOfSoilder
        {
            get
            {
                return m_RowOfSoilder;
            }
            set
            {
                m_RowOfSoilder = value;
            }
        }
        public int ColOfSoilder
        {
            get
            {
                return m_ColOfSoilder;
            }
            set
            {
                m_ColOfSoilder = value;
            }
        }

    }
}
