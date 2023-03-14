using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05_OtheloLogic
{
    public class GameFunctions
    {
        private const int k_Player1 = 1;
        private const int k_Player2 = 2;
        private const int k_Draw = 3;
        private const int k_6X6 = 6;
        private const int k_8X8 = 8;

        private GameBoard gameBoard;
        private Player m_Player1, m_Player2;

        private int m_SizeOfBoard = 0;
        private List<BoardPoint> m_LegalMovesComputer;

        public GameFunctions(int i_SizeOfBoard)
        {
            gameBoard = new GameBoard(i_SizeOfBoard);
            m_Player1 = new Player();
            m_Player2 = new Player();

            m_SizeOfBoard = i_SizeOfBoard;
            m_Player1.Name = "Nessi";
            m_Player2.Name = "Sapir";
            m_LegalMovesComputer = new List<BoardPoint>();
        }

        public int CheckWhoIsTheWinner(ref int io_WhiteCoins, ref int io_BlackCoins)
        {
            int countO = 0, countX = 0;

            for (int indexOfLine = 0; indexOfLine < m_SizeOfBoard; indexOfLine++)
            {
                for (int indexOfColumn = 0; indexOfColumn < m_SizeOfBoard; indexOfColumn++)
                {
                    char currentChar = gameBoard.GetCurrentCellChar(indexOfLine, indexOfColumn);

                    if (currentChar == 'O')
                    {
                        countO++;
                    }
                    else if (currentChar == 'X')
                    {
                        countX++;
                    }
                }
            }

            io_WhiteCoins = countO;
            io_BlackCoins = countX;

            if (countO > countX)
            {
                return k_Player1;
            }
            else if (countX > countO)
            {
                return k_Player2;
            }
            else
            {
                return k_Draw;
            }
        }

        public void CheckAvailableMove(ref bool io_LegalMove, int i_NumOfPlayer, ref bool io_Player1Turn, bool i_ToSaveMoves)
        {
            bool v_ToChangeBoard = false;
            bool v_PossibleMoveByComputer = false;
            m_LegalMovesComputer = new List<BoardPoint>();

            if (io_LegalMove)
            {
                io_LegalMove = false;
            }

            if (!i_ToSaveMoves)
            {
                for (int indexOfLine = 0; indexOfLine < m_SizeOfBoard; indexOfLine++)
                {
                    for (int indexOfColumn = 0; indexOfColumn < m_SizeOfBoard && !io_LegalMove; indexOfColumn++)
                    {
                        if (gameBoard.IsEmptyCell(indexOfLine, indexOfColumn))
                        {
                            CheckAndUpdateCellToFill(indexOfLine, indexOfColumn, i_NumOfPlayer, ref io_Player1Turn, v_ToChangeBoard, ref io_LegalMove);
                        }
                    }
                }
            }
            else
            {
                for (int indexOfLine = 0; indexOfLine < m_SizeOfBoard; indexOfLine++)
                {
                    for (int indexOfColumn = 0; indexOfColumn < m_SizeOfBoard; indexOfColumn++)
                    {
                        if (gameBoard.IsEmptyCell(indexOfLine, indexOfColumn))
                        {
                            CheckAndUpdateCellToFill(indexOfLine, indexOfColumn, i_NumOfPlayer, ref io_Player1Turn, v_ToChangeBoard, ref io_LegalMove);

                            if (io_LegalMove)
                            {
                                v_PossibleMoveByComputer = true;

                                BoardPoint tempPoint = new BoardPoint();

                                tempPoint.LineIndex = indexOfLine;
                                tempPoint.ColumnIndex = indexOfColumn;

                                m_LegalMovesComputer.Add(tempPoint);
                            }
                        }
                    }
                }

                io_LegalMove = v_PossibleMoveByComputer;
            }
        }

        public void CheckAndUpdateCellToFill(int i_LineIndex, int i_ColumnIndex, int i_NumOfPlayer, ref bool io_Player1Turn, bool i_ToChangeBoard, ref bool io_IsLegalMove)
        {
            char coin, oppositeCoin;
            bool v_CoinBack = false, v_LegalMove = false;
            int currentLine = i_LineIndex, currentColumn = i_ColumnIndex, countOppositeCoin = 0;

            if (i_NumOfPlayer == k_Player1)
            {
                coin = 'O';
                oppositeCoin = 'X';

                char CurrentCharInBoard = gameBoard.GetCurrentCellChar(currentLine, currentColumn);

                CheckAndUpdateCellToFillRightRec(currentLine, currentColumn + 1, ref v_CoinBack, coin, oppositeCoin, ref countOppositeCoin, i_ToChangeBoard);

                ChecksAndEnterToCell(currentLine, currentColumn, ref v_CoinBack, ref countOppositeCoin, ref v_LegalMove, coin, i_ToChangeBoard);

                CheckAndUpdateCellToFillLeftRec(currentLine, currentColumn - 1, ref v_CoinBack, coin, oppositeCoin, ref countOppositeCoin, i_ToChangeBoard);

                ChecksAndEnterToCell(currentLine, currentColumn, ref v_CoinBack, ref countOppositeCoin, ref v_LegalMove, coin, i_ToChangeBoard);

                CheckAndUpdateCellToFillRecDownRightRec(currentLine + 1, currentColumn + 1, ref v_CoinBack, coin, oppositeCoin, ref countOppositeCoin, i_ToChangeBoard);

                ChecksAndEnterToCell(currentLine, currentColumn, ref v_CoinBack, ref countOppositeCoin, ref v_LegalMove, coin, i_ToChangeBoard);

                CheckAndUpdateCellToFillRecDownleftRec(currentLine + 1, currentColumn - 1, ref v_CoinBack, coin, oppositeCoin, ref countOppositeCoin, i_ToChangeBoard);

                ChecksAndEnterToCell(currentLine, currentColumn, ref v_CoinBack, ref countOppositeCoin, ref v_LegalMove, coin, i_ToChangeBoard);

                CheckAndUpdateCellToFillUpRightRec(currentLine - 1, currentColumn + 1, ref v_CoinBack, coin, oppositeCoin, ref countOppositeCoin, i_ToChangeBoard);

                ChecksAndEnterToCell(currentLine, currentColumn, ref v_CoinBack, ref countOppositeCoin, ref v_LegalMove, coin, i_ToChangeBoard);

                CheckAndUpdateCellToFillUpLeftRec(currentLine - 1, currentColumn - 1, ref v_CoinBack, coin, oppositeCoin, ref countOppositeCoin, i_ToChangeBoard);

                ChecksAndEnterToCell(currentLine, currentColumn, ref v_CoinBack, ref countOppositeCoin, ref v_LegalMove, coin, i_ToChangeBoard);

                CheckAndUpdateCellToFillUpRec(currentLine - 1, currentColumn, ref v_CoinBack, coin, oppositeCoin, ref countOppositeCoin, i_ToChangeBoard);

                ChecksAndEnterToCell(currentLine, currentColumn, ref v_CoinBack, ref countOppositeCoin, ref v_LegalMove, coin, i_ToChangeBoard);

                CheckAndUpdateCellToFillDownRec(currentLine + 1, currentColumn, ref v_CoinBack, coin, oppositeCoin, ref countOppositeCoin, i_ToChangeBoard);

                ChecksAndEnterToCell(currentLine, currentColumn, ref v_CoinBack, ref countOppositeCoin, ref v_LegalMove, coin, i_ToChangeBoard);

                io_IsLegalMove = v_LegalMove;
            }
            else 
            {
                ////PLAYER2

                coin = 'X';
                oppositeCoin = 'O';

                char CurrentCharInBoard = gameBoard.GetCurrentCellChar(currentLine, currentColumn);

                CheckAndUpdateCellToFillRightRec(currentLine, currentColumn + 1, ref v_CoinBack, coin, oppositeCoin, ref countOppositeCoin, i_ToChangeBoard);

                ChecksAndEnterToCell(currentLine, currentColumn, ref v_CoinBack, ref countOppositeCoin, ref v_LegalMove, coin, i_ToChangeBoard);

                CheckAndUpdateCellToFillLeftRec(currentLine, currentColumn - 1, ref v_CoinBack, coin, oppositeCoin, ref countOppositeCoin, i_ToChangeBoard);

                ChecksAndEnterToCell(currentLine, currentColumn, ref v_CoinBack, ref countOppositeCoin, ref v_LegalMove, coin, i_ToChangeBoard);

                CheckAndUpdateCellToFillRecDownRightRec(currentLine + 1, currentColumn + 1, ref v_CoinBack, coin, oppositeCoin, ref countOppositeCoin, i_ToChangeBoard);

                ChecksAndEnterToCell(currentLine, currentColumn, ref v_CoinBack, ref countOppositeCoin, ref v_LegalMove, coin, i_ToChangeBoard);

                CheckAndUpdateCellToFillRecDownleftRec(currentLine + 1, currentColumn - 1, ref v_CoinBack, coin, oppositeCoin, ref countOppositeCoin, i_ToChangeBoard);

                ChecksAndEnterToCell(currentLine, currentColumn, ref v_CoinBack, ref countOppositeCoin, ref v_LegalMove, coin, i_ToChangeBoard);

                CheckAndUpdateCellToFillUpRightRec(currentLine - 1, currentColumn + 1, ref v_CoinBack, coin, oppositeCoin, ref countOppositeCoin, i_ToChangeBoard);

                ChecksAndEnterToCell(currentLine, currentColumn, ref v_CoinBack, ref countOppositeCoin, ref v_LegalMove, coin, i_ToChangeBoard);

                CheckAndUpdateCellToFillUpLeftRec(currentLine - 1, currentColumn - 1, ref v_CoinBack, coin, oppositeCoin, ref countOppositeCoin, i_ToChangeBoard);

                ChecksAndEnterToCell(currentLine, currentColumn, ref v_CoinBack, ref countOppositeCoin, ref v_LegalMove, coin, i_ToChangeBoard);

                CheckAndUpdateCellToFillUpRec(currentLine - 1, currentColumn, ref v_CoinBack, coin, oppositeCoin, ref countOppositeCoin, i_ToChangeBoard);

                ChecksAndEnterToCell(currentLine, currentColumn, ref v_CoinBack, ref countOppositeCoin, ref v_LegalMove, coin, i_ToChangeBoard);

                CheckAndUpdateCellToFillDownRec(currentLine + 1, currentColumn, ref v_CoinBack, coin, oppositeCoin, ref countOppositeCoin, i_ToChangeBoard);

                ChecksAndEnterToCell(currentLine, currentColumn, ref v_CoinBack, ref countOppositeCoin, ref v_LegalMove, coin, i_ToChangeBoard);

                io_IsLegalMove = v_LegalMove;
            }
        }

        public void ChecksAndEnterToCell(int i_CurrentLine, int i_CurrentColumn, ref bool io_CoinBack, ref int io_CountOppositeCoin, ref bool io_LegalMove, char coinToFill, bool i_ToChangeBoard)
        {
            if (io_CoinBack && io_CountOppositeCoin > 0)
            {
                io_LegalMove = true;

                if (i_ToChangeBoard)
                {
                    gameBoard.InsertToCell(i_CurrentLine, i_CurrentColumn, coinToFill);
                }
            }

            io_CoinBack = false;
            io_CountOppositeCoin = 0;
        }

        public void CheckAndUpdateCellToFillRightRec(int i_LineIndex, int i_ColumnIndex, ref bool io_CoinBack, char i_Coin, char i_OppositeCoin, ref int io_CountOppositeCoin, bool i_ToChangeBoard)
        {
            int currentLine = i_LineIndex, currentColumn = i_ColumnIndex;

            if (currentColumn > (m_SizeOfBoard - 1) || gameBoard.GetCurrentCellChar(currentLine, currentColumn) == '\0')
            {
                return;
            }

            if (gameBoard.GetCurrentCellChar(currentLine, currentColumn) == i_Coin)
            {
                io_CoinBack = true;
                return;
            }

            if (gameBoard.GetCurrentCellChar(currentLine, currentColumn) == i_OppositeCoin)
            {
                io_CountOppositeCoin++;
            }

            CheckAndUpdateCellToFillRightRec(currentLine, currentColumn + 1, ref io_CoinBack, i_Coin, i_OppositeCoin, ref io_CountOppositeCoin, i_ToChangeBoard);

            if (io_CoinBack && io_CountOppositeCoin > 0 && i_ToChangeBoard)
            {
                gameBoard.InsertToCell(currentLine, currentColumn, i_Coin);
            }
        }

        public void CheckAndUpdateCellToFillDownRec(int i_LineIndex, int i_ColumnIndex, ref bool io_CoinBack, char i_Coin, char i_OppositeCoin, ref int io_CountOppositeCoin, bool i_ToChangeBoard)
        {
            int currentLine = i_LineIndex, currentColumn = i_ColumnIndex;

            if (currentLine > (m_SizeOfBoard - 1) || gameBoard.GetCurrentCellChar(currentLine, currentColumn) == '\0')
            {
                return;
            }

            if (gameBoard.GetCurrentCellChar(currentLine, currentColumn) == i_Coin)
            {
                io_CoinBack = true;
                return;
            }

            if (gameBoard.GetCurrentCellChar(currentLine, currentColumn) == i_OppositeCoin)
            {
                io_CountOppositeCoin++;
            }

            CheckAndUpdateCellToFillDownRec(currentLine + 1, currentColumn, ref io_CoinBack, i_Coin, i_OppositeCoin, ref io_CountOppositeCoin, i_ToChangeBoard);

            if (io_CoinBack && io_CountOppositeCoin > 0 && i_ToChangeBoard)
            {
                gameBoard.InsertToCell(currentLine, currentColumn, i_Coin);
            }
        }

        public void CheckAndUpdateCellToFillUpRec(int i_LineIndex, int i_ColumnIndex, ref bool io_CoinBack, char i_Coin, char i_OppositeCoin, ref int io_CountOppositeCoin, bool i_ToChangeBoard)
        {
            int currentLine = i_LineIndex, currentColumn = i_ColumnIndex;

            if (currentLine < 0 || gameBoard.GetCurrentCellChar(currentLine, currentColumn) == '\0')
            {
                return;
            }

            if (gameBoard.GetCurrentCellChar(currentLine, currentColumn) == i_Coin)
            {
                io_CoinBack = true;
                return;
            }

            if (gameBoard.GetCurrentCellChar(currentLine, currentColumn) == i_OppositeCoin)
            {
                io_CountOppositeCoin++;
            }

            CheckAndUpdateCellToFillUpRec(currentLine - 1, currentColumn, ref io_CoinBack, i_Coin, i_OppositeCoin, ref io_CountOppositeCoin, i_ToChangeBoard);

            if (io_CoinBack && io_CountOppositeCoin > 0 && i_ToChangeBoard)
            {
                gameBoard.InsertToCell(currentLine, currentColumn, i_Coin);
            }
        }

        public void CheckAndUpdateCellToFillLeftRec(int i_LineIndex, int i_ColumnIndex, ref bool io_CoinBack, char i_Coin, char i_OppositeCoin, ref int io_CountOppositeCoin, bool i_ToChangeBoard)
        {
            int currentLine = i_LineIndex, currentColumn = i_ColumnIndex;

            if (currentColumn < 0 || gameBoard.GetCurrentCellChar(currentLine, currentColumn) == '\0')
            {
                return;
            }

            if (gameBoard.GetCurrentCellChar(currentLine, currentColumn) == i_Coin)
            {
                io_CoinBack = true;
                return;
            }

            if (gameBoard.GetCurrentCellChar(currentLine, currentColumn) == i_OppositeCoin)
            {
                io_CountOppositeCoin++;
            }

            CheckAndUpdateCellToFillLeftRec(currentLine, currentColumn - 1, ref io_CoinBack, i_Coin, i_OppositeCoin, ref io_CountOppositeCoin, i_ToChangeBoard);

            if (io_CoinBack && io_CountOppositeCoin > 0 && i_ToChangeBoard)
            {
                gameBoard.InsertToCell(currentLine, currentColumn, i_Coin);
            }
        }

        public void CheckAndUpdateCellToFillUpLeftRec(int i_LineIndex, int i_ColumnIndex, ref bool io_CoinBack, char i_Coin, char i_OppositeCoin, ref int io_CountOppositeCoin, bool i_ToChangeBoard)
        {
            int currentLine = i_LineIndex, currentColumn = i_ColumnIndex;

            if (currentColumn < 0 || currentLine < 0 || gameBoard.GetCurrentCellChar(currentLine, currentColumn) == '\0')
            {
                return;
            }

            if (gameBoard.GetCurrentCellChar(currentLine, currentColumn) == i_Coin)
            {
                io_CoinBack = true;
                return;
            }

            if (gameBoard.GetCurrentCellChar(currentLine, currentColumn) == i_OppositeCoin)
            {
                io_CountOppositeCoin++;
            }

            CheckAndUpdateCellToFillUpLeftRec(currentLine - 1, currentColumn - 1, ref io_CoinBack, i_Coin, i_OppositeCoin, ref io_CountOppositeCoin, i_ToChangeBoard);

            if (io_CoinBack && io_CountOppositeCoin > 0 && i_ToChangeBoard)
            {
                gameBoard.InsertToCell(currentLine, currentColumn, i_Coin);
            }
        }

        public void CheckAndUpdateCellToFillRecDownRightRec(int i_LineIndex, int i_ColumnIndex, ref bool io_CoinBack, char i_Coin, char i_OppositeCoin, ref int io_CountOppositeCoin, bool i_ToChangeBoard)
        {
            int currentLine = i_LineIndex, currentColumn = i_ColumnIndex;

            if (currentLine > (m_SizeOfBoard - 1) || currentColumn > (m_SizeOfBoard - 1) || gameBoard.GetCurrentCellChar(currentLine, currentColumn) == '\0')
            {
                return;
            }

            if (gameBoard.GetCurrentCellChar(currentLine, currentColumn) == i_Coin)
            {
                io_CoinBack = true;
                return;
            }

            if (gameBoard.GetCurrentCellChar(currentLine, currentColumn) == i_OppositeCoin)
            {
                io_CountOppositeCoin++;
            }

            CheckAndUpdateCellToFillRecDownRightRec(currentLine + 1, currentColumn + 1, ref io_CoinBack, i_Coin, i_OppositeCoin, ref io_CountOppositeCoin, i_ToChangeBoard);

            if (io_CoinBack && io_CountOppositeCoin > 0 && i_ToChangeBoard)
            {
                gameBoard.InsertToCell(currentLine, currentColumn, i_Coin);
            }
        }

        public void CheckAndUpdateCellToFillRecDownleftRec(int i_LineIndex, int i_ColumnIndex, ref bool io_CoinBack, char i_Coin, char i_OppositeCoin, ref int io_CountOppositeCoin, bool i_ToChangeBoard)
        {
            int currentLine = i_LineIndex, currentColumn = i_ColumnIndex;

            if (currentColumn < 0 || currentLine > (m_SizeOfBoard - 1) || gameBoard.GetCurrentCellChar(currentLine, currentColumn) == '\0')
            {
                return;
            }

            if (gameBoard.GetCurrentCellChar(currentLine, currentColumn) == i_Coin)
            {
                io_CoinBack = true;
                return;
            }

            if (gameBoard.GetCurrentCellChar(currentLine, currentColumn) == i_OppositeCoin)
            {
                io_CountOppositeCoin++;
            }

            CheckAndUpdateCellToFillRecDownleftRec(currentLine + 1, currentColumn - 1, ref io_CoinBack, i_Coin, i_OppositeCoin, ref io_CountOppositeCoin, i_ToChangeBoard);

            if (io_CoinBack && io_CountOppositeCoin > 0 && i_ToChangeBoard)
            {
                gameBoard.InsertToCell(currentLine, currentColumn, i_Coin);
            }
        }

        public void CheckAndUpdateCellToFillUpRightRec(int i_LineIndex, int i_ColumnIndex, ref bool io_CoinBack, char i_Coin, char i_OppositeCoin, ref int io_CountOppositeCoin, bool i_ToChangeBoard)
        {
            int currentLine = i_LineIndex, currentColumn = i_ColumnIndex;

            if (currentColumn > (m_SizeOfBoard - 1) || currentLine < 0 || gameBoard.GetCurrentCellChar(currentLine, currentColumn) == '\0')
            {
                return;
            }

            if (gameBoard.GetCurrentCellChar(currentLine, currentColumn) == i_Coin)
            {
                io_CoinBack = true;
                return;
            }

            if (gameBoard.GetCurrentCellChar(currentLine, currentColumn) == i_OppositeCoin)
            {
                io_CountOppositeCoin++;
            }

            CheckAndUpdateCellToFillUpRightRec(currentLine - 1, currentColumn + 1, ref io_CoinBack, i_Coin, i_OppositeCoin, ref io_CountOppositeCoin, i_ToChangeBoard);

            if (io_CoinBack && io_CountOppositeCoin > 0 && i_ToChangeBoard)
            {
                gameBoard.InsertToCell(currentLine, currentColumn, i_Coin);
            }
        }

        public List<BoardPoint> LegalMovesComputer
        {
            get { return m_LegalMovesComputer; }
        }

        public char[,] GetPlayBoard()
        {
            return gameBoard.Board;
        }
    }
}
