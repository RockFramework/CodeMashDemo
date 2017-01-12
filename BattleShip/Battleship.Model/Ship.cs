using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Battleship.Model
{
    public class Ship
    {
        private readonly ShipType _shipType;
        private readonly int _shipSize;
        private readonly Cell _location;
        private readonly Direction _direction;
        private readonly ReadOnlyCollection<Cell> _occupiedCells;

        public Ship(ShipType shipType, Cell location, Direction direction)
        {
            _shipType = shipType;
            _location = location;
            _direction = direction;

            var size = GetSize(shipType);
            _shipSize = size;
            _occupiedCells = GetOccupiedCells(location, direction, size);
        }

        public ShipType ShipType
        {
            get { return _shipType; }
        }

        public int ShipSize
        {
            get { return _shipSize; }
        }

        public Cell Location
        {
            get { return _location; }
        }

        public Direction Direction
        {
            get { return _direction; }
        }

        public ReadOnlyCollection<Cell> OccupiedCells
        {
            get { return _occupiedCells; }
        }

        public bool IsHit(Cell guess)
        {
            // TODO: [student] Implement
            return _occupiedCells.Any(c => c.Row == guess.Row && c.Column == guess.Column);
        }

        public bool IsSunk(bool[,] history)
        {
            // TODO: [student] Implement
            return _occupiedCells.All(c => history[c.Row, c.Column]);
        }

        internal static int GetSize(ShipType ship)
        {
            switch (ship)
            {
                case ShipType.AircraftCarrier:
                    return 5;
                case ShipType.Battleship:
                    return 4;
                case ShipType.Destroyer:
                    return 3;
                case ShipType.Submarine:
                    return 3;
                case ShipType.PatrolBoat:
                    return 2;
                default:
                    throw new ArgumentOutOfRangeException("ship");
            }
        }

        private static ReadOnlyCollection<Cell> GetOccupiedCells(
            Cell location, Direction direction, int shipSize)
        {
            var occupiedCells = new List<Cell>();

            for (int i = 0; i < shipSize; i++)
            {
                if (direction == Direction.Horizontal)
                {
                    occupiedCells.Add(new Cell(location.Row, location.Column + i));
                }
                else
                {
                    occupiedCells.Add(new Cell(location.Row + i, location.Column));
                }
            }

            return occupiedCells.AsReadOnly();
        }
    }
}