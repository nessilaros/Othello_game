using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ex05_OtheloLogic;

namespace Ex05_OtheloUI
{
    public enum eMenuButtonPressed
    {
        AgainstFriend,
        AgainstComputer
    }

    public partial class GamePlayForm : Form
    {
        private const int k_6X6 = 6;
        private const int k_8X8 = 8;
        private const int k_10x10 = 10;
        private const int k_12X12 = 12;

        private const int k_Player1 = 1;
        private const int k_Player2 = 2;
        private const int k_Draw = 3;

        private Button[,] m_ButtonsBoard = null;
        private eMenuButtonPressed m_MenuButtonPressed;
        private GameFunctions gameFunction;
        private bool m_Player1Turn = true, m_AgainstComputer = true, m_PossibleMoveByCheckPlayer1 = true,
            m_PossibleMoveByPlayer1 = true, m_PossibleMoveByPlayer2 = true, m_PossibleMoveByCheckPlayer2 = true;

        private int m_SizeOfBoard = 0, m_Player1Score = 0, m_Player2Score = 0;

        public GamePlayForm(int i_SizeOfBoard, eMenuButtonPressed i_MenuButtonPressed, int i_Player1Score = 0, int i_Player2Score = 0)
        {
            gameFunction = new GameFunctions(i_SizeOfBoard);

            m_SizeOfBoard = i_SizeOfBoard;
            m_MenuButtonPressed = i_MenuButtonPressed;
            m_Player1Score = i_Player1Score;
            m_Player2Score = i_Player2Score;

            if (i_MenuButtonPressed == eMenuButtonPressed.AgainstFriend)
            {
                m_AgainstComputer = false;
            }
            else
            {
                m_AgainstComputer = true;
            }

            InitializeComponent();
        }

        private void GamePlay_Load(object sender, EventArgs e)
        {
            buildBoard();

            CheckAndUpdateAvailableMovesOnBoard();
        }

        private void reMatch()
        {
            this.Hide();

            GamePlayForm gamePlayForm = new GamePlayForm(m_SizeOfBoard, m_MenuButtonPressed, m_Player1Score, m_Player2Score);

            gamePlayForm.ShowDialog();
        }

        private void buildBoard()
        {
            m_ButtonsBoard = new Button[m_SizeOfBoard, m_SizeOfBoard];

            int buttonXLocation = -30, buttonYLocation = 5;

            for (int indexOfLine = 0; indexOfLine < m_SizeOfBoard; indexOfLine++)
            {
                for (int indexOfColumn = 0; indexOfColumn < m_SizeOfBoard; indexOfColumn++)
                {
                    m_ButtonsBoard[indexOfLine, indexOfColumn] = new Button();
                    m_ButtonsBoard[indexOfLine, indexOfColumn].Size = new Size(30, 30);

                    if (indexOfColumn == m_SizeOfBoard - 1)
                    {
                        buttonXLocation += 35;
                        m_ButtonsBoard[indexOfLine, indexOfColumn].Location = new System.Drawing.Point(buttonXLocation, buttonYLocation);

                        buttonXLocation = -30;
                        buttonYLocation += 35;
                    }
                    else
                    {
                        buttonXLocation += 35;
                        m_ButtonsBoard[indexOfLine, indexOfColumn].Location = new System.Drawing.Point(buttonXLocation, buttonYLocation);
                    }

                    m_ButtonsBoard[indexOfLine, indexOfColumn].Name = string.Format("{0},{1}", indexOfLine, indexOfColumn);
                    m_ButtonsBoard[indexOfLine, indexOfColumn].Click += DoWhenButtonClicked;

                    this.Controls.Add(m_ButtonsBoard[indexOfLine, indexOfColumn]);
                }
            }

            int fixedIndex1 = (m_SizeOfBoard / 2) - 1, fixedIndex2 = m_SizeOfBoard / 2;

            m_ButtonsBoard[fixedIndex1, fixedIndex1].BackColor = Color.White;
            m_ButtonsBoard[fixedIndex1, fixedIndex1].Text = "O";

            m_ButtonsBoard[fixedIndex2, fixedIndex2].BackColor = Color.White;
            m_ButtonsBoard[fixedIndex2, fixedIndex2].Text = "O";

            m_ButtonsBoard[fixedIndex1, fixedIndex2].BackColor = Color.Black;
            m_ButtonsBoard[fixedIndex1, fixedIndex2].Text = "O";
            m_ButtonsBoard[fixedIndex1, fixedIndex2].ForeColor = Color.White;

            m_ButtonsBoard[fixedIndex2, fixedIndex1].BackColor = Color.Black;
            m_ButtonsBoard[fixedIndex2, fixedIndex1].Text = "O";
            m_ButtonsBoard[fixedIndex2, fixedIndex1].ForeColor = Color.White;
        }

