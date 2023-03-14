using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05_OtheloLogic
{
    public class GameBoard
    {
        private const int k_6X6 = 6;
        private const int k_8X8 = 8;

        private char[,] m_Board;
        private int m_SizeOfBoard;
        private int m_LogicSizeOfCoins;

        public GameBoard(int i_SizeOfBoard)
        {
            m_SizeOfBoard = i_SizeOfBoard;
            buildBoard();
        }

        public int SizeOfBoard
        {
            get { return m_SizeOfBoard; }
            set
            {
                m_SizeOfBoard = value;
                buildBoard();
            }         
        }

        public int LogicSizeOfCoins
        {
            get { return m_LogicSizeOfCoins; }
            set { m_LogicSizeOfCoins = value; }
        }

        public char[,] Board
        {
            get { return m_Board; }
        }

        private void buildBoard()
        {
            m_Board = new char[m_SizeOfBoard, m_SizeOfBoard];

            int fixedIndex1 = (m_SizeOfBoard / 2) - 1, fixedIndex2 = m_SizeOfBoard / 2;

            m_Board[fixedIndex1, fixedIndex1] = 'O';
            m_Board[fixedIndex2, fixedIndex2] = 'O';
            m_Board[fixedIndex1, fixedIndex2] = 'X';
            m_Board[fixedIndex2, fixedIndex1] = 'X';
        }

        public bool IsEmptyCell(int i_NumOfLine, int i_NumOfColumn)
        {
            bool v_isEmpty = true;

            if (m_Board[i_NumOfLine, i_NumOfColumn] != '\0')
            {
                v_isEmpty = false;
            }

            return v_isEmpty;
        }

        public void InsertToCell(int i_IndexOfLine, int i_IndexOfColumn, char i_CharToInsert)
        {
            m_Board[i_IndexOfLine, i_IndexOfColumn] = i_CharToInsert;
        }

        public char GetCurrentCellChar(int i_Line, int i_Column)
        {
            return m_Board[i_Line, i_Column];
        }  
    }
}
