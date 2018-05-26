using System;
using System.Drawing;
using System.Windows.Forms;

namespace CheckersWinUI
{
    public class SettingsForm : Form
    {
        private Label m_BoardSizeLabel;
        private RadioButton m_RadioButton6x6;
        private RadioButton m_RadioButton8x8;
        private RadioButton m_RadioButton10x10;
        private Label m_PlayersLabel;
        private Label m_Player1Label;
        private TextBox m_Player1NameTextBox;
        private CheckBox m_Player2CheckBox;
        private TextBox m_Player2NameTextBox;
        private Button m_DoneButton;
        private int m_BoardSize;

        public SettingsForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Height = 280;
            this.Text = "Game Settings";
            this.ShowIcon = false;
            buildInnerForms();
        }

        private void buildInnerForms()
        {
            // BoardSize Label
            m_BoardSizeLabel = new Label();
            m_BoardSizeLabel.Text = "Board Size:";
            m_BoardSizeLabel.Font = new Font("Arial", 10, FontStyle.Bold | FontStyle.Underline);
            m_BoardSizeLabel.ForeColor = Color.Black;
            m_BoardSizeLabel.Top = 28;
            m_BoardSizeLabel.Left = 20;
            this.Controls.Add(m_BoardSizeLabel);

            // RadioButton 10x10
            m_RadioButton10x10 = new RadioButton();
            m_RadioButton10x10.Text = "10 x 10";
            m_RadioButton10x10.Font = new Font("Arial", 9, FontStyle.Bold);
            m_RadioButton10x10.AutoSize = false;
            m_RadioButton10x10.CheckedChanged += new EventHandler(radioButton10x10_CheckedChanged);
            m_RadioButton10x10.Top = 62;
            m_RadioButton10x10.Left = 195;
            this.Controls.Add(m_RadioButton10x10);

            // RadioButton 8x8
            m_RadioButton8x8 = new RadioButton();
            m_RadioButton8x8.Text = "8 x 8";
            m_RadioButton8x8.Font = new Font("Arial", 9, FontStyle.Bold);
            m_RadioButton8x8.AutoSize = false;
            m_RadioButton8x8.CheckedChanged += new EventHandler(radioButton8x8_CheckedChanged);
            m_RadioButton8x8.Top = 62;
            m_RadioButton8x8.Left = 116;
            this.Controls.Add(m_RadioButton8x8);

            // RadioButton 6x6
            m_RadioButton6x6 = new RadioButton();
            m_RadioButton6x6.Text = "6 x 6";
            m_RadioButton6x6.Font = new Font("Arial", 9, FontStyle.Bold);
            m_RadioButton6x6.AutoSize = false;
            m_RadioButton6x6.Checked = true; // this is for making 6x6 the default size
            m_BoardSize = 6;
            m_RadioButton6x6.CheckedChanged += new EventHandler(radioButton6x6_CheckedChanged);
            m_RadioButton6x6.Top = 62;
            m_RadioButton6x6.Left = 35;
            this.Controls.Add(m_RadioButton6x6);

            // Players Label
            m_PlayersLabel = new Label();
            m_PlayersLabel.Text = "Players:";
            m_PlayersLabel.Font = new Font("Arial", 10, FontStyle.Bold | FontStyle.Underline);
            m_PlayersLabel.ForeColor = Color.Black;
            m_PlayersLabel.Top = 95;
            m_PlayersLabel.Left = 20;
            this.Controls.Add(m_PlayersLabel);

            // Player1 Label
            m_Player1Label = new Label();
            m_Player1Label.Text = "Player 1:";
            m_Player1Label.Font = new Font("Arial", 9, FontStyle.Bold);
            m_Player1Label.ForeColor = Color.Black;
            m_Player1Label.Top = 130;
            m_Player1Label.Left = 40;
            this.Controls.Add(m_Player1Label);

            // Player1 Name TextBox
            m_Player1NameTextBox = new TextBox();
            m_Player1NameTextBox.Width = 130;
            m_Player1NameTextBox.Top = 130;
            m_Player1NameTextBox.Left = 140;
            this.Controls.Add(m_Player1NameTextBox);

            // Player2 CheckBox
            m_Player2CheckBox = new CheckBox();
            m_Player2CheckBox.Text = "Player 2:";
            m_Player2CheckBox.Font = new Font("Arial", 9, FontStyle.Bold);
            m_Player2CheckBox.ForeColor = Color.Black;
            m_Player2CheckBox.Top = 160;
            m_Player2CheckBox.Left = 25;
            m_Player2CheckBox.CheckedChanged += new EventHandler(player2CheckBox_CheckedChanged);
            this.Controls.Add(m_Player2CheckBox);

            // Player1 Name TextBox
            m_Player2NameTextBox = new TextBox();
            m_Player2NameTextBox.Text = "[Computer]";
            m_Player2NameTextBox.Enabled = false;
            m_Player2NameTextBox.Width = 130;
            m_Player2NameTextBox.Top = 160;
            m_Player2NameTextBox.Left = 140;
            this.Controls.Add(m_Player2NameTextBox);

            // Done Button
            m_DoneButton = new Button();
            m_DoneButton.Text = "Done";
            m_DoneButton.Font = new Font("Arial", 9, FontStyle.Bold);
            m_DoneButton.ForeColor = Color.Black;
            m_DoneButton.Top = 200;
            m_DoneButton.Left = 175;
            m_DoneButton.Click += new EventHandler(doneButton_Clicked);
            this.Controls.Add(m_DoneButton);
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
            bool isValidSettings = checkIfValidSettings();
            if (isValidSettings == true)
            {
                this.Hide();
            }
        }

        private bool checkIfValidSettings()
        {
            bool validSettings = false;
            if (m_Player2NameTextBox.Text == string.Empty || m_Player1NameTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please enter name for each player.", "Error");
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
                m_Player2NameTextBox.Text = "[Computer]";
                m_Player2NameTextBox.Enabled = false;
            }
            else
            {
                m_Player2NameTextBox.Text = string.Empty;
                m_Player2NameTextBox.Enabled = true;
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
                return m_Player1NameTextBox.Text;
            }
        }

        public bool IsHumanPlayer2
        {
            get
            {
                return m_Player2CheckBox.Checked;
            }
        }

        public string Player2Name
        {
            get
            {
                return m_Player2NameTextBox.Text;
            }
        }
    }
}
