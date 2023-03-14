using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex05_OtheloUI
{
    public partial class SettingsForm : Form
    {
        private const int k_6X6 = 6;
        private const int k_8X8 = 8;
        private const int k_10x10 = 10;
        private const int k_12X12 = 12;

        private int m_SizeOfBoard = k_6X6;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
        }

        private void buttonBoardSize_Click(object sender, EventArgs e)
        { 
            if (buttonBoardSize.Text == "Board Size : 6x6 (click to increase)")
            {
                buttonBoardSize.Text = "Board Size : 8x8 (click to increase)";

                m_SizeOfBoard = 8;
            }
            else if (buttonBoardSize.Text == "Board Size : 8x8 (click to increase)")
            {
                buttonBoardSize.Text = "Board Size : 10x10 (click to increase)";

                m_SizeOfBoard = 10;
            }
            else if (buttonBoardSize.Text == "Board Size : 10x10 (click to increase)")
            {
                buttonBoardSize.Text = "Board Size : 12x12 (click to increase)";

                m_SizeOfBoard = 12;
            }
            else if (buttonBoardSize.Text == "Board Size : 12x12 (click to increase)")
            {
                buttonBoardSize.Text = "Board Size : 6x6 (click to increase)";

                m_SizeOfBoard = 6;
            }
        }

        private void buttonPlayAgainstComputer_Click(object sender, EventArgs e)
        {
            GamePlayForm gamePlay = new GamePlayForm(m_SizeOfBoard, eMenuButtonPressed.AgainstComputer);

            this.Hide();

            gamePlay.ShowDialog();

            this.Close();
        }

        private void buttonPlayAgainstFriend_Click(object sender, EventArgs e)
        {
            GamePlayForm gamePlay = new GamePlayForm(m_SizeOfBoard, eMenuButtonPressed.AgainstFriend);

            this.Hide();

            gamePlay.ShowDialog();

            this.Close();
        }

        public int SizeOfBoard
        {
            get { return m_SizeOfBoard; }
        }
    }
}
