using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05_OtheloLogic
{
    public class BoardPoint
    {
        private int m_LineIndex, m_ColumnIndex;

        public BoardPoint()
        {
            m_LineIndex = 0;
            m_ColumnIndex = 0;
        }

        public int LineIndex
        {
            get { return m_LineIndex; }
            set { m_LineIndex = value; }
        }

        public int ColumnIndex
        {
            get { return m_ColumnIndex; }
            set { m_ColumnIndex = value; }
        }
    }
}
