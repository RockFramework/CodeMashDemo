namespace Battleship.Model
{
    public class GuessRequest
    {
        public Cell Cell { get; set; }
        public string GameState { get; set; }
    }
}