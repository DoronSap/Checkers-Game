namespace GameLogic
{
    public struct ComputerLocations
    {
        public int m_StartRow;
        public int m_StartCol;
        public int m_FinishRow;
        public int m_FinishCol;

        public ComputerLocations(int i_StartRow, int i_StartCol, int i_FinishRow, int i_FinishCol)
        {
            m_StartRow = i_StartRow;
            m_StartCol = i_StartCol;
            m_FinishRow = i_FinishRow;
            m_FinishCol = i_FinishCol;
        }
    }

}
