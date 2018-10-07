

namespace Domain
{
    public class Game
    {
        public Player Player { get; set; }

        public Grid Grid { get; set; }

        public bool FireShot(string targetedSquareCoordinate)
        {
            return Grid.FireShotAtGrid(targetedSquareCoordinate);
        }

        public bool IsGameOver()
        {
            return !Grid.AnyFloatingShips();
        }
    }
}
