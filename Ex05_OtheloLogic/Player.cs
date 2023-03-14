using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05_OtheloLogic
{
    public class Player
    {
        private string m_Name;
        private int m_NumOfCoins;
        private bool m_IsWinner = false;
        
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public int NumOfCoins
        {
            get { return m_NumOfCoins; }
            set { m_NumOfCoins = value; }
        }

        public bool IsWinner
        {
            get { return m_IsWinner; }
            set { m_IsWinner = value; }
        }
    }
}
