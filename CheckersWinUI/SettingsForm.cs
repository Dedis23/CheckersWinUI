using System;
using System.Drawing;
using System.Windows.Forms;

namespace CheckersWinUI
{
    public class SettingsForm : Form
    {
        private Label labelBoardSize;
        private RadioButton radioButton6x6;
        private RadioButton radioButton8x8;
        private RadioButton radioButton10x10;
        private Label labelPlayers;
        private Label labelPlayer1;
        private TextBox textBoxPlayer1Name;
        private CheckBox checkBoxPlayer2;
        private TextBox textBoxPlayer2Name;
        private Button buttonDone;
        private int m_BoardSize;
        private bool m_IsValidSettings;

        public SettingsForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Height = 280;
            this.Text = "Game Settings";
            this.ShowIcon = false;
            this.m_IsValidSettings = false;
            this.BackColor = Color.NavajoWhite;
            buildInnerForms();
        }

        private void buildInnerForms()
        {
            // BoardSize Label
            labelBoardSize = new Label();
            labelBoardSize.Text = "Board Size:";
            labelBoardSize.Font = new Font("Consolas", 10, FontStyle.Bold | FontStyle.Underline);
            labelBoardSize.ForeColor = Color.Black;
            labelBoardSize.Top = 28;
            labelBoardSize.Left = 20;
            this.Controls.Add(labelBoardSize);

            // RadioButton 10x10
            radioButton10x10 = new RadioButton();
            radioButton10x10.Text = "10 x 10";
            radioButton10x10.Font = new Font("Consolas", 9, FontStyle.Bold);
            radioButton10x10.AutoSize = false;
            radioButton10x10.CheckedChanged += new EventHandler(radioButton10x10_CheckedChanged);
            radioButton10x10.Top = 62;
            radioButton10x10.Left = 195;
            this.Controls.Add(radioButton10x10);

            // RadioButton 8x8
            radioButton8x8 = new RadioButton();
            radioButton8x8.Text = "8 x 8";
            radioButton8x8.Font = new Font("Consolas", 9, FontStyle.Bold);
            radioButton8x8.AutoSize = false;
            radioButton8x8.CheckedChanged += new EventHandler(radioButton8x8_CheckedChanged);
            radioButton8x8.Top = 62;
            radioButton8x8.Left = 116;
            this.Controls.Add(radioButton8x8);

            // RadioButton 6x6
            radioButton6x6 = new RadioButton();
            radioButton6x6.Text = "6 x 6";
            radioButton6x6.Font = new Font("Consolas", 9, FontStyle.Bold);
            radioButton6x6.AutoSize = false;
            radioButton6x6.Checked = true; // this is for making 6x6 the default size
            m_BoardSize = 6;
            radioButton6x6.CheckedChanged += new EventHandler(radioButton6x6_CheckedChanged);
            radioButton6x6.Top = 62;
            radioButton6x6.Left = 35;
            this.Controls.Add(radioButton6x6);

            // Players Label
            labelPlayers = new Label();
            labelPlayers.Text = "Players:";
            labelPlayers.Font = new Font("Consolas", 10, FontStyle.Bold | FontStyle.Underline);
            labelPlayers.ForeColor = Color.Black;
            labelPlayers.Top = 95;
            labelPlayers.Left = 20;
            this.Controls.Add(labelPlayers);

            // Player1 Label
            labelPlayer1 = new Label();
            labelPlayer1.Text = "Player 1:";
            labelPlayer1.Font = new Font("Consolas", 9, FontStyle.Bold);
            labelPlayer1.ForeColor = Color.Black;
            labelPlayer1.Top = 130;
            labelPlayer1.Left = 40;
            this.Controls.Add(labelPlayer1);

            // Player1 Name TextBox
            textBoxPlayer1Name = new TextBox();
            textBoxPlayer1Name.Width = 130;
            textBoxPlayer1Name.Top = 130;
            textBoxPlayer1Name.Left = 140;
            textBoxPlayer1Name.MaxLength = 8;
            this.Controls.Add(textBoxPlayer1Name);

            // Player2 CheckBox
            checkBoxPlayer2 = new CheckBox();
            checkBoxPlayer2.Text = "Player 2:";
            checkBoxPlayer2.Font = new Font("Consolas", 9, FontStyle.Bold);
            checkBoxPlayer2.ForeColor = Color.Black;
            checkBoxPlayer2.Top = 160;
            checkBoxPlayer2.Left = 25;
            checkBoxPlayer2.CheckedChanged += new EventHandler(player2CheckBox_CheckedChanged);
            this.Controls.Add(checkBoxPlayer2);

            // Player2 Name TextBox
            textBoxPlayer2Name = new TextBox();
            textBoxPlayer2Name.Text = "[Computer]";
            textBoxPlayer2Name.Enabled = false;
            textBoxPlayer2Name.Width = 130;
            textBoxPlayer2Name.Top = 160;
            textBoxPlayer2Name.Left = 140;
            textBoxPlayer2Name.MaxLength = 8;
            this.Controls.Add(textBoxPlayer2Name);

            // Done Button
            buttonDone = new Button();
            buttonDone.Text = "Done";
            buttonDone.Font = new Font("Arial", 9, FontStyle.Bold);
            buttonDone.ForeColor = Color.Black;
            buttonDone.Top = 200;
            buttonDone.Left = 175;
            buttonDone.TextAlign = ContentAlignment.MiddleCenter;
            buttonDone.BackColor = Color.Transparent;
            buttonDone.FlatStyle = FlatStyle.Flat;
            buttonDone.Click += new EventHandler(doneButton_Clicked);
            this.Controls.Add(buttonDone);
        }

        private void radioButton10x10_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton10x10 = sender as RadioButton;
            if (radioButton10x10.Checked == true)
            {
                m_BoardSize = 10;
            }
        }

        private void radioButton8x8_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton8x8 = sender as RadioButton;
            if (radioButton8x8.Checked == true)
            {
                m_BoardSize = 8;
            }
        }

        private void radioButton6x6_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton6x6 = sender as RadioButton;
            if (radioButton6x6.Checked == true)
            {
                m_BoardSize = 6;
            }
        }

        private void doneButton_Clicked(object sender, EventArgs e)
        {
            m_IsValidSettings = checkIfValidSettings();
            if (m_IsValidSettings == true)
            {
                this.Hide();
            }
        }

        public bool ValidSettings
        {
            get
            {
                return m_IsValidSettings;
            }
        }

        private bool checkIfValidSettings()
        {
            bool validSettings = false;
            if (textBoxPlayer2Name.Text == string.Empty || textBoxPlayer1Name.Text == string.Empty)
            {
                MessageBox.Show("Please enter name for each player.", this.Text);
            }
            else
            {
                validSettings = true;
            }

            return validSettings;
        }

        private void player2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox player2CheckBox = sender as CheckBox;
            if (player2CheckBox.Checked == false)
            {
                textBoxPlayer2Name.Text = "[Computer]";
                textBoxPlayer2Name.Enabled = false;
            }
            else
            {
                textBoxPlayer2Name.Text = string.Empty;
                textBoxPlayer2Name.Enabled = true;
            }
        }

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
        }

        public string Player1Name
        {
            get
            {
                return textBoxPlayer1Name.Text;
            }
        }

        public bool IsHumanPlayer2
        {
            get
            {
                return checkBoxPlayer2.Checked;
            }
        }

        public string Player2Name
        {
            get
            {
                return textBoxPlayer2Name.Text;
            }
        }
    }
}
