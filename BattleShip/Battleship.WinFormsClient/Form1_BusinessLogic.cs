using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using Battleship.Model;

namespace Battleship.WinFormsClient
{
    public partial class Form1
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private int CreateNewGame()
        {
            // Our goal here is obtain the ID of a new game. To do so, we'll make a call to our API.
            // The API will be responsible for actually generating the ID.

            HttpContent content = new StringContent(""); // Empty content for creating a new game.

            var httpResponse = _httpClient.PostAsync("http://localhost:30791/api/game", content).Result;
            httpResponse.EnsureSuccessStatusCode();

            var createGameResponse = httpResponse.Content.ReadAsAsync<CreateGameResponse>().Result;
            return createGameResponse.GameId;
        }

        // /api/game/{gameId}/guess POST
        private GuessResponse GetGuessResponse(Cell cell, int gameId, string gameState)
        {
            HttpContent content = new ObjectContent<GuessRequest>(
                new GuessRequest { Cell = cell, GameState = gameState },
                new JsonMediaTypeFormatter());

            var requestUri = string.Format("http://localhost:30791/api/game/{0}", gameId);

            var httpResponse = _httpClient.PostAsync(requestUri, content).Result;
            httpResponse.EnsureSuccessStatusCode();

            var guessResponse = httpResponse.Content.ReadAsAsync<GuessResponse>().Result;
            return guessResponse;
        }
    }
}