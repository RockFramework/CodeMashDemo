//namespace Battleship.Model
//{
//    public class GameHistory
//    {
//        public const int BOARDWIDTH = 10;
//        public const int BOARDHEIGHT = 10;

//        private readonly CellState[,] _cells = new CellState[BOARDHEIGHT, BOARDWIDTH];

//        public CellState CellAt(int x, int y)
//        {
//            return _cells[y, x];
//        }

//        public void Clear()
//        {
//            for (int y = 0; y < BOARDHEIGHT; y++)
//            {
//                for (int x = 0; x < BOARDWIDTH; x++)
//                {
//                    _cells[y, x] = CellState.Unknown;
//                }
//            }
//        }

//        public void Update(Point location, CellState cellState)
//        {
//            _cells[location.Y, location.X] = cellState;
//        }

//        public CellState[,] GetCells()
//        {
//            var cells = new CellState[BOARDHEIGHT, BOARDWIDTH];

//            for (int y = 0; y < BOARDHEIGHT; y++)
//            {
//                for (int x = 0; x < BOARDWIDTH; x++)
//                {
//                    cells[y, x] = _cells[y, x];
//                }
//            }

//            return cells;
//        }
//    }
//}
