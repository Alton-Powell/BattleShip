using Domain;


namespace Application.Interfaces
{
    public interface IGameEngine
    {
        void InitializeGame(string playerName);
        bool IsGameOver();
        bool FireShot(string targetedSquare);

        string PlayerName { get; }
    }
}