        private void DoWhenButtonClicked(object sender, EventArgs e)
        {
            Button buttonPressed = sender as Button; ////Indexes import

            int fixedLineIndexCheck = 0, fixedColumnIndexCheck = 0;

            fixIndexes(ref fixedLineIndexCheck, ref fixedColumnIndexCheck, buttonPressed.Name);

            if (m_ButtonsBoard[fixedLineIndexCheck, fixedColumnIndexCheck].BackColor != Color.Green && m_ButtonsBoard[fixedLineIndexCheck, fixedColumnIndexCheck].BackColor != SystemColors.Control)
            {
                MessageBox.Show("Invalid choose,please try again.");
                return;
            }

            if (m_Player1Turn && m_AgainstComputer) 
            {
                ////Player1 and then computer

                this.Text = "White's Turn";

                if (m_PossibleMoveByCheckPlayer1)
                {
                    int fixedLineIndex = 0, fixedColumnIndex = 0;

                    fixIndexes(ref fixedLineIndex, ref fixedColumnIndex, buttonPressed.Name);

                    gameFunction.CheckAndUpdateCellToFill(fixedLineIndex, fixedColumnIndex, k_Player1, ref m_Player1Turn, true, ref m_PossibleMoveByPlayer1);

                    updateResultOnButtonsBoard(gameFunction.GetPlayBoard());

                    this.Refresh();
                }
                else
                {
                    MessageBox.Show("Not available move,moving to next turn.");
                }

                m_Player1Turn = false;

                gameFunction.CheckAvailableMove(ref m_PossibleMoveByCheckPlayer2, k_Player2, ref m_Player1Turn, true);

                List<BoardPoint> legalMovesList = gameFunction.LegalMovesComputer;

                if (m_PossibleMoveByCheckPlayer2)
                {
                    Random chooseMoveByComputer = new Random();
                    int choosedIndexByComputer = chooseMoveByComputer.Next(legalMovesList.Count);

                    int fixedLine = legalMovesList[choosedIndexByComputer].LineIndex;
                    int fixedColumn = legalMovesList[choosedIndexByComputer].ColumnIndex;

                    gameFunction.CheckAndUpdateCellToFill(fixedLine, fixedColumn, k_Player2, ref m_Player1Turn, true, ref m_PossibleMoveByPlayer2);

                    updateResultOnButtonsBoard(gameFunction.GetPlayBoard());
                    this.Refresh();
                }
                else
                {
                    MessageBox.Show("Not available move,moving to next turn.");
                }

                m_Player1Turn = true;
                CheckAndUpdateAvailableMovesOnBoard();
            }
            else if (m_Player1Turn && !m_AgainstComputer)
            {
                ////Player1

                this.Text = "Black's Turn";

                if (m_PossibleMoveByCheckPlayer1)
                {
                    int fixedLineIndex = 0, fixedColumnIndex = 0;

                    fixIndexes(ref fixedLineIndex, ref fixedColumnIndex, buttonPressed.Name);

                    gameFunction.CheckAndUpdateCellToFill(fixedLineIndex, fixedColumnIndex, k_Player1, ref m_Player1Turn, true, ref m_PossibleMoveByPlayer1);

                    updateResultOnButtonsBoard(gameFunction.GetPlayBoard());
                }
                else
                {
                    MessageBox.Show("Not available move,moving to next turn.");
                }

                m_Player1Turn = false;

                CheckAndUpdateAvailableMovesOnBoard();
            }
            else if (!m_Player1Turn)
            {
                ////Player2

                this.Text = "White's Turn";

                if (m_PossibleMoveByCheckPlayer2)
                {
                    int fixedLineIndex = 0, fixedColumnIndex = 0;

                    fixIndexes(ref fixedLineIndex, ref fixedColumnIndex, buttonPressed.Name);

                    gameFunction.CheckAndUpdateCellToFill(fixedLineIndex, fixedColumnIndex, k_Player2, ref m_Player1Turn, true, ref m_PossibleMoveByPlayer2);

                    updateResultOnButtonsBoard(gameFunction.GetPlayBoard());
                }
                else
                {
                    MessageBox.Show("Not available move,moving to next turn.");
                }

                m_Player1Turn = true;

                CheckAndUpdateAvailableMovesOnBoard();
            }
            
            gameFunction.CheckAvailableMove(ref m_PossibleMoveByCheckPlayer1, k_Player1, ref m_Player1Turn, true);
            gameFunction.CheckAvailableMove(ref m_PossibleMoveByCheckPlayer2, k_Player2, ref m_Player1Turn, true);

            if (!m_PossibleMoveByCheckPlayer1 && !m_PossibleMoveByCheckPlayer2)
            {
                int whiteCoins = 0, blackCoins = 0;

                int WhoWins = gameFunction.CheckWhoIsTheWinner(ref whiteCoins, ref blackCoins);

                if (WhoWins == k_Player1)
                {
                    m_Player1Score++;

                    if (MessageBox.Show(string.Format("White Won!! ({1}/{2}) ({3}/{4}){0}Do you want to rematch?", Environment.NewLine, whiteCoins, blackCoins, m_Player1Score, m_Player2Score), "Othello", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        reMatch();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else if (WhoWins == k_Player2)
                {
                    m_Player2Score++;

                    if (MessageBox.Show(string.Format("Black Won!! ({1}/{2}) ({3}/{4}){0}Do you want to rematch?", Environment.NewLine, blackCoins, whiteCoins, m_Player2Score, m_Player1Score), "Othello", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        reMatch();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else if (WhoWins == k_Draw) 
                {
                    if (MessageBox.Show(string.Format("This is a draw!!{0}Do you want to rematch?", Environment.NewLine), "Othello", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                    {
                        reMatch();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }

        private void DrawAvailableMovesToBoard(List<BoardPoint> legalMovesList)
        {
            foreach (BoardPoint boardPoint in legalMovesList)
            {
                if (boardPoint != null)
                {
                    int indexOfLine = boardPoint.LineIndex;
                    int indexOfColumn = boardPoint.ColumnIndex;

                    m_ButtonsBoard[indexOfLine, indexOfColumn].BackColor = Color.Green;
                    m_ButtonsBoard[indexOfLine, indexOfColumn].Enabled = true;
                }
            }

            for (int indexOfLine = 0; indexOfLine < m_SizeOfBoard; indexOfLine++)
            {
                for (int indexOfColumn = 0; indexOfColumn < m_SizeOfBoard; indexOfColumn++)
                {
                    if (m_ButtonsBoard[indexOfLine, indexOfColumn].BackColor == Control.DefaultBackColor)
                    {
                        m_ButtonsBoard[indexOfLine, indexOfColumn].Enabled = false;
                    }
                }
            }
        }

        private void CleanAvailableMovesOnBoard()
        {
            for (int indexOfLine = 0; indexOfLine < m_SizeOfBoard; indexOfLine++)
            {
                for (int indexOfColumn = 0; indexOfColumn < m_SizeOfBoard; indexOfColumn++)
                {
                    m_ButtonsBoard[indexOfLine, indexOfColumn].Enabled = true;

                    if (m_ButtonsBoard[indexOfLine, indexOfColumn].BackColor == Color.Green || m_ButtonsBoard[indexOfLine, indexOfColumn].BackColor == SystemColors.Control)
                    {
                        m_ButtonsBoard[indexOfLine, indexOfColumn].BackColor = Control.DefaultBackColor;
                    }
                }
            }
        }
    
        public Button[,] ButtonsBoard
        {
            get { return m_ButtonsBoard; }
        }

        private void updateResultOnButtonsBoard(char[,] i_CharPlayBoard)
        {
            for (int indexOfLine = 0; indexOfLine < m_SizeOfBoard; indexOfLine++)
            {
                for (int indexOfColumn = 0; indexOfColumn < m_SizeOfBoard; indexOfColumn++)
                {
                    if (i_CharPlayBoard[indexOfLine, indexOfColumn] == 'O') 
                    {
                        m_ButtonsBoard[indexOfLine, indexOfColumn].BackColor = Color.White;
                        m_ButtonsBoard[indexOfLine, indexOfColumn].Text = "O";
                        m_ButtonsBoard[indexOfLine, indexOfColumn].ForeColor = Color.Black;
                    }
                    else if(i_CharPlayBoard[indexOfLine, indexOfColumn] == 'X')
                    {
                        m_ButtonsBoard[indexOfLine, indexOfColumn].BackColor = Color.Black;
                        m_ButtonsBoard[indexOfLine, indexOfColumn].Text = "O";
                        m_ButtonsBoard[indexOfLine, indexOfColumn].ForeColor = Color.White;
                    }
                }
            }
        }

        private void CheckAndUpdateAvailableMovesOnBoard()
        {
            if (m_Player1Turn)
            {
                gameFunction.CheckAvailableMove(ref m_PossibleMoveByCheckPlayer1, k_Player1, ref m_Player1Turn, true);
            }
            else
            {
                gameFunction.CheckAvailableMove(ref m_PossibleMoveByCheckPlayer2, k_Player2, ref m_Player1Turn, true);
            }

            List<BoardPoint> legalMovesList = gameFunction.LegalMovesComputer;

            CleanAvailableMovesOnBoard();
            DrawAvailableMovesToBoard(legalMovesList);
        }

        private void fixIndexes(ref int io_IndexOfLine, ref int io_IndexOfColumn, string i_ButtonName)
        {
            StringBuilder lineIndex = new StringBuilder(), columnIndex = new StringBuilder();

            int index = 0;

            while (i_ButtonName[index] != ',')
            {
                lineIndex.Append(i_ButtonName[index]);

                index++;
            }

            index++;

            while (index < i_ButtonName.Length)
            {
                columnIndex.Append(i_ButtonName[index]);
                index++;
            }

            io_IndexOfLine = int.Parse(lineIndex.ToString());
            io_IndexOfColumn = int.Parse(columnIndex.ToString());
        }
    }
}