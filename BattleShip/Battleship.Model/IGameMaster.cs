namespace Battleship.Model
{
    public interface IGameMaster
    {
        Ship[] CreateGameboard(int gameId);
    }
}