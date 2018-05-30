using System.Windows.Forms;
using System.Drawing;

namespace CheckersWinUI
{
    public class BoardSquare : PictureBox
    {
        public enum eBoardSquareType
        {
            BlackPawn,
            BlackKing,
            WhitePawn,
            WhiteKing,
            None,
        }

        private eBoardSquareType m_BoardSquareType;
        private int m_Row;
        private int m_Col;

        public BoardSquare(eBoardSquareType i_BoardSquareType, bool i_IsActive, int i_Row, int i_Col)
        {
            this.Width = 45;
            this.Height = 45;
            this.m_Row = i_Row;
            this.m_Col = i_Col;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.m_BoardSquareType = i_BoardSquareType;
            if (i_IsActive == true)
            {
                SetSquareImage(i_BoardSquareType);
                this.BackColor = Color.Peru;
                this.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            else
            {
                this.BackColor = Color.Wheat;
                this.Enabled = false;
            }
        }

        public void SetSquareImage(eBoardSquareType i_BoardSquareType)
        {
            switch (i_BoardSquareType)
            {
                case eBoardSquareType.BlackPawn:
                    this.Image = Properties.Resources.Black_Pawn;
                    this.m_BoardSquareType = eBoardSquareType.BlackPawn;
                    break;
                case eBoardSquareType.BlackKing:
                    this.Image = Properties.Resources.Black_King;
                    this.m_BoardSquareType = eBoardSquareType.BlackKing;
                    break;
                case eBoardSquareType.WhitePawn:
                    this.Image = Properties.Resources.White_Pawn;
                    this.m_BoardSquareType = eBoardSquareType.WhitePawn;
                    break;
                case eBoardSquareType.WhiteKing:
                    this.Image = Properties.Resources.White_King;
                    this.m_BoardSquareType = eBoardSquareType.WhiteKing;
                    break;
                case eBoardSquareType.None:
                    if (this.Image != null)
                    {
                        this.Image.Dispose();
                        this.Image = null;                    
                    }

                    this.m_BoardSquareType = eBoardSquareType.None;
                    break;
                default:
                    break;
            }
        }

        public void SetActive()
        {
            this.BackColor = Color.FromArgb(0, 191, 255);
        }

        public void SetInActive()
        {
            this.BackColor = Color.Peru;
        }

        public eBoardSquareType BoardSquareType
        {
            get
            {
                return m_BoardSquareType;
            }
        }

        public int Row
        {
            get
            {
                return m_Row;
            }

            set
            {
                m_Row = value;
            }
        }

        public int Col
        {
            get
            {
                return m_Col;
            }

            set
            {
                m_Col = value;
            }
        }
    }
}
