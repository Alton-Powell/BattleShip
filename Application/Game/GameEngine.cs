using Application.Interfaces;
using Domain;
using System;

namespace Application
{
    public class GameEngine : IGameEngine
    {

        private readonly IGridBuilder _gridBuilder;
        private Game _game;
       

        public GameEngine(IGridBuilder gridBuilder)
        {
            _gridBuilder = gridBuilder ?? throw new ArgumentNullException(nameof(gridBuilder));
        }

        public void InitializeGame(string playerName)
        {
            Player player = new Player(playerName);

            Grid grid = _gridBuilder.Create();
            
            _game = new Game();
            _game.Player = player;
            _game.Grid = grid;
        }

        public bool FireShot(string targetedSquare)
        {
            return _game.FireShot(targetedSquare);
        }

        public bool IsGameOver()
        {
            return _game.IsGameOver();
        }
    }
}
