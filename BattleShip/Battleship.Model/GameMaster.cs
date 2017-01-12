using System;
using System.Collections.Generic;

namespace Battleship.Model
{
    public class GameMaster : IGameMaster
    {
        private readonly int _rowCount;
        private readonly int _columnCount;

        public GameMaster(int rowCount, int columnCount)
        {
            _rowCount = rowCount;
            _columnCount = columnCount;
        }

        public Ship[] CreateGameboard(int gameId)
        {
            var ships = new List<Ship>();
            var shipTypes = (ShipType[])Enum.GetValues(typeof(ShipType));

            Random random = new Random(gameId);

            var board = new int[_rowCount, _columnCount];

            for (int s = 0; s < shipTypes.Length; s++)
            {
                var ship = Ship.GetSize(shipTypes[s]);

                bool added = false;
                while (!added)
                {
                    int x = random.Next(0, _columnCount);
                    int y = random.Next(0, _rowCount);
                    bool horizontal = (random.Next(2) == 0);
                    if (horizontal)
                    {
                        // Check for vertical space
                        bool hasSpace = true;
                        for (int i = 0; i < ship; i++)
                        {
                            if (y + i >= _rowCount)
                            {
                                hasSpace = false;
                                break;
                            }
                            if (board[y + i, x] != 0)
                            {
                                hasSpace = false;
                                break;
                            }
                        }
                        if (!hasSpace)
                        {
                            // No room there, check again
                            continue;
                        }
                        ships.Add(new Ship(shipTypes[s], new Cell(y, x), Direction.Horizontal));
                        for (int i = 0; i < ship; i++)
                        {
                            board[y + i, x] = s + 1;
                        }
                        added = true;
                    }
                    else
                    {
                        // Check for horizontal space
                        bool hasSpace = true;
                        for (int i = 0; i < ship; i++)
                        {
                            if (x + i >= _columnCount)
                            {
                                hasSpace = false;
                                break;
                            }
                            if (board[y, x + i] != 0)
                            {
                                hasSpace = false;
                                break;
                            }
                        }
                        if (!hasSpace)
                        {
                            // No room there, check again
                            continue;
                        }
                        ships.Add(new Ship(shipTypes[s], new Cell(y, x), Direction.Vertical));
                        for (int i = 0; i < ship; i++)
                        {
                            board[y, x + i] = s + 1;
                        }
                        added = true;
                    }
                }
            }

            return ships.ToArray();
        }
    }
}