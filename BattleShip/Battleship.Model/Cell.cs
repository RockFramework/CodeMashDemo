namespace Battleship.Model
{
    public class Cell
    {
        private readonly int _row;
        private readonly int _column;

        public Cell(int row, int column)
        {
            _row = row;
            _column = column;
        }

        public int Row
        {
            get { return _row; }
        }

        public int Column
        {
            get { return _column; }
        }
    }
}