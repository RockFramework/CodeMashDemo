using System;
using System.Collections;
using System.Linq;
using System.Web.Http;
using Battleship.Model;

namespace Battleship.WebApi.Controllers
{
    public class GameController : ApiController
    {
        private readonly Random _seedGenerator;

        public GameController()
            : this(new Random())
        {
        }

        public GameController(Random seedGenerator)
        {
            _seedGenerator = seedGenerator;
        }

        [Route("api/game")]
        public IHttpActionResult Post()
        {
            var gameId = _seedGenerator.Next(1000000000, int.MaxValue);
            return Ok(new CreateGameResponse { GameId = gameId });
        }

        [Route("api/game/{gameId}")]
        public IHttpActionResult Post(int gameId, GuessRequest guessRequest)
        {
            if (guessRequest == null) 
            {
                return BadRequest();
            }

            var history = DeserializeGameState(guessRequest.GameState, 10, 10);
            history[guessRequest.Cell.Row, guessRequest.Cell.Column] = true;
            var nextGameState = SerializeGameState(history);

            var ships = GetShips(gameId);

            var response = GetGuessResponse(guessRequest.Cell, ships, history, nextGameState);
            return Ok(response);
        }

        private Ship[] GetShips(int gameId)
        {
            var gameMaster = new GameMaster(10, 10);

            return gameMaster.CreateGameboard(gameId);

            // TODO: [core] Implement
            return new[]
            {
                new Ship(ShipType.AircraftCarrier, new Cell(0, 0), Direction.Horizontal),
                new Ship(ShipType.Battleship, new Cell(1, 0), Direction.Horizontal),
                new Ship(ShipType.Destroyer, new Cell(2, 0), Direction.Horizontal),
                new Ship(ShipType.PatrolBoat, new Cell(3, 0), Direction.Horizontal),
                new Ship(ShipType.Submarine, new Cell(4, 0), Direction.Horizontal)
            };
        }

        private GuessResponse GetGuessResponse(Cell guess, Ship[] ships, bool[,] history, string gameState)
        {
            var hitShip = GetHitShip(guess, ships);
            ShipType? sunkShip = null;
            var gameOver = false;

            if (hitShip != null)
            {
                if (hitShip.IsSunk(history))
                {
                    sunkShip = hitShip.ShipType;

                    if (IsGameOver(ships, history))
                    {
                        gameOver = true;
                    }
                }
            }

            return new GuessResponse(gameState, hitShip != null, sunkShip, gameOver);
        }

        private Ship GetHitShip(Cell guess, Ship[] ships)
        {
            // TODO: Implement
            return ships.FirstOrDefault(s => s.IsHit(guess));
        }

        private bool IsGameOver(Ship[] ships, bool[,] history)
        {
            // TODO: Implement
            return ships.All(s => s.IsSunk(history));
        }

        private static string SerializeGameState(bool[,] history)
        {
            var rowCount = history.GetUpperBound(0) + 1;
            var columnCount = history.GetUpperBound(1) + 1;

            var flatHistory = new bool[rowCount * columnCount];

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    flatHistory[row * columnCount + column] = history[row, column];
                }
            }

            var bitArray = new BitArray(flatHistory);

            var size = bitArray.Count / 8;
            if (bitArray.Count % 8 != 0)
            {
                size++;
            }
            var bytes = new byte[size];
            bitArray.CopyTo(bytes, 0);

            var gameState = Convert.ToBase64String(bytes);
            return gameState;
        }

        private static bool[,] DeserializeGameState(string gameState, int rowCount, int columnCount)
        {
            bool[] flatHistory;

            if (gameState == null)
            {
                flatHistory = new bool[100];
            }
            else
            {
                var bytes = Convert.FromBase64String(gameState);
                var bitArray = new BitArray(bytes);

                flatHistory = new bool[bitArray.Count];
                bitArray.CopyTo(flatHistory, 0);
            }

            var history = new bool[rowCount, columnCount];

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    history[row, column] = flatHistory[row * columnCount + column];
                }
            }

            return history;
        }
    }
}
