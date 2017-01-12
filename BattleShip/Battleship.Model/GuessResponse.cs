using System;

namespace Battleship.Model
{
    public class GuessResponse
    {
        private readonly bool _hit;
        private readonly ShipType? _sunkShip;
        private readonly bool _gameOver;
        private readonly string _gameState;

        public GuessResponse(string gameState, bool hit, ShipType? sunkShip = null, bool gameOver = false)
        {
            if (!hit && sunkShip.HasValue) throw new ArgumentException("'sunkShip' cannot have a value when 'hit' is false.", "sunkShip");

            _hit = hit;
            _gameOver = gameOver;
            _sunkShip = sunkShip;
            _gameState = gameState;
        }

        public bool Hit
        {
            get { return _hit; }
        }

        public ShipType? SunkShip
        {
            get { return _sunkShip; }
        }

        public bool GameOver
        {
            get { return _gameOver; }
        }

        public string GameState
        {
            get { return _gameState; }
        }
    }
}